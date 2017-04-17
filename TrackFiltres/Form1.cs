using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms.DataVisualization.Charting;
using Cursor = System.Windows.Forms.Cursor;

//using SparseCollections;
//using Mathematics;

namespace TrackFiltres
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Cur = pictureBoxTrack.Cursor;
            CurHand = GetCursorFromBitmap();
        }

        private PointsCoordSet listMarkedPC;
        private PointsCoordSet listSourcePC;
        private PointsCoordSet listKalmanPC;
        //List<PointCoords> listKalmanMarkPC;
        private string sPictureDir = "";
        private bool bMark = false;
        private bool bDrawLog = false;
        private bool bDrawMark = false;
        private bool bDrawTrack = false;
        private bool bIsFin = false;
        private bool bKalTrack = false;
        //bool bKalMark = false;
        private bool bMatr3 = false;
        private bool bDrawKalmanXY = false;
        private Point mouseDown;
        private int startx = 0; // offset of image when mouse was pressed
        private int starty = 0;
        private int imgx = 0; // current offset of image
        private int imgy = 0;
        private Cursor Cur = null;
        private Cursor CurHand = null;
        private bool mousepressed = false; // true as long as left mousebutton is pressed
        private float zoom = 1;
        private Bitmap CurrentBitmap = null;

        /// <summary>
        /// NumFrame posX posY Width Class
        /// </summary>
        private int NCurrentFile = -1;

        private int NFirstFile = -1;
        private int NLastFile = -1;
        //string CoordsFile = null;
        private string strLogName;
        private string strMark;
