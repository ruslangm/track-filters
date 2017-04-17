using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFiltres
{
    
    class Kalc3Dim
    {
    //A она же F
        private MatrixS F; // factor of real value to previous real value матрица перехода между состояниями
        private MatrixS X0; // predicted state предсказание в текущий момент времени
        private MatrixS X; // Измерение
        private MatrixS D; // Вход
        private MatrixS P; // predicted covariance предсказание ощибки ковариации
        private MatrixS Q; // measurement noise  ковариация шума процесса
        private MatrixS H; // factor of measured value to real value //отношение измерений и состояний
        private MatrixS R; // environment noise  //ковариация шума измерения
        private MatrixS I;  // I matrix
        //private MatrixS VData; // В гости к нам приходит
        //private MatrixS Covariance;
        private string sName = "";
        private int iNum;
        int iIndex = 0;
        // v и a
        private MatrixS dat;
        /*
        State = data;
        //time update prediction
        X0 = F*State;
        P0 = F*Covariance*F + Q;
        //measurement update correction
        double K = H*P0/(H*P0*H + R);
        State = X0 + K*(data -  H*X0);
        Covariance = (1 - K * H) * P0;
        return State;
        */
        public Kalc3Dim(string str)
        {
            sName = str;
            I = MatrixS.Parse("1 0 0\r\n0 1 0\r\n0 0 1");
            //Q = MatrixS.Parse("0,01 0 0\r\n0 0,01 0\r\n0 0 0,01");
            //Q = MatrixS.Parse("0,5 0 0\r\n0 0,5 0\r\n0 0 0,5");
            //Q = MatrixS.Parse("4400 0 0\r\n0 4400 0\r\n0 0 4400"); // measurement noise  ковариация шума процесса
            
            Q = MatrixS.Parse("16 0 0\r\n0 16 0\r\n0 0 16"); // measurement noise  ковариация шума процесса
            R = MatrixS.Parse("10000 0 0\r\n0 10000 0\r\n0 0 10000");//ковариация шума измерения
            //R = MatrixS.Parse("0,5 0 0\r\n0 0,5 0\r\n0 0 0,5");
            F = MatrixS.Parse("1 1 0.5\r\n0 1 1\r\n0 0 1");
            H = MatrixS.Parse("1 0 0\r\n0 1 0\r\n0 0 1"); // factor of measured value to real value //отношение измерений и состояний
            X0 = MatrixS.Parse("1\r\n1\r\n1"); //Предсказание состояния системы
            D = MatrixS.Parse("1\r\n0\r\n0"); //полученные данные
            X = MatrixS.Parse("1\r\n0\r\n0"); //Уточненные данные
            P = MatrixS.Parse("0,5 0 0\r\n0 0,5 0\r\n0 0 0,5"); //Предсказание ошибки ковариации
            dat = MatrixS.Parse("0\r\n0\r\n0");
            // VData = MatrixS.Parse("1\r\n1\r\n1");
           // State = MatrixS.Parse("1\r\n1\r\n1");
            //Covariance = MatrixS.Parse("0.1 0 0\r\n0 0.1 0\r\n0 0 0.1");
            

        }
        private void Predict()
        {
            //X = F * X;
            //P = F * Covariance * F + Q;
            if (iIndex++ == 0)
                X[0, 0] = D[0, 0];
            X0 = F*X;
            MatrixS FPFt = F*P;
            MatrixS Ft = MatrixS.Transpose(F);
            P = FPFt*Ft +Q;


        }
        private void Update()
        {
        //K = H*P/(H*P*Ht + R);
            MatrixS Ht = MatrixS.Transpose(H);
            MatrixS HP = H * P;
            MatrixS HPHt =  HP * Ht;
            MatrixS HPHtR = HPHt + R;
            MatrixS HRInv = HPHtR.Invert();
            //Kalman Gain
            //measurement update correction
            //var K = H*P0/(H*P0*H + R);
            //State = X0 + K*(data - H*X0);
            //Covariance = (1 - K*H)*P0;
            MatrixS K = HP * HRInv;
            MatrixS HX0 = H*X0;
            MatrixS DHX0 = D - HX0;
            MatrixS KDHX0 = K * DHX0;
            X = X0 + KDHX0;
            MatrixS KH = K * H;
            MatrixS IMinus = I - KH;
              P = IMinus * P;


        }
        public void SetInitParmsQ(double dParm)
        {
            int iCols = Q.cols;
            for (int jc = 0; jc < iCols; jc++)
                Q[jc, jc] = dParm;
        }

        public void SetInitParmsR(double dParm)
        {
            int iCols = R.cols;
            for (int jc = 0; jc < iCols; jc++)
                R[jc, jc] = dParm;
        }
        public void SetInitParmsH(double dParm)
        {
            int iCols = H.cols;
            for (int jc = 0; jc < iCols; jc++)
                H[jc, jc] = dParm;
        }
        public double SetData(double dData, int iCurNum, bool bIsPredict,bool bNoA)
        {
            iNum = iCurNum;

            D[0, 0] = dData;
            D[1, 0] = 0;
            D[2, 0] = 0;
 
            if (D[0, 0] == -1)
            {
                if (bIsPredict == false)
                    D[0, 0] = X[0, 0] + X[1, 0] + 0.5 * X[2, 0];
                else
                {
                    Predict();
                    D[0, 0] = X0[0, 0];
                }
            }
            dat[2, 0] = dat[1, 0];
            dat[1, 0] = dat[0, 0];
            dat[0, 0] = D[0, 0];
            if (bNoA == false)
            {
                if (iIndex >= 2)
                {
                    D[1, 0] = dat[0, 0] - dat[1, 0];
                    D[2, 0] = dat[2, 0] - 2 * dat[1, 0] + dat[0, 0];
                }
                else
                {
                    D[1, 0] = 0;
                    D[2, 0] = 0;
                }
            }
            Predict();
            Update();
           // FormStringForLog("Update", X[0, 0], X[1, 0], X[2, 0]);
            return X[0, 0];

        }
        void FormStringForLog(string str, double C, double V, double A)
        {
            string strLog = sName +"-------" + iNum.ToString() + ": " + str + " C= " + C.ToString() + " V= " + V.ToString() + " A= " + A.ToString();
            LogFile.WriteLog(strLog);
        }
    }
}
