using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace TrackFiltres
{
    class LogFile
    {
        static string spath = "";

        public static void WriteLog(string sLog)
        {
            if (spath.Length == 0)
            {
                spath = Directory.GetCurrentDirectory();
                spath += "\\" + "" + DateTime.Now.TimeOfDay.TotalSeconds.ToString() + ".log";
            }
            using (StreamWriter sw = new StreamWriter(spath, true))
            {
                sw.WriteLine(sLog);
                sw.Close();
            }
        }
    }
}