//        StreamWriter strMarkStream = null;
        private StreamReader strLog = null;

        /// NumFrame posX posY Width Class
        private string GetFileName(int iNum)
        {
            var strNameFile = sPictureDir + "\\" + iNum.ToString() + ".jpg";
            return strNameFile;
        }

        private bool SelectCoordsFile()
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Select an coords file.";
            ofd.Filter = "Txt files(*.txt)|*.txt";
            //string fileName;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                strLog = new StreamReader(ofd.FileName);
                return true;
            }
            return false;
        }

        private void SetBitmapToPB(string filePic)
        {
            var streamReader = new StreamReader(filePic);
            CurrentBitmap = (Bitmap) Image.FromStream(streamReader.BaseStream);
            streamReader.Close();
            //pictureBoxTrack.Image = CurrentBitmap;
            pictureBoxTrack.Refresh();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //private bool FillCoordData(StreamReader sr, List<PointCoords> list,bool bIsNeed,Color col)
        private bool FillCoordData(StreamReader sr, PointsCoordSet list, bool bIsNeed, Color col)
        {
            if (sr == null)
            {
                if (bIsNeed)
                    MessageBox.Show("Не задан файл координат");
                return false;
            }
            //list = new List<PointCoords>();
            string strTemp;
            var iNum = -1;

            while (true)
            {
                strTemp = sr.ReadLine();
                if (strTemp == null)
                    break;

                var pC = new PointCoords();
                if (pC.SetDataFromFile(strTemp, col) == false)
                    continue;
                if (iNum == -1)
                    iNum = pC.NumFrame;
                else
                {
                    iNum++;
                    if (pC.NumFrame > iNum)
                    {
                        var iNumDel = pC.NumFrame;
                        while (iNumDel != iNum)
                        {
                            var pC1 = new PointCoords();
                            pC1.NumFrame = iNum;
                            pC1.col = pC.col;
                            pC1.Width = pC.Width;
                            list.Add(pC1);
                            iNum++;
                        }
                    }
                }
                list.Add(pC);
            }
            if (NCurrentFile == -1)
            {
                NCurrentFile = NFirstFile = list[0].NumFrame;
                NLastFile = list[list.Count - 1].NumFrame;
            }
            var iReminder = NLastFile - NFirstFile + 1 - list.Count;
            iNum++;
            for (var jc = 0; jc < iReminder; jc++)
            {
                var pC1 = new PointCoords();
                pC1.NumFrame = iNum;
                pC1.col = Color.AntiqueWhite;
                pC1.Width = 40;
                list.Add(pC1);
                iNum++;
            }
            PointsCoordSet.AddList(list);
            return true;
        }

        private void SelectImageFile()
        {
            var ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
                SetBitmapToPB(ofd.FileName);
        }

        private string SelectDir()
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();
            // Set the help text description for the FolderBrowserDialog.
            folderBrowserDialog1.Description = "Выберите директорию";
            folderBrowserDialog1.ShowNewFolderButton = false;

            folderBrowserDialog1.SelectedPath = Directory.GetCurrentDirectory();
            var result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
                return "";
            var sDirName = folderBrowserDialog1.SelectedPath;
            return sDirName;
        }

        private Cursor GetCursorFromBitmap()
        {
            var myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            var myType = typeof(Form1);
            var NameCursor = myType.Namespace + ".Hand.png";
            //string NameCursor = "VisualTemplate.Hand.png";
            //Stream myStream = myAssembly.GetManifestResourceStream(NameCursor);
            var myStream = myAssembly.GetManifestResourceStream(NameCursor);
            var bmpCur = new Bitmap(myStream);
            var ptr = bmpCur.GetHicon();
            return new Cursor(ptr);
        }

        private Point GetPictureXY(int x, int y)
        {
            float xP = x;
            float yP = y;
            xP = (float) x/zoom - (float) imgx;
            yP = (float) y/zoom - (float) imgy;
            var pOut = new Point(-1, -1);
            var bpr = (xP < 0) || (xP > CurrentBitmap.Width) || (yP < 0) || (yP > CurrentBitmap.Height);
            if (bpr == false)
            {
                pOut.X = (int) xP;
                pOut.Y = (int) yP;
            }
            return pOut;
        }

        private Point GetBoxXY(int x, int y)
        {
            float xP = x;
            float yP = y;
            //xP = (float)x * zoom + (float)imgx;
            //yP = (float)y * zoom + (float)imgy;
            xP = (float) x + (float) imgx;
            yP = (float) y + (float) imgy;
            var pOut = new Point(-1, -1);
            var bpr = (xP < 0) || (xP > CurrentBitmap.Width) || (yP < 0) || (yP > CurrentBitmap.Height);
            if (bpr == false)
            {
                pOut.X = (int) xP;
                pOut.Y = (int) yP;
            }
            return pOut;
        }

        private Point ptPaint;
        //bool isInitPoint = false;


        private void директорияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sPictureDir = SelectDir();
            if (sPictureDir == "")
                return;
            var files = Directory.GetFiles(sPictureDir, "*.txt");
            if (files.Length > 1)
            {
                MessageBox.Show("Более одного лог файла в директории. Выберите файл отдельно");
                логФайлToolStripMenuItem.Enabled = true;
                return;
            }
            if (files.Length == 0)
            {
                MessageBox.Show("Лог файл в директории отсутствует. Выберите файл отдельно");
                логФайлToolStripMenuItem.Enabled = true;
                return;
            }

            NCurrentFile = -1;
            strLogName = files[0];
            strMark = strLogName.Substring(0, strLogName.Length - 3);
            strMark += "mrk";
            listMarkedPC = new PointsCoordSet("Marked");
            if (File.Exists(strMark))
            {
                var strMarkRead = new StreamReader(strMark);
                FillCoordData(strMarkRead, listMarkedPC, false, Color.Green);
            }
            strLog = new StreamReader(strLogName);
            listSourcePC = new PointsCoordSet("Source");
            FillCoordData(strLog, listSourcePC, true, Color.Red);


            ShowImages();
            отметитьОбъектыToolStripMenuItem.Enabled = true;
            фильтрКальманаToolStripMenuItem.Enabled = true;
            матрицаToolStripMenuItem.Enabled = true;
        }

        private void ShowImages()
        {
            var iListCount = listSourcePC.Count;
            MessageBox.Show("Прочитано " + iListCount.ToString() + " строк");
            if (iListCount > 1)
                buttonright.Enabled = true;


            var strPic = GetFileName(NCurrentFile);
            SetBitmapToPB(strPic);
            labelNumFrame.Text = NCurrentFile.ToString();
        }

        private void файлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectImageFile();
        }

        private void логФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectCoordsFile() == true)
                отметитьОбъектыToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void отметитьОбъектыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sPictureDir == "")
            {
                MessageBox.Show("Не задана директория");
                return;
            }
            if (strLog == null)
            {
                MessageBox.Show("Не задан файл координат");
                return;
            }
            listMarkedPC = new PointsCoordSet("Marked");
            PointsCoordSet.AddList(listMarkedPC);
            //
            //FillCoordData();
            отметитьОбъектыToolStripMenuItem.Enabled = false;
            bMark = true;
            buttonright.Enabled = false;
            buttonLeft.Enabled = false;
        }

        private PointCoords FindPoint(int NumFrame, List<PointCoords> pCoords, out int index)
        {
            index = -1;
            if (pCoords == null)
                return null;
            PointCoords pt = null;
            for (var jc = 0; jc < pCoords.Count; jc++)
            {
                pt = pCoords[jc];
                index = jc;
                if (pt.NumFrame == NumFrame)
                    return pt;
            }
            return null;
        }

        private void SaveCenter(Point pt)
        {
            int index;
            var pC = FindPoint(NCurrentFile, listMarkedPC, out index);
            if (pC == null)
            {
                pC = new PointCoords();
                pC.NumFrame = NCurrentFile;
                listMarkedPC.Add(pC);
            }
            pC.X = pt.X;
            pC.Y = pt.Y;
        }

        private void PictureBoxDoubleClick(object sender, MouseEventArgs e)
        {
            // isInitPoint = true;
            var ptDC = GetPictureXY(e.X, e.Y);
            if (ptDC.X == -1)
                return;
            ptPaint = new Point(ptDC.X, ptDC.Y);
            if (bMark == true)
                SaveCenter(ptPaint);
            else
                labelNumFrame.Text = "Нет марк";
            pictureBoxTrack.Refresh(); // calls imageBox_Paint
            if ((bIsFin == true) && (bMark == true))
            {
                bMark = false;
                bIsFin = false;
                сохранитьМаркировкуToolStripMenuItem.Enabled = true;
                return;
            }
            if ((bIsFin == false) && (bMark == true))
                buttonRight();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pt"></param>
        /// <returns></returns>
        private bool DrawPoint(Graphics g, PointCoords pt)
        {
            if (pt == null)
                return false;
            if (pt.X == -1)
                return false;
            //Brush BrushPoint = new SolidBrush(Color.Brown);
            var pen = new Pen(pt.col);
            //Pen pen2 = new Pen(Color.Red);
            //Pen pen3 = new Pen(Color.Blue);
            //int szEllipse = 50;
            var ptDC = GetBoxXY(pt.X, pt.Y);
            if (ptDC.X == -1)
                return false;
            var szEllipse = pt.Width;
            g.DrawEllipse(pen, ptDC.X - szEllipse/2, ptDC.Y - szEllipse/2, szEllipse, szEllipse);
            return true;
            //g.DrawEllipse(pen2, ptDC.X - szEllipse, ptDC.Y - szEllipse, 2 * szEllipse, 2 * szEllipse);
            //g.DrawEllipse(pen3, ptPaint.X, ptPaint.Y, 3 * szEllipse, 3 * szEllipse);
            //+ imgx
            //g.FillEllipse(BrushPoint, ptPaint.X - 5 + imgx, ptPaint.Y - 5 + imgy, 10, 10);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="pCoords"></param>
        private void PaintRoute(Graphics g, List<PointCoords> pCoords)
        {
            var NCurP = NCurrentFile;
            int index;
            var pCoord = FindPoint(NCurP, pCoords, out index);
            if (pCoord == null)
                return;
            if (DrawPoint(g, pCoord) == false)
                return;
            if (bDrawTrack == false)
                return;
            //Draw track
            if (index == 0)
                return;
            var pen = new Pen(pCoord.col);
            for (var jc = 0; jc < index - 1; jc++)
            {
                if (pCoords[jc + 1].NumFrame - pCoords[jc].NumFrame != 1)
                    continue;
                DrawPoint(g, pCoords[jc]);
                //g.DrawLine(pen, pCoords[jc].X, pCoords[jc].Y, pCoords[jc + 1].X, pCoords[jc + 1].Y);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureBoxPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.ScaleTransform(zoom, zoom);

            if (CurrentBitmap != null)
                e.Graphics.DrawImage(CurrentBitmap, imgx, imgy);
            //Paint source points
            if (bDrawLog == true)
                PaintRoute(e.Graphics, listSourcePC);
            if (bDrawMark == true)
                PaintRoute(e.Graphics, listMarkedPC);
            if (bKalTrack == true)
                PaintRoute(e.Graphics, listKalmanPC);


            if (bDrawKalmanXY == true)
                PaintRoute(e.Graphics, listKalman1XY);
            if (bMatr3 == true)
                PaintRoute(e.Graphics, listMatrix3);
        }

        private void PictureBoxMouseDown(object sender, MouseEventArgs e)
        {
            var mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Left)
                if (!mousepressed)
                {
                    pictureBoxTrack.Cursor = CurHand; //Cursors.Hand;
                    mousepressed = true;
                    mouseDown = mouse.Location;
                    startx = imgx;
                    starty = imgy;
                }
        }

        private void PictureBoxMouseMov(object sender, MouseEventArgs e)
        {
            /*var mouse = e as MouseEventArgs;

            if (mouse.Button == MouseButtons.Left)
            {
                if (!mousepressed)
                    return;
                var mousePosNow = mouse.Location;

                var deltaX = mousePosNow.X - mouseDown.X;
                // the distance the mouse has been moved since mouse was pressed
                var deltaY = mousePosNow.Y - mouseDown.Y;

                imgx = (int) (startx + deltaX/zoom); // calculate new offset of image based on the current zoom factor
                imgy = (int) (starty + deltaY/zoom);

                pictureBoxTrack.Refresh();
            }*/
        }

        private void PictureBoxMoseUp(object sender, MouseEventArgs e)
        {
            if (mousepressed)
            {
                mousepressed = false;
                pictureBoxTrack.Cursor = Cur;
            }
        }

        /// <summary>
        /// А это будет событие родительского окна
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            var oldzoom = zoom;

            if (e.Delta > 0)
                zoom += 0.1F;

            else if (e.Delta < 0)
                zoom = Math.Max(zoom - 0.1F, 0.01F);

            var mouse = e as MouseEventArgs;
            var mousePosNow = mouse.Location;

            var x = mousePosNow.X - pictureBoxTrack.Location.X; // Where location of the mouse in the pictureframe
            var y = mousePosNow.Y - pictureBoxTrack.Location.Y;

            var oldimagex = (int) (x/oldzoom); // Where in the IMAGE is it now
            var oldimagey = (int) (y/oldzoom);

            var newimagex = (int) (x/zoom); // Where in the IMAGE will it be when the new zoom i made
            var newimagey = (int) (y/zoom);

            imgx = newimagex - oldimagex + imgx; // Where to move image to keep focus on one point
            imgy = newimagey - oldimagey + imgy;
            if (imgx < 0)
                imgx = 0;
            if (imgy < 0)
                imgy = 0;
            pictureBoxTrack.Refresh(); // calls imageBox_Paint
        }

        private void buttonRight()
        {
            NCurrentFile++;
            if (NCurrentFile == NLastFile)
                buttonright.Enabled = false;
            var strPic = GetFileName(NCurrentFile);
            var strPicNext = GetFileName(NCurrentFile + 1);
            if (File.Exists(strPicNext) == false)
            {
                if (bMark == true)
                    bIsFin = true;

                NLastFile = NCurrentFile;
                buttonright.Enabled = false;
            }
            labelNumFrame.Text = NCurrentFile.ToString();
            SetBitmapToPB(strPic);
            if (bMark == false)
                buttonLeft.Enabled = true;
        }

        private void buttonleft()
        {
            NCurrentFile--;
            if (NCurrentFile - 1 < NFirstFile)
                buttonLeft.Enabled = false;
            var strPic = GetFileName(NCurrentFile);
            labelNumFrame.Text = NCurrentFile.ToString();
            SetBitmapToPB(strPic);
            if (bMark == false)
                buttonright.Enabled = true;
        }

        private void buttonright_Click(object sender, EventArgs e)
        {
            buttonRight();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            buttonleft();
        }

        private void checkBoxDrawSource_CheckedChanged(object sender, EventArgs e)
        {
            bDrawLog = checkBoxDrawSource.Checked;
            pictureBoxTrack.Refresh();
        }

        private void checkBoxDrawMarked_CheckedChanged(object sender, EventArgs e)
        {
            bDrawMark = checkBoxDrawMarked.Checked;
            pictureBoxTrack.Refresh();
        }

        private void сохранитьМаркировкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listMarkedPC.Count == 0)
            {
                MessageBox.Show("Маркировка сохранена");
                return;
            }
            var strMark = strLogName.Substring(0, strLogName.Length - 3);
            strMark += "mrk";
            var sw = new StreamWriter(strMark);
            for (var jc = 0; jc < listMarkedPC.Count; jc++)
            {
                var pt = listMarkedPC[jc];
                var strVal = pt.NumFrame.ToString() + ' ' + pt.X.ToString() + ' ' + pt.Y.ToString() + ' ' +
                             pt.Width.ToString() + ' ' + '0';
                sw.WriteLine(strVal);
            }
            sw.Close();
            MessageBox.Show("Маркировка сохранена");
            // buttonright.Enabled = true;
            buttonLeft.Enabled = true;
        }

        private void checkBoxTracks_CheckedChanged(object sender, EventArgs e)
        {
            bDrawTrack = checkBoxTracks.Checked;
            pictureBoxTrack.Refresh();
        }

        private PointsCoordSet CalcKalman(PointsCoordSet list)
        {
            var listKalman = new PointsCoordSet("Kalman");
            if (list.Count <= 0)
                return listKalman;
            var KallFil = new KalmanFilter();
            var Num = list[0].NumFrame - 1;
            for (var jc = 0; jc < list.Count; jc++)
            {
                var pt = new Point(list[jc].X, list[jc].Y);
                if (list[jc].X == -1)
                    list[jc].Y = -1;
                var ptC = KallFil.CalcEstimationPoint(pt, list[jc].NumFrame) + list[jc];
                ptC.col = Color.Fuchsia;
                listKalman.Add(ptC);
            }
            return listKalman;
        }

        /// <summary>
        /// Работаем со скоростью
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private void CalcKalmanV(List<PointCoords> list, string strName, bool iIsPredict, bool bNoA)
        {
            var listKalman = new PointsCoordSet(strName);
            if (list.Count <= 0)
                return;
            var KallFilX = new Kalc3Dim("X", true);
            var KallFilY = new Kalc3Dim("Y", false);
            var Num = list[0].NumFrame - 1;
            for (var jc = 0; jc < list.Count; jc++)
            {
                var pt = new Point(list[jc].X, list[jc].Y);
                var ptC = list[jc] + pt;
                ptC.col = Color.Fuchsia;
                if (list[jc].X == -1)
                    ptC.col = Color.Yellow;
                var x = KallFilX.SetData(list[jc].X, list[jc].NumFrame, iIsPredict, bNoA);
                var y = KallFilY.SetData(list[jc].Y, list[jc].NumFrame, iIsPredict, bNoA);
                ptC.X = (int) x;
                ptC.Y = (int) y;
                //PointCoords ptC = KallFil.CalcEstimationPoint(pt, list[jc].NumFrame) + list[jc];

                listKalman.Add(ptC);
            }
            PointsCoordSet.AddList(listKalman);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private PointsCoordSet CalcKalmanOpt(PointsCoordSet list, string strName, Kalc3Dim KallFil, bool iIsPredict,
            bool bNoA, bool bIsX)
        {
            var listKalman = new PointsCoordSet(strName);
            var Num = list[0].NumFrame - 1;
            for (var jc = 0; jc < list.Count; jc++)
            {
                var pt = new Point(list[jc].X, list[jc].Y);
                var ptC = list[jc] + pt;
                ptC.col = Color.Fuchsia;
                if (list[jc].X == -1)
                    ptC.col = Color.Yellow;
                if (bIsX == true)
                {
                    var x = KallFil.SetData(list[jc].X, list[jc].NumFrame, iIsPredict, bNoA);
                    ptC.X = (int) x;
                }
                else
                {
                    var y = KallFil.SetData(list[jc].Y, list[jc].NumFrame, iIsPredict, bNoA);
                    ptC.Y = (int) y;
                }


                listKalman.Add(ptC);
            }
            //PointsCoordSet.AddList(listKalman);
            return listKalman;
        }

        private void запускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listKalmanPC = CalcKalman(listSourcePC);


            //KallFil.CalcEstimationScalar(;
        }

