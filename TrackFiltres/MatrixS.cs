﻿/*
    Matrix class in C#
    Written by Ivan Kuckir (ivan.kuckir@gmail.com, http://blog.ivank.net)
    Faculty of Mathematics and Physics
    Charles University in Prague
    (C) 2010
    - updated on 1. 6.2014 - Trimming the string before parsing
    - updated on 14.6.2012 - parsing improved. Thanks to Andy!
    - updated on 3.10.2012 - there was a terrible bug in LU, SoLE and Inversion. Thanks to Danilo Neves Cruz for reporting that!
// solving system of linear equations ... A * x = b
MatrixS A = MatrixS.Parse("1 2\r\n-3 4.5"); // \r\n is newline in textbox
MatrixS b = MatrixS.Parse("-1\r\n-3);
MatrixS x = A.SolveWith(b);

// LU decomposition (P is permutation matrix)
Matrix PLU = A.GetP() * A.L * A.U;   // -> PLU == A
double d = A.Det();
Matrix I = A.Invert();
Matrix Powered = Matrix.Power(A, 10);
Matrix S = A.Duplicate();
Matrix Z = Matrix.ZeroMatrix(4,3);
Matrix I = Matrix.IdentityMatrix(6,7);
Matrix R = Matrix.RandomMatrix(4, 4, 80); // rows, cols, dispersion


 */

using System;
using System.Text.RegularExpressions;

namespace TrackFiltres
{
    public class MatrixS
    {
        public int rows;
        public int cols;
        public double[,] mat;

        public MatrixS L;
        public MatrixS U;
        private int[] pi;
        private double detOfP = 1;

        public MatrixS(int iRows, int iCols) // Matrix Class constructor
        {
            rows = iRows;
            cols = iCols;
            mat = new double[rows, cols];
        }

        public Boolean IsSquare()
        {
            return (rows == cols);
        }

        public double this[int iRow, int iCol] // Access this matrix as a 2D array
        {
            get { return mat[iRow, iCol]; }
            set { mat[iRow, iCol] = value; }
        }

        public MatrixS GetCol(int k)
        {
            MatrixS m = new MatrixS(rows, 1);
            for (int i = 0; i < rows; i++) m[i, 0] = mat[i, k];
            return m;
        }

        public void SetCol(MatrixS v, int k)
        {
            for (int i = 0; i < rows; i++) mat[i, k] = v[i, 0];
        }

        public void MakeLU() // Function for LU decomposition
        {
            if (!IsSquare()) throw new MException("The matrix is not square!");
            L = IdentityMatrix(rows, cols);
            U = Duplicate();

            pi = new int[rows];
            for (int i = 0; i < rows; i++) pi[i] = i;

            double p = 0;
            double pom2;
            int k0 = 0;
            int pom1 = 0;

            for (int k = 0; k < cols - 1; k++)
            {
                p = 0;
                for (int i = k; i < rows; i++) // find the row with the biggest pivot
                {
                    if (Math.Abs(U[i, k]) > p)
                    {
                        p = Math.Abs(U[i, k]);
                        k0 = i;
                    }
                }
                if (p == 0) // samé nuly ve sloupci
                    throw new MException("The matrix is singular!");

                pom1 = pi[k];
                pi[k] = pi[k0];
                pi[k0] = pom1; // switch two rows in permutation matrix

                for (int i = 0; i < k; i++)
                {
                    pom2 = L[k, i];
                    L[k, i] = L[k0, i];
                    L[k0, i] = pom2;
                }

                if (k != k0) detOfP *= -1;

                for (int i = 0; i < cols; i++) // Switch rows in U
                {
                    pom2 = U[k, i];
                    U[k, i] = U[k0, i];
                    U[k0, i] = pom2;
                }

                for (int i = k + 1; i < rows; i++)
                {
                    L[i, k] = U[i, k]/U[k, k];
                    for (int j = k; j < cols; j++)
                        U[i, j] = U[i, j] - L[i, k]*U[k, j];
                }
            }
        }


        public MatrixS SolveWith(MatrixS v) // Function solves Ax = v in confirmity with solution vector "v"
        {
            if (rows != cols) throw new MException("The matrix is not square!");
            if (rows != v.rows) throw new MException("Wrong number of results in solution vector!");
            if (L == null) MakeLU();

            MatrixS b = new MatrixS(rows, 1);
            for (int i = 0; i < rows; i++) b[i, 0] = v[pi[i], 0]; // switch two items in "v" due to permutation matrix

            MatrixS z = SubsForth(L, b);
            MatrixS x = SubsBack(U, z);

            return x;
        }

