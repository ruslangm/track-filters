using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;

namespace TrackFiltres
{
    class KalmanMatrixes
    {
  
        public static MatrixS GetMatrixA(double radiusSign, double focalLength, double vehicleSpeed, double deltaT,
            double radiusSignXProectionEstimate, double radiusSignYProectionEstimate)
        {
            String firstElement = (focalLength*radiusSign/
                                   (focalLength*radiusSign - vehicleSpeed*deltaT*radiusSignXProectionEstimate)).ToString();
            String secondElement = (focalLength*radiusSign/
                                    (focalLength*radiusSign - vehicleSpeed*deltaT*radiusSignYProectionEstimate)).ToString();
            String thirdElement = (-(vehicleSpeed*deltaT/(focalLength*radiusSign))).ToString();

            return MatrixS.Parse(firstElement + " 0 0 0 0\r\n0 " + secondElement + " 0 0 0\r\n0 0 1 0 " + thirdElement +
                              "\r\n0 0 0 1 " + thirdElement + "\r\n0 0 0 0 1");
        }

        public static double getRadiusProjectionInCurrentTime(double radiusSign, double focalLength, double distantion)
        {
            return radiusSign*focalLength/distantion;
        }

        private static double getXInCurrentTime(double distantionX, double focalLength, double distantionY)
        {
            return distantionX * focalLength / distantionY;
        }

        public static MatrixS GetMatrixD(double distantionX, double focalLength, double distantionY,
            double radiusSignXProectionEstimate, double radiusSignYProectionEstimate)
        {
            double x = getXInCurrentTime(distantionX, focalLength, distantionY);
            double y = focalLength;

            return MatrixS.Parse(x + "\r\n" + y + "\r\n" + 1/radiusSignXProectionEstimate + "\r\n" + 
                1/radiusSignYProectionEstimate + "\r\n" + "1\r\n");
        }

    }
}