using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TrackFiltres
{
    public class KalmanFilter
    {
        //F
        private Matrix2x2 MatrixTrans = new Matrix2x2(1, 0, 0, 1);
        //Q
        private Matrix2x2 MatrixNoiseProcess = new Matrix2x2(0.000, 0, 0, 0.000);
        //private Matrix2x2 MatrixNoiseProcess = new Matrix2x2(0.1, 0, 0, 0.1);
        //H
        private Matrix2x2 MatrixMeasure = new Matrix2x2(1, 0, 0, 1);
        //R
        //private Matrix2x2 MatrixMeasureNoise = new Matrix2x2(50, 0, 0, 50);
        private Matrix2x2 MatrixMeasureNoise = new Matrix2x2(2, 0, 0, 2);
        //P
        //private Matrix2x2 MatrixError = new Matrix2x2(1, 0, 0, 1);
        private Matrix2x2 MatrixError = new Matrix2x2(0.1, 0, 0, 0.1);
        //I
        private Matrix2x2 MatrixIdent = new Matrix2x2(1, 0, 0, 1);
        
        //Measure
        double[] xEsimate = new double[2] { 0, 0 };
        double[] xEsimateSave = new double[2] { 0, 0 };
        //double z;
        private bool bIsFirst = true;
        public KalmanFilter()
        {
        
        }
        /// <summary>
        ///  расстояние[0] ... угол[1]
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public double[] CalcEstimation(double[] vec)
        {
            if (bIsFirst == true)
            {
                xEsimateSave[0] = vec[0];
                xEsimateSave[1] = vec[1];
                bIsFirst = false;
            }
            if (vec[0] == -1)
            {
                vec[0] = xEsimateSave[0];
                vec[1] = xEsimateSave[1];
            }
            xEsimate = MatrixTrans.MulVector(xEsimateSave);
            // F*p*F' + Q
            Matrix2x2 mTrans1 = Matrix2x2.Multiply(MatrixTrans, MatrixError);
            Matrix2x2 mTrans2 = MatrixTrans.Transpone();
            Matrix2x2 mTrans3 = Matrix2x2.Multiply(mTrans1, mTrans2);
            MatrixError = Matrix2x2.Sum(mTrans3, MatrixNoiseProcess);
            ////Correction K = P*H'(H*P*H' + R)^-1
            mTrans2 = MatrixMeasure.Transpone(); // H'
            mTrans1 = Matrix2x2.Multiply(MatrixError, mTrans2); //P*H'
            mTrans3 = Matrix2x2.Multiply(MatrixMeasure, mTrans1);  //H*P*H'
            mTrans3 = Matrix2x2.Sum(mTrans3,MatrixIdent); //H*P*H' + R
            mTrans3 = mTrans3.Inverse();
            if (mTrans3 == null)
                return vec;
            Matrix2x2 mCorr = Matrix2x2.Multiply(mTrans1, mTrans3);//K
            ///x = x +K*(z-H*x);
            double[] transVector = MatrixMeasure.MulVector(xEsimate); //H*x
            transVector = Matrix2x2.SubVectors(vec, transVector);
            transVector = mCorr.MulVector(transVector);//K*(z-H*x)
            xEsimateSave = Matrix2x2.SumVectors(transVector, xEsimate);
            //P = (I -K * H)*P;
            mTrans1 = Matrix2x2.Multiply(mCorr, MatrixMeasure); //K * H
            mTrans2 = Matrix2x2.Sub(MatrixIdent, mTrans1);//I -K * H
            MatrixError = Matrix2x2.Multiply(mTrans2, MatrixError);
            return xEsimate;
        }
        /// <summary>
        ///  расстояние[0] ... угол[1]
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        const int dim = 2;
        //F
        double[] vError = new double[dim] { 1, 1 };
        double[] vTrans = new double[dim] { 1, 1 };
        double[] vNoiseProcess = new double[dim] { 0.01, 0.01 };
        double[] vMeasure = new double[dim] { 1, 1 };
        //double[] vE = new double[dim] { 1, 1 };
        double[] vE = new double[dim] { 0.01, 0.01 };
        double[] xEsimateV = new double[2] { 0, 0 };
        double[] xEsimateSaveV = new double[2] { 0, 0 };

        public Point CalcEstimationPoint(Point pt,int Num)
        {
            double[] inD = new double[2];
            inD[0] = pt.X;
            inD[1] = pt.Y;
            double[] outD = CalcEstimationScalar(inD,Num);
            Point ptOut = new Point((int)outD[0], (int)outD[1]);
            return ptOut;
        }
        void FormStringForLog(string str, double X, double Y)
        {
            string strLog = str + " X= " + X.ToString() + " Y= " + Y.ToString();
            LogFile.WriteLog(strLog);
        }
        private void Predict()
        {
                        xEsimateV = Matrix2x2.MulVector2(vTrans, xEsimateSaveV);
                        // F*p*F' + Q
                        double[] mTrans1 = Matrix2x2.MulVector2(vTrans, vError);
                        //Matrix2x2 mTrans2 = MatrixTrans.Transpone();
                        double[] mTrans2 = Matrix2x2.MulVector2(mTrans1, vTrans);
                        vError = Matrix2x2.SumVectors(mTrans2, vNoiseProcess);

        }

        double[] Update(double[] vec)
        {
            ////Correction K = P*H'(H*P*H' + R)^-1
            //mTrans2 = MatrixMeasure.Transpone(); // H'
            double[] mTrans3 = Matrix2x2.MulVector2(vError, vMeasure); //P*H'
            double[] mTrans4 = Matrix2x2.MulVector2(vMeasure, mTrans3);  //H*P*H'
            double[] mTrans5 = Matrix2x2.SumVectors(mTrans4, vE); //H*P*H' + R
            double[] mTrans6 = Matrix2x2.InverseVec(mTrans5);
            if (mTrans6 == null)
                return vec;
            double[] mTrans7 = Matrix2x2.MulVector2(vTrans, vError);
            double[] mCorr = Matrix2x2.MulVector2(mTrans7, mTrans6);//K
            ///x = x +K*(z-H*x);
            double[] transVector1 = Matrix2x2.MulVector2(vMeasure,xEsimateV); //H*x
            double[] transVector2 = Matrix2x2.SubVectors(vec, transVector1);
            double[] transVector3 = Matrix2x2.MulVector2(mCorr, transVector2);//K*(z-H*x)
            xEsimateSaveV = Matrix2x2.SumVectors(transVector3, xEsimateV);
            //P = (I -K * H)*P;
            double[] mTrans8 = Matrix2x2.MulVector2(mCorr, vMeasure); //K * H
            double[] mTrans9 = Matrix2x2.SubVectors(vE, mTrans8);//I -K * H
            vError = Matrix2x2.MulVector2(mTrans9, vError);
         /*
            FormStringForLog("vec ", vec[0], vec[1]);
            FormStringForLog("xEsimateV ", xEsimateV[0], xEsimateV[1]);
            FormStringForLog("xEsimateSaveV ", xEsimateSaveV[0], xEsimateSaveV[1]);
            FormStringForLog("Del xEsimateV ", Math.Abs(xEsimateV[0] - vec[0]), Math.Abs(xEsimateV[1] - vec[1]));
            FormStringForLog("Del xEsimateSaveV ", Math.Abs(xEsimateSaveV[0] - vec[0]), Math.Abs(xEsimateSaveV[0] - vec[0]));        
           
          */
            return xEsimateV;
        }
        
        public double[] CalcEstimationScalar(double[] vec, int Num)
        {
            //LogFile.WriteLog("-------------------------CalcEstimationScalar=" + Num.ToString());
            if (bIsFirst == true)
            {
                xEsimateSaveV[0] = vec[0];
                xEsimateSaveV[1] = vec[1];
                bIsFirst = false;
            }
            if (vec[0] == -1)
            {
                //vec[0] = xEsimateSaveV[0];
                //vec[1] = xEsimateSaveV[1];
                Predict();
                vec[0] = xEsimateV[0];
                vec[1] = xEsimateV[1];
            }

            
            Predict();


            return Update(vec);
            //return xEsimateSaveV;
        }
    }
}