        public MatrixS Invert() // Function returns the inverted matrix
        {
            if (L == null) MakeLU();

            MatrixS inv = new MatrixS(rows, cols);

            for (int i = 0; i < rows; i++)
            {
                MatrixS Ei = MatrixS.ZeroMatrix(rows, 1);
                Ei[i, 0] = 1;
                MatrixS col = SolveWith(Ei);
                inv.SetCol(col, i);
            }
            return inv;
        }


        public double Det() // Function for determinant
        {
            if (L == null) MakeLU();
            double det = detOfP;
            for (int i = 0; i < rows; i++) det *= U[i, i];
            return det;
        }

        public MatrixS GetP() // Function returns permutation matrix "P" due to permutation vector "pi"
        {
            if (L == null) MakeLU();

            MatrixS matrix = ZeroMatrix(rows, cols);
            for (int i = 0; i < rows; i++) matrix[pi[i], i] = 1;
            return matrix;
        }

        public MatrixS Duplicate() // Function returns the copy of this matrix
        {
            MatrixS matrix = new MatrixS(rows, cols);
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = mat[i, j];
            return matrix;
        }

        public static MatrixS SubsForth(MatrixS A, MatrixS b)
            // Function solves Ax = b for A as a lower triangular matrix
        {
            if (A.L == null) A.MakeLU();
            int n = A.rows;
            MatrixS x = new MatrixS(n, 1);

            for (int i = 0; i < n; i++)
            {
                x[i, 0] = b[i, 0];
                for (int j = 0; j < i; j++) x[i, 0] -= A[i, j]*x[j, 0];
                x[i, 0] = x[i, 0]/A[i, i];
            }
            return x;
        }

        public static MatrixS SubsBack(MatrixS A, MatrixS b)
            // Function solves Ax = b for A as an upper triangular matrix
        {
            if (A.L == null) A.MakeLU();
            int n = A.rows;
            MatrixS x = new MatrixS(n, 1);

            for (int i = n - 1; i > -1; i--)
            {
                x[i, 0] = b[i, 0];
                for (int j = n - 1; j > i; j--) x[i, 0] -= A[i, j]*x[j, 0];
                x[i, 0] = x[i, 0]/A[i, i];
            }
            return x;
        }

        public static MatrixS ZeroMatrix(int iRows, int iCols) // Function generates the zero matrix
        {
            MatrixS matrix = new MatrixS(iRows, iCols);
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    matrix[i, j] = 0;
            return matrix;
        }

        public static MatrixS IdentityMatrix(int iRows, int iCols) // Function generates the identity matrix
        {
            MatrixS matrix = ZeroMatrix(iRows, iCols);
            for (int i = 0; i < Math.Min(iRows, iCols); i++)
                matrix[i, i] = 1;
            return matrix;
        }

        public static MatrixS RandomMatrix(int iRows, int iCols, int dispersion) // Function generates the random matrix
        {
            Random random = new Random();
            MatrixS matrix = new MatrixS(iRows, iCols);
            for (int i = 0; i < iRows; i++)
                for (int j = 0; j < iCols; j++)
                    matrix[i, j] = random.Next(-dispersion, dispersion);
            return matrix;
        }

