using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var xLength = g.GetLength(0);//ширина исх картины
            var yLength = g.GetLength(1);//длина исх картины
            var lengthSx = sx.GetLength(0);//размерность матрицы-фильтра
            var sy = GetTransposedMatrix(sx, lengthSx);//обратная матрица матрицы sx
            int middle = lengthSx / 2;//середина
            var result = new double[xLength, yLength];//итог. картина

            for (int x = middle; x < xLength - middle; x++)
            {
                for (int y = middle; y < yLength - middle; y++)
                {
                    //свёрткой (Сonvolution)
                    double[,] matrixSurroundings = GetMatrixSurroundings(g, x, y, lengthSx, middle);//окрестности точки(матрица)
                    double gx = GetProductMatrix(matrixSurroundings, sx);//поэлементарное умножение матриц
                    double gy = GetProductMatrix(matrixSurroundings, sy);//поэлементарное умножение матриц

                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }
            return result;
        }
        //метод, который создает обратную матрицу
        public static double[,] GetTransposedMatrix(double[,] sx, int length)
        {
            var sy = new double[length, length];
            for (int y = 0; y < length; y++)
                for (int x = 0; x < length; x++)
                    sy[x, y] = sx[y, x];//переставляем элемент матрицы
            return sy;
        }

        //метод, создает матрицу окрестности точки (х, у)
        public static double[,] GetMatrixSurroundings(double[,] g, int x, int y, int lengthSx, int middle)
        {
            double[,] matrix = new double[lengthSx, lengthSx];

            for (int i = -middle; i <= middle; i++)
                for (int j = -middle; j <= middle; j++)
                    matrix[i + middle, j + middle] = g[x + j, y + i];//заполняем матрицу окрестностями точки 

            return matrix;
        }

        //метод, который поэлементно умножает матрицы
        public static double GetProductMatrix(double[,] matrixSurroundings, double[,] matrix)
        {
            int length = matrixSurroundings.GetLength(0);
            double compositionMatrix = 0;//переменная для суммы произведений элементов матриц

            for (int i = 0; i < length; i++)
                for (int j = 0; j < length; j++)
                    compositionMatrix += matrixSurroundings[i, j] * matrix[i, j];//сумма произведений элементов матриц

            return compositionMatrix;
        }
    }
}