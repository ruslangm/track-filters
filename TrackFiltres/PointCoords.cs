using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TrackFiltres
{
    public class PointCoords
    {
        public int NumFrame;
        public int X;
        public int Y;
        public int Width;
        public string Class;
        public Color col;

        /// NumFrame posX posY Width Class
        public PointCoords()
        {
            Class = "";
            Width = 20;
            col = Color.Green;
            X = -1;
            Y = -1;
        }


        public bool SetDataFromFile(string strFile, Color color)
        {
            string[] split = strFile.Split(new Char[] {' '});
            if (split.Length != 5)
                return false;
            if (Int32.TryParse(split[0], out NumFrame) == false)
                return false;
            if (Int32.TryParse(split[1], out X) == false)
                return false;
            if (Int32.TryParse(split[2], out Y) == false)
                return false;
            if (Int32.TryParse(split[3], out Width) == false)
                return false;
            Class = split[4];
            col = color;
            return true;
        }

        public static PointCoords operator +(Point pt, PointCoords ptc)
        {
            PointCoords ptcn = new PointCoords();
            ptcn.Class = ptc.Class;
            ptcn.Width = ptc.Width;
            ptcn.NumFrame = ptc.NumFrame;
            ptcn.X = pt.X;
            ptcn.Y = pt.Y;
            return ptcn;
        }

        public static PointCoords operator +(PointCoords ptc, Point pt)
        {
            PointCoords ptcn = pt + ptc;
            return ptcn;
        }
    }
}