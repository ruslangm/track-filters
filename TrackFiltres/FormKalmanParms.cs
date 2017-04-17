using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TrackFiltres
{
    public partial class FormKalmanParms : Form
    {
        public FormKalmanParms()
        {
            InitializeComponent();
        }
        public double dBeg;
        public double dFin;
        public double dStep;
        private void buttonOk_Click(object sender, EventArgs e)
        {
            dBeg = FromStringToDouble(textBoxBeg.Text);
            dFin = FromStringToDouble(textBoxFin.Text);
            dStep = FromStringToDouble(textBoxStep.Text); 
        }

        double FromStringToDouble(string strD)
        {
            string str = strD.Replace('.', ',');
            double dbl = Convert.ToDouble(str);
            return dbl;
        }
    }
}