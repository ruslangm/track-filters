using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFiltres
{
    class PointsCoordSet : List<PointCoords>
    {
        public static List<PointsCoordSet> ListPointsSet;

        public PointsCoordSet(string Name)
        {
            strName = Name;
            if (PointsCoordSet.ListPointsSet == null)
            {
                PointsCoordSet.ListPointsSet = new List<PointsCoordSet>();
            }
        }

        public string strName;

        public static int GetIndex(string Name)
        {
            int iMax = PointsCoordSet.ListPointsSet.Count;
            for (int jc = 0; jc < iMax; jc++)
            {
                if (PointsCoordSet.ListPointsSet[jc].strName == Name)
                {
                    return jc;
                }
            }
            return -1;
        }

        public static void DeletePointsList(string Name)
        {
            int index = PointsCoordSet.GetIndex(Name);
            if (index == -1)
                return;
            PointsCoordSet.ListPointsSet.RemoveAt(index);
        }

        public static PointsCoordSet GetListOnName(string Name)
        {
            int index = PointsCoordSet.GetIndex(Name);

            if (index == -1)
                return null;
            return PointsCoordSet.ListPointsSet[index];
        }

        public static PointsCoordSet GetListOnIndex(int index)
        {
            if (index == -1)
                return null;
            return PointsCoordSet.ListPointsSet[index];
        }

        public static void AddList(PointsCoordSet list)
        {
            PointsCoordSet.DeletePointsList(list.strName);
            PointsCoordSet.ListPointsSet.Add(list);
        }
    }
}