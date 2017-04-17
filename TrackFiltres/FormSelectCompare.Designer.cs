namespace TrackFiltres
{
    partial class FormSelectCompare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSelectCompare));
            this.listBoxSelExper = new System.Windows.Forms.ListBox();
            this.comboBoxEtalon = new System.Windows.Forms.ComboBox();
            this.lablecomare = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxSelExper
            // 
            this.listBoxSelExper.FormattingEnabled = true;
            this.listBoxSelExper.Location = new System.Drawing.Point(12, 33);
            this.listBoxSelExper.Name = "listBoxSelExper";
            this.listBoxSelExper.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxSelExper.Size = new System.Drawing.Size(356, 199);
            this.listBoxSelExper.TabIndex = 0;
            // 
            // comboBoxEtalon
            // 
            this.comboBoxEtalon.FormattingEnabled = true;
            this.comboBoxEtalon.Location = new System.Drawing.Point(443, 33);
            this.comboBoxEtalon.Name = "comboBoxEtalon";
            this.comboBoxEtalon.Size = new System.Drawing.Size(193, 21);
            this.comboBoxEtalon.TabIndex = 1;
            // 
            // lablecomare
            // 
            this.lablecomare.AutoSize = true;
            this.lablecomare.Location = new System.Drawing.Point(12, 14);
            this.lablecomare.Name = "lablecomare";
            this.lablecomare.Size = new System.Drawing.Size(125, 13);
            this.lablecomare.TabIndex = 2;
            this.lablecomare.Text = "Сравнение - не более 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(443, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Эталон";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(446, 101);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(560, 101);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormSelectCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 272);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lablecomare);
            this.Controls.Add(this.comboBoxEtalon);
            this.Controls.Add(this.listBoxSelExper);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSelectCompare";
            this.Text = "Выбрать для сравнения";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxSelExper;
        private System.Windows.Forms.ComboBox comboBoxEtalon;
        private System.Windows.Forms.Label lablecomare;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}