        public static MatrixS Parse(string ps) // Function parses the matrix from string
        {
            string s = NormalizeMatrixString(ps);
            string[] rows = Regex.Split(s, "\r\n");
            string[] nums = rows[0].Split(' ');
            MatrixS matrix = new MatrixS(rows.Length, nums.Length);
            try
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    string rws = rows[i];
                    rws = rws.Replace('.', ',');
                    nums = rws.Split(' ');
                    for (int j = 0; j < nums.Length; j++)
                        matrix[i, j] = double.Parse(nums[j]);
                }
            }
            //FormatException exc
            catch (FormatException exc)
            {
                throw new MException("Wrong input format!");
            }
            return matrix;
        }

        public override string ToString() // Function returns matrix as a string
        {
            string s = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++) s += String.Format("{0,5:0.00}", mat[i, j]) + " ";
                s += "\r\n";
            }
            return s;
        }

        public static MatrixS Transpose(MatrixS m) // Matrix transpose, for any rectangular matrix
        {
            MatrixS t = new MatrixS(m.cols, m.rows);
            for (int i = 0; i < m.rows; i++)
                for (int j = 0; j < m.cols; j++)
                    t[j, i] = m[i, j];
            return t;
        }

        public static MatrixS Power(MatrixS m, int pow) // Power matrix to exponent
        {
            if (pow == 0) return IdentityMatrix(m.rows, m.cols);
            if (pow == 1) return m.Duplicate();
            if (pow == -1) return m.Invert();

            MatrixS x;
            if (pow < 0)
            {
                x = m.Invert();
                pow *= -1;
            }
            else x = m.Duplicate();

            MatrixS ret = IdentityMatrix(m.rows, m.cols);
            while (pow != 0)
            {
                if ((pow & 1) == 1) ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        private static void SafeAplusBintoC(MatrixS A, int xa, int ya, MatrixS B, int xb, int yb, MatrixS C, int size)
        {
            for (int i = 0; i < size; i++) // rows
                for (int j = 0; j < size; j++) // cols
                {
                    C[i, j] = 0;
                    if (xa + j < A.cols && ya + i < A.rows) C[i, j] += A[ya + i, xa + j];
                    if (xb + j < B.cols && yb + i < B.rows) C[i, j] += B[yb + i, xb + j];
                }
        }

        private static void SafeAminusBintoC(MatrixS A, int xa, int ya, MatrixS B, int xb, int yb, MatrixS C, int size)
        {
            for (int i = 0; i < size; i++) // rows
                for (int j = 0; j < size; j++) // cols
                {
                    C[i, j] = 0;
                    if (xa + j < A.cols && ya + i < A.rows) C[i, j] += A[ya + i, xa + j];
                    if (xb + j < B.cols && yb + i < B.rows) C[i, j] -= B[yb + i, xb + j];
                }
        }

        private static void SafeACopytoC(MatrixS A, int xa, int ya, MatrixS C, int size)
        {
            for (int i = 0; i < size; i++) // rows
                for (int j = 0; j < size; j++) // cols
                {
                    C[i, j] = 0;
                    if (xa + j < A.cols && ya + i < A.rows) C[i, j] += A[ya + i, xa + j];
                }
        }

        private static void AplusBintoC(MatrixS A, int xa, int ya, MatrixS B, int xb, int yb, MatrixS C, int size)
        {
            for (int i = 0; i < size; i++) // rows
                for (int j = 0; j < size; j++) C[i, j] = A[ya + i, xa + j] + B[yb + i, xb + j];
        }

        private static void AminusBintoC(MatrixS A, int xa, int ya, MatrixS B, int xb, int yb, MatrixS C, int size)
        {
            for (int i = 0; i < size; i++) // rows
                for (int j = 0; j < size; j++) C[i, j] = A[ya + i, xa + j] - B[yb + i, xb + j];
        }

        private static void ACopytoC(MatrixS A, int xa, int ya, MatrixS C, int size)
        {
            for (int i = 0; i < size; i++) // rows
                for (int j = 0; j < size; j++) C[i, j] = A[ya + i, xa + j];
        }

        private static MatrixS StrassenMultiply(MatrixS A, MatrixS B) // Smart matrix multiplication
        {
            if (A.cols != B.rows) throw new MException("Wrong dimension of matrix!");

            MatrixS R;

            int msize = Math.Max(Math.Max(A.rows, A.cols), Math.Max(B.rows, B.cols));

            if (msize < 32)
            {
                R = ZeroMatrix(A.rows, B.cols);
                for (int i = 0; i < R.rows; i++)
                    for (int j = 0; j < R.cols; j++)
                        for (int k = 0; k < A.cols; k++)
                            R[i, j] += A[i, k]*B[k, j];
                return R;
            }

            int size = 1;
            int n = 0;
            while (msize > size)
            {
                size *= 2;
                n++;
            }
            ;
            int h = size/2;


            MatrixS[,] mField = new MatrixS[n, 9];

            /*
             *  8x8, 8x8, 8x8, ...
             *  4x4, 4x4, 4x4, ...
             *  2x2, 2x2, 2x2, ...
             *  . . .
             */

            int z;
            for (int i = 0; i < n - 4; i++) // rows
            {
                z = (int) Math.Pow(2, n - i - 1);
                for (int j = 0; j < 9; j++) mField[i, j] = new MatrixS(z, z);
            }

            SafeAplusBintoC(A, 0, 0, A, h, h, mField[0, 0], h);
            SafeAplusBintoC(B, 0, 0, B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 1], 1, mField); // (A11 + A22) * (B11 + B22);

            SafeAplusBintoC(A, 0, h, A, h, h, mField[0, 0], h);
            SafeACopytoC(B, 0, 0, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 2], 1, mField); // (A21 + A22) * B11;

            SafeACopytoC(A, 0, 0, mField[0, 0], h);
            SafeAminusBintoC(B, h, 0, B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 3], 1, mField); //A11 * (B12 - B22);

            SafeACopytoC(A, h, h, mField[0, 0], h);
            SafeAminusBintoC(B, 0, h, B, 0, 0, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 4], 1, mField); //A22 * (B21 - B11);

            SafeAplusBintoC(A, 0, 0, A, h, 0, mField[0, 0], h);
            SafeACopytoC(B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 5], 1, mField); //(A11 + A12) * B22;

            SafeAminusBintoC(A, 0, h, A, 0, 0, mField[0, 0], h);
            SafeAplusBintoC(B, 0, 0, B, h, 0, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 6], 1, mField); //(A21 - A11) * (B11 + B12);

            SafeAminusBintoC(A, h, 0, A, h, h, mField[0, 0], h);
            SafeAplusBintoC(B, 0, h, B, h, h, mField[0, 1], h);
            StrassenMultiplyRun(mField[0, 0], mField[0, 1], mField[0, 1 + 7], 1, mField); // (A12 - A22) * (B21 + B22);

            R = new MatrixS(A.rows, B.cols); // result

            /// C11
            for (int i = 0; i < Math.Min(h, R.rows); i++) // rows
                for (int j = 0; j < Math.Min(h, R.cols); j++) // cols
                    R[i, j] = mField[0, 1 + 1][i, j] + mField[0, 1 + 4][i, j] - mField[0, 1 + 5][i, j] +
                              mField[0, 1 + 7][i, j];

            /// C12
            for (int i = 0; i < Math.Min(h, R.rows); i++) // rows
                for (int j = h; j < Math.Min(2*h, R.cols); j++) // cols
                    R[i, j] = mField[0, 1 + 3][i, j - h] + mField[0, 1 + 5][i, j - h];

            /// C21
            for (int i = h; i < Math.Min(2*h, R.rows); i++) // rows
                for (int j = 0; j < Math.Min(h, R.cols); j++) // cols
                    R[i, j] = mField[0, 1 + 2][i - h, j] + mField[0, 1 + 4][i - h, j];

            /// C22
            for (int i = h; i < Math.Min(2*h, R.rows); i++) // rows
                for (int j = h; j < Math.Min(2*h, R.cols); j++) // cols
                    R[i, j] = mField[0, 1 + 1][i - h, j - h] - mField[0, 1 + 2][i - h, j - h] +
                              mField[0, 1 + 3][i - h, j - h] + mField[0, 1 + 6][i - h, j - h];

            return R;
        }

        // function for square matrix 2^N x 2^N

        private static void StrassenMultiplyRun(MatrixS A, MatrixS B, MatrixS C, int l, MatrixS[,] f)
            // A * B into C, level of recursion, matrix field
        {
            int size = A.rows;
            int h = size/2;

            if (size < 32)
            {
                for (int i = 0; i < C.rows; i++)
                    for (int j = 0; j < C.cols; j++)
                    {
                        C[i, j] = 0;
                        for (int k = 0; k < A.cols; k++) C[i, j] += A[i, k]*B[k, j];
                    }
                return;
            }

            AplusBintoC(A, 0, 0, A, h, h, f[l, 0], h);
            AplusBintoC(B, 0, 0, B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 1], l + 1, f); // (A11 + A22) * (B11 + B22);

            AplusBintoC(A, 0, h, A, h, h, f[l, 0], h);
            ACopytoC(B, 0, 0, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 2], l + 1, f); // (A21 + A22) * B11;

            ACopytoC(A, 0, 0, f[l, 0], h);
            AminusBintoC(B, h, 0, B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 3], l + 1, f); //A11 * (B12 - B22);

            ACopytoC(A, h, h, f[l, 0], h);
            AminusBintoC(B, 0, h, B, 0, 0, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 4], l + 1, f); //A22 * (B21 - B11);

            AplusBintoC(A, 0, 0, A, h, 0, f[l, 0], h);
            ACopytoC(B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 5], l + 1, f); //(A11 + A12) * B22;

            AminusBintoC(A, 0, h, A, 0, 0, f[l, 0], h);
            AplusBintoC(B, 0, 0, B, h, 0, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 6], l + 1, f); //(A21 - A11) * (B11 + B12);

            AminusBintoC(A, h, 0, A, h, h, f[l, 0], h);
            AplusBintoC(B, 0, h, B, h, h, f[l, 1], h);
            StrassenMultiplyRun(f[l, 0], f[l, 1], f[l, 1 + 7], l + 1, f); // (A12 - A22) * (B21 + B22);

            /// C11
            for (int i = 0; i < h; i++) // rows
                for (int j = 0; j < h; j++) // cols
                    C[i, j] = f[l, 1 + 1][i, j] + f[l, 1 + 4][i, j] - f[l, 1 + 5][i, j] + f[l, 1 + 7][i, j];

            /// C12
            for (int i = 0; i < h; i++) // rows
                for (int j = h; j < size; j++) // cols
                    C[i, j] = f[l, 1 + 3][i, j - h] + f[l, 1 + 5][i, j - h];

            /// C21
            for (int i = h; i < size; i++) // rows
                for (int j = 0; j < h; j++) // cols
                    C[i, j] = f[l, 1 + 2][i - h, j] + f[l, 1 + 4][i - h, j];

            /// C22
            for (int i = h; i < size; i++) // rows
                for (int j = h; j < size; j++) // cols
                    C[i, j] = f[l, 1 + 1][i - h, j - h] - f[l, 1 + 2][i - h, j - h] + f[l, 1 + 3][i - h, j - h] +
                              f[l, 1 + 6][i - h, j - h];
        }

        public static MatrixS StupidMultiply(MatrixS m1, MatrixS m2) // Stupid matrix multiplication
        {
            if (m1.cols != m2.rows) throw new MException("Wrong dimensions of matrix!");

            MatrixS result = ZeroMatrix(m1.rows, m2.cols);
            for (int i = 0; i < result.rows; i++)
                for (int j = 0; j < result.cols; j++)
                    for (int k = 0; k < m1.cols; k++)
                        result[i, j] += m1[i, k]*m2[k, j];
            return result;
        }

        public static MatrixS Multiply(double n, MatrixS m) // Multiplication by constant n
        {
            MatrixS r = new MatrixS(m.rows, m.cols);
            for (int i = 0; i < m.rows; i++)
                for (int j = 0; j < m.cols; j++)
                    r[i, j] = m[i, j]*n;
            return r;
        }

        private static MatrixS Add(MatrixS m1, MatrixS m2) // Sčítání matic
        {
            if (m1.rows != m2.rows || m1.cols != m2.cols)
                throw new MException("Matrices must have the same dimensions!");
            MatrixS r = new MatrixS(m1.rows, m1.cols);
            for (int i = 0; i < r.rows; i++)
                for (int j = 0; j < r.cols; j++)
                    r[i, j] = m1[i, j] + m2[i, j];
            return r;
        }

        public static string NormalizeMatrixString(string matStr) // From Andy - thank you! :)
        {
            // Remove any multiple spaces
            while (matStr.IndexOf("  ") != -1)
                matStr = matStr.Replace("  ", " ");

            // Remove any spaces before or after newlines
            matStr = matStr.Replace(" \r\n", "\r\n");
            matStr = matStr.Replace("\r\n ", "\r\n");

            // If the data ends in a newline, remove the trailing newline.
            // Make it easier by first replacing \r\n’s with |’s then
            // restore the |’s with \r\n’s
            matStr = matStr.Replace("\r\n", "|");
            while (matStr.LastIndexOf("|") == (matStr.Length - 1))
                matStr = matStr.Substring(0, matStr.Length - 1);

            matStr = matStr.Replace("|", "\r\n");
            return matStr.Trim();
        }

        //   O P E R A T O R S

        public static MatrixS operator -(MatrixS m)
        {
            return MatrixS.Multiply(-1, m);
        }

        public static MatrixS operator +(MatrixS m1, MatrixS m2)
        {
            return MatrixS.Add(m1, m2);
        }

        public static MatrixS operator -(MatrixS m1, MatrixS m2)
        {
            return MatrixS.Add(m1, -m2);
        }

        public static MatrixS operator *(MatrixS m1, MatrixS m2)
        {
            return MatrixS.StrassenMultiply(m1, m2);
        }

        public static MatrixS operator *(double n, MatrixS m)
        {
            return MatrixS.Multiply(n, m);
        }
    }
}

//  The class for exceptions

public class MException : Exception
{
    public MException(string Message)
        : base(Message)
    {
    }
}