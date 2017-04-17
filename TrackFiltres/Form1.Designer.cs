namespace TrackFiltres
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.директорияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.логФайлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отметитьОбъектыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьМаркировкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.фильтрКальманаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запускToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.запускМаркToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.одномерныйToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.predictToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.нетАИVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оптимизацияYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поискОптимумаXYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сравнитьКалмановToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выбратьОтображениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.матрицаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelTrack = new System.Windows.Forms.Panel();
            this.pictureBoxTrack = new System.Windows.Forms.PictureBox();
            this.buttonLeft = new System.Windows.Forms.Button();
            this.buttonright = new System.Windows.Forms.Button();
            this.checkBoxDrawSource = new System.Windows.Forms.CheckBox();
            this.checkBoxDrawMarked = new System.Windows.Forms.CheckBox();
            this.labelNumFrame = new System.Windows.Forms.Label();
            this.checkBoxTracks = new System.Windows.Forms.CheckBox();
            this.checkBoxKalmanXY = new System.Windows.Forms.CheckBox();
            this.buttonBeg = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.panelTrack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.директорияToolStripMenuItem,
            this.файлToolStripMenuItem,
            this.логФайлToolStripMenuItem,
            this.отметитьОбъектыToolStripMenuItem,
            this.сохранитьМаркировкуToolStripMenuItem,
            this.фильтрКальманаToolStripMenuItem,
            this.матрицаToolStripMenuItem,
            this.graphicsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(952, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // директорияToolStripMenuItem
            // 
            this.директорияToolStripMenuItem.Name = "директорияToolStripMenuItem";
            this.директорияToolStripMenuItem.Size = new System.Drawing.Size(85, 20);
            this.директорияToolStripMenuItem.Text = "Директория";
            this.директорияToolStripMenuItem.Click += new System.EventHandler(this.директорияToolStripMenuItem_Click);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(128, 20);
            this.файлToolStripMenuItem.Text = "Файл изображения ";
            this.файлToolStripMenuItem.Click += new System.EventHandler(this.файлToolStripMenuItem_Click);
            // 
            // логФайлToolStripMenuItem
            // 
            this.логФайлToolStripMenuItem.Enabled = false;
            this.логФайлToolStripMenuItem.Name = "логФайлToolStripMenuItem";
            this.логФайлToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.логФайлToolStripMenuItem.Text = "Лог файл";
            this.логФайлToolStripMenuItem.Click += new System.EventHandler(this.логФайлToolStripMenuItem_Click);
            // 
            // отметитьОбъектыToolStripMenuItem
            // 
            this.отметитьОбъектыToolStripMenuItem.Enabled = false;
            this.отметитьОбъектыToolStripMenuItem.Name = "отметитьОбъектыToolStripMenuItem";
            this.отметитьОбъектыToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.отметитьОбъектыToolStripMenuItem.Text = "Отметить объекты";
            this.отметитьОбъектыToolStripMenuItem.Click += new System.EventHandler(this.отметитьОбъектыToolStripMenuItem_Click);
            // 
            // сохранитьМаркировкуToolStripMenuItem
            // 
            this.сохранитьМаркировкуToolStripMenuItem.Enabled = false;
            this.сохранитьМаркировкуToolStripMenuItem.Name = "сохранитьМаркировкуToolStripMenuItem";
            this.сохранитьМаркировкуToolStripMenuItem.Size = new System.Drawing.Size(147, 20);
            this.сохранитьМаркировкуToolStripMenuItem.Text = "Сохранить маркировку";
            this.сохранитьМаркировкуToolStripMenuItem.Click += new System.EventHandler(this.сохранитьМаркировкуToolStripMenuItem_Click);
            // 
            // фильтрКальманаToolStripMenuItem
            // 
            this.фильтрКальманаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.запускToolStripMenuItem,
            this.запускМаркToolStripMenuItem,
            this.одномерныйToolStripMenuItem,
            this.predictToolStripMenuItem,
            this.нетАИVToolStripMenuItem,
            this.optimToolStripMenuItem,
            this.оптимизацияYToolStripMenuItem,
            this.поискОптимумаXYToolStripMenuItem,
            this.сравнитьКалмановToolStripMenuItem,
            this.выбратьОтображениеToolStripMenuItem});
            this.фильтрКальманаToolStripMenuItem.Enabled = false;
            this.фильтрКальманаToolStripMenuItem.Name = "фильтрКальманаToolStripMenuItem";
            this.фильтрКальманаToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.фильтрКальманаToolStripMenuItem.Text = "Фильтр Калмана";
            // 
            // запускToolStripMenuItem
            // 
            this.запускToolStripMenuItem.Enabled = false;
            this.запускToolStripMenuItem.Name = "запускToolStripMenuItem";
            this.запускToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.запускToolStripMenuItem.Text = "Запуск лог";
            this.запускToolStripMenuItem.Click += new System.EventHandler(this.запускToolStripMenuItem_Click);
            // 
            // запускМаркToolStripMenuItem
            // 
            this.запускМаркToolStripMenuItem.Enabled = false;
            this.запускМаркToolStripMenuItem.Name = "запускМаркToolStripMenuItem";
            this.запускМаркToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.запускМаркToolStripMenuItem.Text = "Запуск марк";
            // 
            // одномерныйToolStripMenuItem
            // 
            this.одномерныйToolStripMenuItem.Name = "одномерныйToolStripMenuItem";
            this.одномерныйToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.одномерныйToolStripMenuItem.Text = "Трехмерный";
            this.одномерныйToolStripMenuItem.Click += new System.EventHandler(this.одномерныйToolStripMenuItem_Click);
            // 
            // predictToolStripMenuItem
            // 
            this.predictToolStripMenuItem.Name = "predictToolStripMenuItem";
            this.predictToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.predictToolStripMenuItem.Text = "Predict";
            this.predictToolStripMenuItem.Click += new System.EventHandler(this.predictToolStripMenuItem_Click);
            // 
            // нетАИVToolStripMenuItem
            // 
            this.нетАИVToolStripMenuItem.Name = "нетАИVToolStripMenuItem";
            this.нетАИVToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.нетАИVToolStripMenuItem.Text = "Нет а и v";
            this.нетАИVToolStripMenuItem.Click += new System.EventHandler(this.нетАИVToolStripMenuItem_Click);
            // 
            // optimToolStripMenuItem
            // 
            this.optimToolStripMenuItem.Name = "optimToolStripMenuItem";
            this.optimToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.optimToolStripMenuItem.Text = "Оптимизация X";
            this.optimToolStripMenuItem.Click += new System.EventHandler(this.optimToolStripMenuItem_Click);
            // 
            // оптимизацияYToolStripMenuItem
            // 
            this.оптимизацияYToolStripMenuItem.Name = "оптимизацияYToolStripMenuItem";
            this.оптимизацияYToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.оптимизацияYToolStripMenuItem.Text = "Оптимизация Y";
            this.оптимизацияYToolStripMenuItem.Click += new System.EventHandler(this.оптимизацияYToolStripMenuItem_Click);
            // 
            // поискОптимумаXYToolStripMenuItem
            // 
            this.поискОптимумаXYToolStripMenuItem.Name = "поискОптимумаXYToolStripMenuItem";
            this.поискОптимумаXYToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.поискОптимумаXYToolStripMenuItem.Text = "Поиск оптимума XY";
            this.поискОптимумаXYToolStripMenuItem.Click += new System.EventHandler(this.поискОптимумаXYToolStripMenuItem_Click);
            // 
            // сравнитьКалмановToolStripMenuItem
            // 
            this.сравнитьКалмановToolStripMenuItem.Enabled = false;
            this.сравнитьКалмановToolStripMenuItem.Name = "сравнитьКалмановToolStripMenuItem";
            this.сравнитьКалмановToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.сравнитьКалмановToolStripMenuItem.Text = "Сравнить Калманов";
            this.сравнитьКалмановToolStripMenuItem.Click += new System.EventHandler(this.сравнитьКалмановToolStripMenuItem_Click);
            // 
            // выбратьОтображениеToolStripMenuItem
            // 
            this.выбратьОтображениеToolStripMenuItem.Name = "выбратьОтображениеToolStripMenuItem";
            this.выбратьОтображениеToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.выбратьОтображениеToolStripMenuItem.Text = "Выбрать отображение";
            this.выбратьОтображениеToolStripMenuItem.Click += new System.EventHandler(this.выбратьОтображениеToolStripMenuItem_Click);
            // 
            // матрицаToolStripMenuItem
            // 
            this.матрицаToolStripMenuItem.Enabled = false;
            this.матрицаToolStripMenuItem.Name = "матрицаToolStripMenuItem";
            this.матрицаToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.матрицаToolStripMenuItem.Text = "Матрица";
            this.матрицаToolStripMenuItem.Click += new System.EventHandler(this.матрицаToolStripMenuItem_Click);
            // 
            // graphicsToolStripMenuItem
            // 
            this.graphicsToolStripMenuItem.Name = "graphicsToolStripMenuItem";
            this.graphicsToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.graphicsToolStripMenuItem.Text = "Графика";
            this.graphicsToolStripMenuItem.Click += new System.EventHandler(this.graphicsToolStripMenuItem_Click);
            // 
            // panelTrack
            // 
            this.panelTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelTrack.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelTrack.BackColor = System.Drawing.SystemColors.Control;
            this.panelTrack.Controls.Add(this.pictureBoxTrack);
            this.panelTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panelTrack.Location = new System.Drawing.Point(0, 28);
            this.panelTrack.Name = "panelTrack";
            this.panelTrack.Size = new System.Drawing.Size(952, 437);
            this.panelTrack.TabIndex = 1;
            // 
            // pictureBoxTrack
            // 
            this.pictureBoxTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxTrack.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxTrack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxTrack.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxTrack.Name = "pictureBoxTrack";
            this.pictureBoxTrack.Size = new System.Drawing.Size(946, 431);
            this.pictureBoxTrack.TabIndex = 0;
            this.pictureBoxTrack.TabStop = false;
            this.pictureBoxTrack.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBoxPaint);
            this.pictureBoxTrack.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PictureBoxDoubleClick);
            this.pictureBoxTrack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseDown);
            this.pictureBoxTrack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMouseMov);
            this.pictureBoxTrack.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PictureBoxMoseUp);
            // 
            // buttonLeft
            // 
            this.buttonLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonLeft.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonLeft.Enabled = false;
            this.buttonLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonLeft.Location = new System.Drawing.Point(24, 485);
            this.buttonLeft.Name = "buttonLeft";
            this.buttonLeft.Size = new System.Drawing.Size(101, 48);
            this.buttonLeft.TabIndex = 2;
            this.buttonLeft.Text = "<------";
            this.buttonLeft.UseVisualStyleBackColor = false;
            this.buttonLeft.Click += new System.EventHandler(this.buttonLeft_Click);
            // 
            // buttonright
            // 
            this.buttonright.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonright.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonright.Enabled = false;
            this.buttonright.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.buttonright.Location = new System.Drawing.Point(806, 482);
            this.buttonright.Name = "buttonright";
            this.buttonright.Size = new System.Drawing.Size(97, 48);
            this.buttonright.TabIndex = 3;
            this.buttonright.Text = "----->";
            this.buttonright.UseVisualStyleBackColor = false;
            this.buttonright.Click += new System.EventHandler(this.buttonright_Click);
            // 
            // checkBoxDrawSource
            // 
            this.checkBoxDrawSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDrawSource.AutoSize = true;
            this.checkBoxDrawSource.Location = new System.Drawing.Point(353, 485);
            this.checkBoxDrawSource.Name = "checkBoxDrawSource";
            this.checkBoxDrawSource.Size = new System.Drawing.Size(66, 17);
            this.checkBoxDrawSource.TabIndex = 4;
            this.checkBoxDrawSource.Text = "Из лога";
            this.checkBoxDrawSource.UseVisualStyleBackColor = true;
            this.checkBoxDrawSource.CheckedChanged += new System.EventHandler(this.checkBoxDrawSource_CheckedChanged);
            // 
            // checkBoxDrawMarked
            // 
            this.checkBoxDrawMarked.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxDrawMarked.AutoSize = true;
            this.checkBoxDrawMarked.Location = new System.Drawing.Point(353, 516);
            this.checkBoxDrawMarked.Name = "checkBoxDrawMarked";
            this.checkBoxDrawMarked.Size = new System.Drawing.Size(90, 17);
            this.checkBoxDrawMarked.TabIndex = 5;
            this.checkBoxDrawMarked.Text = "Отмеченные";
            this.checkBoxDrawMarked.UseVisualStyleBackColor = true;
            this.checkBoxDrawMarked.CheckedChanged += new System.EventHandler(this.checkBoxDrawMarked_CheckedChanged);
            // 
            // labelNumFrame
            // 
            this.labelNumFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNumFrame.AutoSize = true;
            this.labelNumFrame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNumFrame.Location = new System.Drawing.Point(595, 502);
            this.labelNumFrame.Name = "labelNumFrame";
            this.labelNumFrame.Size = new System.Drawing.Size(20, 16);
            this.labelNumFrame.TabIndex = 6;
            this.labelNumFrame.Text = "---";
            // 
            // checkBoxTracks
            // 
            this.checkBoxTracks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxTracks.AutoSize = true;
            this.checkBoxTracks.Location = new System.Drawing.Point(486, 516);
            this.checkBoxTracks.Name = "checkBoxTracks";
            this.checkBoxTracks.Size = new System.Drawing.Size(57, 17);
            this.checkBoxTracks.TabIndex = 7;
            this.checkBoxTracks.Text = "Треки";
            this.checkBoxTracks.UseVisualStyleBackColor = true;
            this.checkBoxTracks.CheckedChanged += new System.EventHandler(this.checkBoxTracks_CheckedChanged);
            // 
            // checkBoxKalmanXY
            // 
            this.checkBoxKalmanXY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxKalmanXY.AutoSize = true;
            this.checkBoxKalmanXY.Location = new System.Drawing.Point(478, 485);
            this.checkBoxKalmanXY.Name = "checkBoxKalmanXY";
            this.checkBoxKalmanXY.Size = new System.Drawing.Size(65, 17);
            this.checkBoxKalmanXY.TabIndex = 10;
            this.checkBoxKalmanXY.Text = "Калман";
            this.checkBoxKalmanXY.UseVisualStyleBackColor = true;
            this.checkBoxKalmanXY.CheckedChanged += new System.EventHandler(this.checkBoxKalmanXY_CheckedChanged);
            // 
            // buttonBeg
            // 
            this.buttonBeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBeg.Location = new System.Drawing.Point(160, 491);
            this.buttonBeg.Name = "buttonBeg";
            this.buttonBeg.Size = new System.Drawing.Size(112, 39);
            this.buttonBeg.TabIndex = 11;
            this.buttonBeg.Text = "В начало";
            this.buttonBeg.UseVisualStyleBackColor = true;
            this.buttonBeg.Click += new System.EventHandler(this.buttonBeg_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnd.Location = new System.Drawing.Point(684, 482);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(91, 48);
            this.buttonEnd.TabIndex = 12;
            this.buttonEnd.Text = "В конец";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(952, 542);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.buttonBeg);
            this.Controls.Add(this.checkBoxKalmanXY);
            this.Controls.Add(this.checkBoxTracks);
            this.Controls.Add(this.labelNumFrame);
            this.Controls.Add(this.checkBoxDrawMarked);
            this.Controls.Add(this.checkBoxDrawSource);
            this.Controls.Add(this.buttonright);
            this.Controls.Add(this.buttonLeft);
            this.Controls.Add(this.panelTrack);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Трекинг";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelTrack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem директорияToolStripMenuItem;
        private System.Windows.Forms.Panel panelTrack;
        private System.Windows.Forms.PictureBox pictureBoxTrack;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem логФайлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отметитьОбъектыToolStripMenuItem;
        private System.Windows.Forms.Button buttonLeft;
        private System.Windows.Forms.Button buttonright;
        private System.Windows.Forms.CheckBox checkBoxDrawSource;
        private System.Windows.Forms.CheckBox checkBoxDrawMarked;
        private System.Windows.Forms.ToolStripMenuItem сохранитьМаркировкуToolStripMenuItem;
        private System.Windows.Forms.Label labelNumFrame;
        private System.Windows.Forms.CheckBox checkBoxTracks;
        private System.Windows.Forms.ToolStripMenuItem фильтрКальманаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запускToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem запускМаркToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem одномерныйToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxKalmanXY;
        private System.Windows.Forms.ToolStripMenuItem матрицаToolStripMenuItem;
        private System.Windows.Forms.Button buttonBeg;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.ToolStripMenuItem сравнитьКалмановToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem predictToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem нетАИVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optimToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выбратьОтображениеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оптимизацияYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поискОптимумаXYToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem graphicsToolStripMenuItem;
    }
}

