using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrackFiltres
{
    public partial class FormSelectCompare : Form
    {
        bool bIs1 = false;
        public FormSelectCompare()
        {
            InitializeComponent();
            FillControls();
        }
        public void Set1Select()
        {
            listBoxSelExper.SelectionMode = SelectionMode.One;
            this.label1.Text = "";
            label1.Enabled = false;
            comboBoxEtalon.Enabled = false;
            bIs1 = true;
        }
        public bool FillControls()
        {
            int iMax = PointsCoordSet.ListPointsSet.Count;
            if (iMax <= 0)
                return false;
            for (int jc = 0; jc < iMax; jc++)
            {
                string str = PointsCoordSet.ListPointsSet[jc].strName;
                comboBoxEtalon.Items.Add(str);
                listBoxSelExper.Items.Add(str);

            }
            return true;
        }
        public int[] ReturnRes(out bool bres)
        {
            bres = true;
            int[] aIndexes;

            if (bIs1)
            {
                aIndexes = new int[1];
                //Получает коллекцию, содержащую индексы всех
                //выделенных в настоящий момент позиций в элементе управления ListBox 
                //(индексы, начинающиеся с нуля).
                aIndexes[0] = listBoxSelExper.SelectedIndices[0];
                if (aIndexes[0] < 0)
                {
                    bres = false;
                    return aIndexes;
                }
                return aIndexes;
            }
            aIndexes = new int[3];
            //            aIndexes[0] = comboBoxEtalon.SelectedIndex;

           /* for (int i = 0; i < listBoxSelExper.Items.Count; i++)
                aIndexes[i] = (int)listBoxSelExper.Items[i]; */

            /*aIndexes[0] = (int) listBoxSelExper.Items[0];
            aIndexes[1] = listBoxSelExper.SelectedIndices[0];
            aIndexes[2] = listBoxSelExper.SelectedIndices[1];
            if ((aIndexes[0] < 0) || (aIndexes[1] < 0) || (aIndexes[2] < 0))
                bres = false;*/
            return aIndexes;
        }


    }
}