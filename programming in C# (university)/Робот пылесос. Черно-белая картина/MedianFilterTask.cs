using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var xLength = original.GetLength(0);//ширина исх картинки
            var yLength = original.GetLength(1);//высота исх картинки
            var medianPicture = new double[xLength, yLength];//картина без шумов

            for (int x = 0; x < xLength; x++)
                for (int y = 0; y < yLength; y++)
                    //добавляем пиксель в картину
                    medianPicture[x, y] = GetMedianPixels(x, y, original, xLength, yLength);
            return medianPicture;
        }
        //получить медийное значение
        public static double GetMedianPixels(int x, int y, double[,] pixels, int xLength, int yLength)
        {
            var pixelTable = new List<double>();//лист для таблицы пикселей
            GetPixelTable(x, y, pixels, xLength, yLength, pixelTable);//создаем таблицу пикселей
            var tableCount = pixelTable.Count;//кол-во переменных в таблице
            if (tableCount % 2 == 1)//если размер пикселей нечетное
                return pixelTable[tableCount / 2];//находим середнее значение в таблице(формула из википендии)
            else
                return (pixelTable[(tableCount / 2) - 1] + pixelTable[tableCount / 2]) / 2;//находим среднее значение(формула из википендии)
        }
        //создаем таблицу писелей => пиксели вокруг определенной координаты
        public static List<double> GetPixelTable(int x, int y, double[,] pixels, int xLength, int yLength, List<double> pixelTable)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (IsPixelInsideBoundariesPicture(x - 1 + i, y - 1 + j, xLength, yLength))//если писель внутри границ
                        pixelTable.Add(pixels[x - 1 + i, y - 1 + j]);//добавить этот пиксел в лист пикселей
            pixelTable.Sort();//сортировка от мин до макс
            return pixelTable;
        }
        //проверка нахождения пикселя внутри картины и осей координат
        public static bool IsPixelInsideBoundariesPicture(int x, int y, int xLength, int yLength)
        {
            return (x < xLength && y < yLength) && (x > -1 && y > -1);
        }
    }
}