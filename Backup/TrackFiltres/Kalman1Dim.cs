using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFiltres
{
    class Kalman1Dim
    {
        public double X0; // predicted state
        public double P0; // predicted covariance
        public double F; // factor of real value to previous real value
        public double Q; // measurement noise
        public double H; // factor of measured value to real value
        public double R; // environment noise
        public double State;
        public double Covariance;
        private string sName = "";

        public Kalman1Dim(string str)
        {
            sName = str;
            Q = 0;
            R = 0.1;
            F = 1;
            H = 1;
            X0 = 1;
            P0 = 1;
            State = 1;
            Covariance = 0.1;

        }
        public double CalcEstimation1Dim(double data)
        {
        
        State = data;
        //time update prediction
        X0 = F*State;
        P0 = F*Covariance*F + Q;
        //measurement update correction
        double K = H*P0/(H*P0*H + R);
        State = X0 + K*(data -  H*X0);
        Covariance = (1 - K * H) * P0;
        return State;
        }
        void FormStringForLog(string str, double X)
        {
            string strLog = sName + ": " + str + "  = " + X.ToString();
            LogFile.WriteLog(strLog);
        }
    }
}