/*private void checkBoxKalF_CheckedChanged(object sender, EventArgs e)
                                {
                                    bKalTrack = checkBoxKalF.Checked;
                                    pictureBoxTrack.Refresh();
                                }
                        
                                private void запускМаркToolStripMenuItem_Click(object sender, EventArgs e)
                                {
                                    listKalmanMarkPC = CalcKalman(listMarkedPC);
                                }
                        
                                private void checkBoxKalMark_CheckedChanged(object sender, EventArgs e)
                                {
                                    bKalMark = checkBoxKalMark.Checked;
                                    pictureBoxTrack.Refresh();
                                }*/

        private void оптимизироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private List<PointCoords> listKalman1XY;
        //private List<PointCoords> listKalmanPXY;
        //private List<PointCoords>[] listKalmans;
        //List<PointCoords> listKalmanNXY;
        private List<PointCoords> CalcKalman1Dim(List<PointCoords> list)
        {
            var listKalman = new List<PointCoords>();
            if (list.Count <= 0)
                return listKalman;
            var KallFilX = new Kalman1Dim("X");
            var KallFilY = new Kalman1Dim("Y");
            var Num = list[0].NumFrame - 1;
            for (var jc = 0; jc < list.Count; jc++)
            {
                if (list[jc].X == -1)
                    list[jc].Y = -1;

                var pt = new Point((int) KallFilX.CalcEstimation1Dim(list[jc].X),
                    (int) KallFilY.CalcEstimation1Dim(list[jc].Y));
                var ptC = pt + list[jc];
                ptC.col = Color.Fuchsia;
                listKalman.Add(ptC);
            }
            return listKalman;
        }

        private void одномерныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcKalmanV(listSourcePC, "3D", false, false);
            сравнитьКалмановToolStripMenuItem.Enabled = true;
        }

        private void checkBoxKalmanXY_CheckedChanged(object sender, EventArgs e)
        {
            bDrawKalmanXY = checkBoxKalmanXY.Checked;
            pictureBoxTrack.Refresh();
        }

        private double det(MatrixS a)
        {
            double s = 0;
            if (a.cols == 2)
            {
                s = a[0, 0]*a[1, 1] - a[0, 1]*a[1, 0];
                return s;
            }
            if (a.cols != 3)
                return s;
            var t1 = a[0, 0]*(a[1, 1]*a[2, 2] - a[1, 2]*a[2, 1]);
            var t2 = a[0, 1]*(a[1, 0]*a[2, 2] - a[1, 2]*a[2, 0]);
            var t3 = a[0, 2]*(a[1, 0]*a[2, 1] - a[1, 1]*a[2, 0]);
            s = t1 - t2 + t3;
            return s;
        }

        private List<PointCoords> listMatrix3;

        private void матрицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*MatrixS A2 = new MatrixS(2, 2);
            A2[0, 0] = 0;
            A2[0, 1] = 1;
            A2[1, 0] = 1;
            A2[1, 1] = 1;
            det(A2);
            MatrixS A3 = MatrixS.Parse("0 0 1\r\n1 1 1\r\n4 2 1");
            ;
            MatrixS b3 = MatrixS.Parse("3\r\n6\r\n11");
            MatrixS x3 = A3.SolveWith(b3);
            // MatrixS A3Inv = A3.Invert();
            //   det(A3);
            MessageBox.Show("a = " + x3[0, 0].ToString() + " b = " + x3[1, 0].ToString() + " b = " + x3[2, 0].ToString());
            List<PointCoords> listKalman = new List<PointCoords>();*/
            if (listSourcePC.Count <= 0)
                return;
            listMatrix3 = new List<PointCoords>();
            var Num = listSourcePC[0].NumFrame - 1;
            var x3 = new MatrixS(3, 1);
            var y3 = new MatrixS(3, 1);
            var b3x = new MatrixS(3, 1);
            var b3y = new MatrixS(3, 1);
            for (var jc = 0; jc < listSourcePC.Count; jc++)
            {
                var pt = new Point(listSourcePC[jc].X, listSourcePC[jc].Y);
                //PointCoords ptC = new PointCoords();
                var ptC = pt + listSourcePC[jc];
                ;
                listMatrix3.Add(ptC);
                if (jc < 2)
                {
                    if (jc == 1)
                    {
                        b3x[0, 0] = listSourcePC[0].X;
                        b3x[1, 0] = listSourcePC[1].X;
                        b3y[0, 0] = listSourcePC[0].Y;
                        b3y[1, 0] = listSourcePC[1].Y;
                    }
                    continue;
                }
                if (jc > 2)
                {
                    ptC.X = (int) (x3[0, 0]*3.0*3.0 + x3[1, 0]*3.0 + x3[2, 0]);
                    ptC.Y = (int) (y3[0, 0]*3.0*3.0 + y3[1, 0]*3.0 + y3[2, 0]);
                    b3x[0, 0] = b3x[1, 0];
                    b3x[1, 0] = b3x[2, 0];
                    b3y[0, 0] = b3y[1, 0];
                    b3y[1, 0] = b3y[2, 0];
                }
                b3x[2, 0] = ptC.X;
                b3y[2, 0] = ptC.Y;


                var A3 = new MatrixS(3, 3);


                // t 0,1,2 y - listSourcePC[jc - 2] listSourcePC[jc - 1] listSourcePC[jc ]
                A3[0, 0] = 0; //x~2a 
                A3[0, 1] = 0; //xb
                A3[0, 2] = 1; //c
                ///
                A3[1, 0] = 1; //x~2a
                A3[1, 1] = 1; //xb 
                A3[1, 2] = 1; //c
                //
                A3[2, 0] = 4; //x~2a
                A3[2, 1] = 2; //xb
                A3[2, 2] = 1; //c


                if (listSourcePC[jc].X != -1)
                {
                    b3x[2, 0] = listSourcePC[jc].X;
                    b3y[2, 0] = listSourcePC[jc].Y;
                }
                x3 = A3.SolveWith(b3x);
                y3 = A3.SolveWith(b3y);
                ptC.col = Color.Moccasin;
            }
        }

        private void buttonBeg_Click(object sender, EventArgs e)
        {
            if (NCurrentFile == -1)
                return;
            NCurrentFile = NFirstFile - 1;
            buttonleft();
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            if (NCurrentFile == -1)
                return;
            NCurrentFile = NLastFile - 1;
            buttonRight();
        }

        /// <summary>
        /// Calc difference mean and deviation
        /// </summary>
        /// <param name="pEt"></param> 
        /// <param name="p1"></param>
        /// <param name="M">Meaning</param> 
        /// <param name="SD">deviation</param> 
        /// <returns>difference[]</returns>
        private int[] CalcCompareParams(PointsCoordSet pEt, PointsCoordSet p1, bool bIsX, out double M, out double SD,
            out int MMax)
        {
            var iMax = pEt.Count;
            int iE;
            int i1;
            var iE1 = new int[iMax];
            M = 0;
            SD = 0;
            MMax = 0;
            int jc;
            var iEmpty = 0;


            for (jc = 0; jc < iMax; jc++)
            {
                if (bIsX == true)
                {
                    iE = pEt[jc].X;
                    i1 = p1[jc].X;
                }
                else
                {
                    iE = pEt[jc].Y;
                    i1 = p1[jc].Y;
                }
                if (listSourcePC[jc].X == -1)
                    continue;
                iEmpty++;
                //                i2 = p2[jc].X;
                iE1[jc] = Math.Abs(iE - i1);
                M += iE1[jc];
                if (MMax < iE1[jc])
                    MMax = iE1[jc];
                //iE2 = Math.Abs(iE - i2);
                //iE12 = Math.Abs(iE1 - iE2);
                //iE12 = iE1 - iE2;
                //string sLog1 = iE.ToString() + "\t" + iE1.ToString() + "\t" + iE2.ToString() + "\t" + iE12.ToString();
                //LogFile.WriteLog(sLog1);
            }
            double dPow = 0;
            if (iEmpty == 0)
                return iE1;
            M /= iEmpty;
            for (jc = 0; jc < iMax; jc++)
            {
                if (listSourcePC[jc].X == -1)
                    continue;
                dPow += Math.Pow(iE1[jc] - M, 2);
                //iE2 = Math.Abs(iE - i2);
                //iE12 = Math.Abs(iE1 - iE2);
                //iE12 = iE1 - iE2;
                //string sLog1 = iE.ToString() + "\t" + iE1.ToString() + "\t" + iE2.ToString() + "\t" + iE12.ToString();
                //LogFile.WriteLog(sLog1);
            }
            SD = Math.Sqrt(dPow/iEmpty);
            return iE1;
        }

        private void сравнитьКалмановToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FC = new FormSelectCompare();
            if (FC.ShowDialog() != DialogResult.OK)
                return;
            bool bRes;
            var iRez = FC.ReturnRes(out bRes);
            if (bRes == false)
                return;
            var pEtalon = PointsCoordSet.GetListOnIndex(iRez[0]);
            var p1 = PointsCoordSet.GetListOnIndex(iRez[1]);
            var p2 = PointsCoordSet.GetListOnIndex(iRez[2]);
            if ((pEtalon == null) || (pEtalon == null) || (pEtalon == null))
                return;
            var iMax = pEtalon.Count;
            var sHeader = "Records " + pEtalon[0].NumFrame.ToString() + "-" + pEtalon[iMax - 1].NumFrame.ToString();
            LogFile.WriteLog(sHeader);
            sHeader = "Et= " + pEtalon.strName + " E1=" + p1.strName + " E2= " + p2.strName + " dl=" + "E1-E2";
            LogFile.WriteLog(sHeader);
            var sLog = "Et" + "\t" + "E1" + "\t" + "E2" + "\t" + "dl";
            LogFile.WriteLog(sLog);
            int iE;
            int iE12;
            LogFile.WriteLog("X");
            double M1X;
            double SD1X;
            double M2X;
            double SD2X;
            int MMax1;
            int MMax2;
            var iE1X = CalcCompareParams(pEtalon, p1, true, out M1X, out SD1X, out MMax1);
            var iE2X = CalcCompareParams(pEtalon, p2, true, out M2X, out SD2X, out MMax2);

            for (var jc = 0; jc < iMax; jc++)
            {
                iE = pEtalon[jc].X;
                iE12 = iE1X[jc] - iE2X[jc];
                var sLog1 = iE.ToString() + "\t" + p1[jc].X.ToString() + "(" + iE1X[jc].ToString() + ")" + "\t" +
                            p2[jc].X.ToString() + "(" + iE2X[jc].ToString() + ")" + "\t" + iE12.ToString();
                LogFile.WriteLog(sLog1);
            }
            sLog = "M1X = " + M1X.ToString() + " SD1X = " + SD1X.ToString() + " M2X = " + M2X.ToString() + " SD2X = " +
                   SD2X.ToString();
            LogFile.WriteLog(sLog);
            double M1Y;
            double SD1Y;
            double M2Y;
            double SD2Y;
            var iE1Y = CalcCompareParams(pEtalon, p1, false, out M1Y, out SD1Y, out MMax1);
            var iE2Y = CalcCompareParams(pEtalon, p2, false, out M2Y, out SD2Y, out MMax2);
            LogFile.WriteLog("Y");
            for (var jc = 0; jc < iMax; jc++)
            {
                iE = pEtalon[jc].Y;
                iE12 = iE1Y[jc] - iE2Y[jc];
                var sLog1 = iE.ToString() + "\t" + p1[jc].Y.ToString() + "(" + iE1Y[jc].ToString() + ")" + "\t" +
                            p2[jc].Y.ToString() + "(" + iE2Y[jc].ToString() + ")" + "\t" + iE12.ToString();
                LogFile.WriteLog(sLog1);
            }

            sLog = "M1Y = " + M1Y.ToString() + " SD1Y = " + SD1Y.ToString() + " M2Y = " + M2Y.ToString() + " SD2Y = " +
                   SD2Y.ToString();
            LogFile.WriteLog(sLog);
        }

        private void predictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcKalmanV(listSourcePC, "Predict", true, false);
            сравнитьКалмановToolStripMenuItem.Enabled = true;
        }

        private void нетАИVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CalcKalmanV(listSourcePC, "Predict NoA", true, true);
            сравнитьКалмановToolStripMenuItem.Enabled = true;
        }

        private int FindOptim(bool bIsX)
        {
            var fKM = new FormKalmanParms();
            if (fKM.ShowDialog() != DialogResult.OK)
                return -1;
            var iBeg = fKM.dBeg;
            var iFin = fKM.dFin;
            var iStep = fKM.dStep;
            var iNums = (int) ((iFin - iBeg)/iStep) + 1;
            double M1Min = 10000;
            double SD1Min = 10000;
            var NMaxMin = 10000;
            var iOpt = 0;
            var i = 0;
            var sLog = "Predict()";
            LogFile.WriteLog(sLog);
            double M1;
            double SD1;
            int NMax;
            //List <int> lInt = new List<int>();
            for (var jc = iBeg; jc < iFin; jc += iStep)
            {
                Kalc3Dim KallFil;
                if (bIsX == true)
                    KallFil = new Kalc3Dim("X", true);
                else
                    KallFil = new Kalc3Dim("Y", false);
                //KallFilX.SetInitParmsQ(jc);
                //KallFilX.SetInitParmsR(jc);
                KallFil.SetInitParmsQ(jc);
                //private PointsCoordSet CalcKalmanOpt(PointsCoordSet list, string strName,Kalc3Dim KallFil, bool iIsPredict, bool bNoA)
                var listKalmanTemp = CalcKalmanOpt(listSourcePC, sLog, KallFil, true, false, bIsX);
//                PointsCoordSet listKalmanTemp = CalcKalmanOpt(listSourcePC, sLog, KallFilX, false, false);
                var iE1 = CalcCompareParams(listMarkedPC, listKalmanTemp, true, out M1, out SD1, out NMax);
                i++;
                if (NMax > NMaxMin)
                    continue;
                if (NMax == NMaxMin)
                {
                    if (M1 > M1Min)
                        continue;
                    if (M1 == M1Min)
                        if (SD1 >= SD1Min)
                            continue;
                    iOpt = i - 1;
                }

                //string sLog1 = "Q[i,i] =" + jc.ToString() + " MX = " + M1X[i].ToString() + " SDX = " + SD1X[i].ToString() + " Max=" + MMax.ToString();
                //LogFile.WriteLog(sLog1);
            }
            return iOpt;
        }

        private void optimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fKM = new FormKalmanParms();
            if (fKM.ShowDialog() != DialogResult.OK)
                return;
            var iBeg = fKM.dBeg;
            var iFin = fKM.dFin;
            var iStep = fKM.dStep;
            var iNums = (int) ((iFin - iBeg)/iStep) + 1;
            var M1X = new double[iNums];
            var SD1X = new double[iNums];
            var i = 0;
            string sLog;
            sLog = "X: " + listSourcePC[0].NumFrame.ToString() + "-" +
                   listSourcePC[listSourcePC.Count - 1].NumFrame.ToString();
            LogFile.WriteLog(sLog);
            sLog = "Predict()";
            LogFile.WriteLog(sLog);
            int MMax;
            for (var jc = iBeg; jc < iFin; jc += iStep)
            {
                var KallFilX = new Kalc3Dim("X", true);
                //KallFilX.SetInitParmsQ(jc);
                //KallFilX.SetInitParmsR(jc);
                KallFilX.SetInitParmsQ(jc);
                //private PointsCoordSet CalcKalmanOpt(PointsCoordSet list, string strName,Kalc3Dim KallFil, bool iIsPredict, bool bNoA)
                var listKalmanTemp = CalcKalmanOpt(listSourcePC, sLog, KallFilX, true, false, true);
                //                PointsCoordSet listKalmanTemp = CalcKalmanOpt(listSourcePC, sLog, KallFilX, false, false);
                var iE1X = CalcCompareParams(listMarkedPC, listKalmanTemp, true, out M1X[i], out SD1X[i], out MMax);
                sLog = "Q[i,i] =" + jc.ToString() + " MX = " + M1X[i].ToString() + " SDX = " + SD1X[i].ToString() +
                       " Max=" + MMax.ToString();
                /*sLog = "Q[i,i] =" + jc.ToString() + " MX = " + M1X[i].ToString() + " SDX = " + SD1X[i].ToString();*/
                //sLog = "" + jc.ToString() + " " + M1X[i].ToString() + " " + SD1X[i].ToString();
                //sLog = "" + SD1X[i.ToString();
                i++;
                LogFile.WriteLog(sLog);
            }
        }

        private void оптимизацияYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fKM = new FormKalmanParms();
            if (fKM.ShowDialog() != DialogResult.OK)
                return;
            var iBeg = fKM.dBeg;
            var iFin = fKM.dFin;
            var iStep = fKM.dStep;
            var iNums = (int) ((iFin - iBeg)/iStep) + 1;
            var M1Y = new double[iNums];
            var SD1Y = new double[iNums];
            var i = 0;
            string sLog;
            sLog = "Y: " + listSourcePC[0].NumFrame.ToString() + "-" +
                   listSourcePC[listSourcePC.Count - 1].NumFrame.ToString();
            LogFile.WriteLog(sLog);
            sLog = "Predict()";
            LogFile.WriteLog(sLog);
            int MMax;
            for (var jc = iBeg; jc < iFin; jc += iStep)
            {
                var KallFilY = new Kalc3Dim("Y", false);
                KallFilY.SetInitParmsQ(jc);
                //private PointsCoordSet CalcKalmanOpt(PointsCoordSet list, string strName,Kalc3Dim KallFil, bool iIsPredict, bool bNoA)
                var listKalmanTemp = CalcKalmanOpt(listSourcePC, sLog, KallFilY, true, false, false);
                //                PointsCoordSet listKalmanTemp = CalcKalmanOpt(listSourcePC, sLog, KallFilX, false, false);
                var iE1Y = CalcCompareParams(listMarkedPC, listKalmanTemp, false, out M1Y[i], out SD1Y[i], out MMax);
                sLog = "Q[i,i] =" + jc.ToString() + " MY = " + M1Y[i].ToString() + " SDY = " + SD1Y[i].ToString() +
                       " Max=" + MMax.ToString();
                i++;
                LogFile.WriteLog(sLog);
            }
        }

        private void выбратьОтображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var FC = new FormSelectCompare();
            FC.Set1Select();
            if (FC.ShowDialog() != DialogResult.OK)
                return;
            bool bRes;
            var iRez = FC.ReturnRes(out bRes);
            if (bRes == false)
                return;
            listKalman1XY = PointsCoordSet.GetListOnIndex(iRez[0]);
            /*for (int i = 0; i < iRez.Length; i++)
                listKalmans[i] = PointsCoordSet.GetListOnIndex(iRez[i]);*/
            bDrawKalmanXY = true;
            //pictureBoxTrack.Refresh();
        }

        private void поискОптимумаXYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var iX = FindOptim(true);
            var iY = FindOptim(false);
            var sLog = "X=" + iX.ToString() + " Y=" + iY.ToString();
        }

        private void graphicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fkd = new FormKalmanDraw();
            fkd.Show();
            fkd.Graph("Marked Estimate", PointsCoordSet.GetListOnName("Marked"), Color.Black);
            //PointsCoordSet.GetListOnIndex(1).RemoveAt(19);
            // PointsCoordSet.GetListOnIndex(1).RemoveAt(19);
            fkd.Graph("Kalman XY", PointsCoordSet.GetListOnName("3D"), Color.Red);
            fkd.ConfigureGraph();
        }
    }
}