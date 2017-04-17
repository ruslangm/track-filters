using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TrackFiltres
{
    public partial class FormKalmanDraw : Form
    {
        private int a;

        public FormKalmanDraw()
        {
            InitializeComponent();
        }

        public void ConfigureGraph()
        {
        }

        public void Graph(string name, List<PointCoords> values, Color clr)
        {
            var s = new Series(name);
            s.ChartType = SeriesChartType.Line;

            for (int i = 0; i < values.Count; i++)
            {
                if (name.Equals("Marked Estimate")) s.MarkerStyle = MarkerStyle.Circle;
                else s.MarkerStyle = MarkerStyle.Square;
                s.Points.Add(new DataPoint(values[i].X, values[i].Y));
            }

            s.Color = clr;
            chart1.Series.Add(s);

            chart1.ChartAreas[0].AxisX.Title = "X";
            chart1.ChartAreas[0].AxisY.Title = "Y";
        }

        private void FormKalmanDraw_SizeChanged(object sender, EventArgs e)
        {
        }

        private void FormKalmanDraw_Load_1(object sender, EventArgs e)
        {
            chart1.Series.Clear();
            chart1.ChartAreas[0].RecalculateAxesScale();
            TopMost = true;
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
        }
    }
}