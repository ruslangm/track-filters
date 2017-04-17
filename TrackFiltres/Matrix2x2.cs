using System;
using System.Collections.Generic;
using System.Text;

namespace TrackFiltres
{
    /// <summary>
    /// 0,0;0,1
    /// 1,0;1,1
    /// </summary>
    class Matrix2x2
    {
        public const int dim = 2;
        private double[,] M2x2 = new double[dim, dim] {{0, 0}, {0, 0}};

        public Matrix2x2(double x00, double x01, double x10, double x11)
        {
            M2x2[0, 0] = x00;
            M2x2[0, 1] = x01;
            M2x2[1, 0] = x10;
            M2x2[1, 1] = x11;
        }

        public Matrix2x2()
        {
        }

        public double this[int index1, int index2]
        {
            get
            {
                if ((index1 >= dim) || (index2 >= dim))
                    return 0;
                return M2x2[index1, index2];
            }
            set
            {
                if ((index1 >= Matrix2x2.dim) || (index2 >= Matrix2x2.dim))
                    return;
                M2x2[index1, index2] = value;
            }
        }

        public Matrix2x2 Transpone()
        {
            Matrix2x2 mt2x2 = new Matrix2x2();
            for (int jcx = 0; jcx < dim; jcx++)
                for (int jcy = 0; jcy < dim; jcy++)
                    mt2x2[jcx, jcy] = M2x2[jcy, jcx];
            return mt2x2;
        }

        public static Matrix2x2 Sum(Matrix2x2 m12x2, Matrix2x2 m22x2)
        {
            Matrix2x2 mm2x2 = new Matrix2x2();
            for (int jcx = 0; jcx < dim; jcx++)
                for (int jcy = 0; jcy < dim; jcy++)
                    mm2x2[jcx, jcy] = m12x2[jcx, jcy] + m22x2[jcx, jcy];
            return mm2x2;
        }

        public static Matrix2x2 Sub(Matrix2x2 m12x2, Matrix2x2 m22x2)
        {
            Matrix2x2 mm2x2 = new Matrix2x2();
            for (int jcx = 0; jcx < dim; jcx++)
                for (int jcy = 0; jcy < dim; jcy++)
                    mm2x2[jcx, jcy] = m12x2[jcx, jcy] - m22x2[jcx, jcy];
            return mm2x2;
        }

        public double[] MulVector(double[] v22)
        {
            double[] vec = new double[dim] {0, 0};
            for (int jcx = 0; jcx < dim; jcx++)
                for (int jcy = 0; jcy < dim; jcy++)
                    vec[jcy] += M2x2[jcx, jcy]*v22[jcy];
            return vec;
        }

        public static Matrix2x2 Multiply(Matrix2x2 m12x2, Matrix2x2 m22x2)
        {
            Matrix2x2 mm2x2 = new Matrix2x2();
            for (int jcx = 0; jcx < dim; jcx++)
            {
                for (int jcy = 0; jcy < Matrix2x2.dim; jcy++)
                {
                    mm2x2[jcx, jcy] = 0;
                    for (int jcr = 0; jcr < dim; jcr++)
                    {
                        mm2x2[jcx, jcy] += m12x2[jcx, jcr]*m22x2[jcr, jcy];
                    }
                }
            }
            return mm2x2;
        }

        public static double[] SubVectors(double[] vec1, double[] vec2)
        {
            double[] vec = new double[dim];
            for (int jcx = 0; jcx < dim; jcx++)
            {
                vec[jcx] = vec1[jcx] - vec2[jcx];
            }
            return vec;
        }

        public static double[] SumVectors(double[] vec1, double[] vec2)
        {
            double[] vec = new double[dim];
            for (int jcx = 0; jcx < dim; jcx++)
            {
                vec[jcx] = vec1[jcx] + vec2[jcx];
            }
            return vec;
        }

        public Matrix2x2 Inverse()
        {
            double det = M2x2[0, 0]*M2x2[1, 1] - M2x2[0, 1]*M2x2[1, 0];
            if (det == 0)
                return null;
            double det1 = 1/det;
            Matrix2x2 m2x2 = new Matrix2x2();
            m2x2[0, 0] = M2x2[1, 1]*det1;
            m2x2[0, 1] = -(M2x2[0, 1]*det1);
            m2x2[1, 0] = -(M2x2[1, 0]*det1);
            m2x2[1, 1] = M2x2[0, 0]*det1;
            return m2x2;
        }

        public static double[] MulVector2(double[] vec1, double[] vec2)
        {
            double[] vec = new double[dim];
            for (int jc = 0; jc < dim; jc++)
                vec[jc] = vec1[jc]*vec2[jc];
            return vec;
        }

        public static double[] InverseVec(double[] vec)
        {
            for (int jc = 0; jc < dim; jc++)
                if (vec[jc] == 0)
                    return null;
            double[] vecres = new double[dim];
            for (int jc = 0; jc < dim; jc++)
                vecres[jc] = 1/vec[jc];
            return vecres;
        }
    }
}