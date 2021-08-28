using System.Drawing;
using System;

namespace Fractals
{
	internal static class DragonFractalTask
	{
		public static double DragonCurveX(double x, double y, double x0, double corner)
        {
			return (x * Math.Cos(corner) - y * Math.Sin(corner)) / Math.Sqrt(2);
		}

		public static double DragonCurveY(double x, double y, double x0, double corner)
        {
			return (x0 * Math.Sin(corner) + y * Math.Cos(corner)) / Math.Sqrt(2);
		}

		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			var random = new Random(seed);
			var x = 1.0;//начальная точка  
			var y = 0.0;
			for (int i = 0; i < iterationsCount; ++i)
			{ 
				var x0 = x;
				//Преобразование 1. (поворот на 45 градусов и сжатие в sqrt(2) раз):
				if (random.Next(2) == 0)
				{
					x = DragonCurveX(x, y, x0, Math.PI / 4);
					y = DragonCurveY(x, y, x0, Math.PI / 4);
				}
				//Преобразование 2. (поворот на 135 градусов, сжатие в Math.Sqrt(2) раз, сдвиг по X на единицу):
				else
				{
					x = DragonCurveX(x, y, x0, Math.PI * 3 / 4) + 1;
					y = DragonCurveY(x, y, x0, Math.PI * 3 / 4);
				}
				//Рисуем текущую точку методом pixels.SetPixel(x, y)
				pixels.SetPixel(x, y);
			}
		}
	}
}