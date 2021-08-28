using System;

namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
        {
            var xLength = original.GetLength(0);//длина изображения
            var yLength = original.GetLength(1);//ширина изображения
            //получаем массив, состоящий из пикселей картины
            double[] picturePixel = GetPicturePixel(original, xLength, yLength);
            //получаем пороговое значение
            double thresholdValue = GetThresholdValue(whitePixelsFraction, xLength, yLength, picturePixel);
            //получаем черно-белую картину
            double[,] blackAndWhitePicture = GetBlackAndWhitePicture(original, xLength, yLength, thresholdValue);

            return blackAndWhitePicture;
        }
        //метод, который преобразует цвет картину в черно-белую
        private static double[,] GetBlackAndWhitePicture(double[,] original, int xLength, int yLength, double thresholdValue)
        {
            var blackAndWhitePicture = new double[xLength, yLength];//черно-белая картина
            var numberColor = 0.0;//номер цвета
            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                {
                    if (original[x, y] >= thresholdValue)
                        numberColor = 1.0;//белый цвет
                    else numberColor = 0.0;//черный цвет
                    blackAndWhitePicture[x, y] = numberColor;//изменяем цвет пикселя на черн или бел цвет
                }

            return blackAndWhitePicture;
        }
        //метод, который находит пороговое значение
        private static double GetThresholdValue(double whitePixelsFraction, int xLength, int yLength, double[] picturePixel)
        {
            var thresholdValue = 0.0;//Пороговое значение
            var whitePixelsCount = (int)(whitePixelsFraction * xLength * yLength);//кол-во белых пикселей
            if (whitePixelsCount == xLength * yLength)//кол-во бел пикс=общ кол-вом пикс в картине
                thresholdValue = double.NegativeInfinity;//берем(-)бесконечность,т.к.изобр.сост.из бел.пикс
            else if (whitePixelsCount == 0)//нет бел пикселей по умолчанию
                thresholdValue = double.PositiveInfinity;//берем(+)бесконечность,т.к.изобр.сост.из черн.пикс
            else
            {
                var countBlackPixelMax = xLength * yLength - whitePixelsCount;//кол-во черных пикселей
                thresholdValue = picturePixel[countBlackPixelMax];//Пороговое значение
            }

            return thresholdValue;
        }
        //медот, который разбивает картину на пиксели и складывает их в одномерный массив
        private static double[] GetPicturePixel(double[,] original, int xLength, int yLength)
        {
            var picturePixel = new double[xLength * yLength];//создание массива пикселей на опред кол-во
            for (int i = 0; i < (xLength * yLength); i++)//берем по пикселям 
                picturePixel[i] = original[i % xLength, i / xLength];//загружаем карт по пикс в данный массив
            Array.Sort(picturePixel);//сортируем пиксели в картине
            return picturePixel;
        }
    }
}