using System;

namespace DistanceTask
{
	public static class DistanceTask
	{
		// Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
		public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
		{
			// находим стороны треугольнка, который образован с помощью линии и точки
			var a = Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));
			var b = Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
			var ab = Math.Sqrt((ax - bx) * (ax - bx) + (ay - by) * (ay - by));
			// линия = точка и точка 
			if (ax == bx && ay == by)
				return a;
			// тупоугольный треугольник
			else if (((x - ax) * (bx - ax) + (y - ay) * (by - ay)) < 0 || ((x - bx) * (ax - bx) + (y - by) * (ay - by)) < 0)
				return Math.Min(a, b);
			else if (y < Math.Max(ay, by) || y > Math.Min(ay, by)) // оставшиеся треугольники
			{
				if (a == 0 || b == 0 || ab == 0) // точка лежит на линии
					return 0;
				var cos = (ab * ab - b * b - a * a) / (2 * a * b);//теорема COS
				var sin = Math.Sqrt(1 - cos * cos);//основное тригонометрическое тождество
				var square = (a * b * sin) / 2;//площадь треугольна через SIN угла 
				return (2 * square) / ab;//находим высоту, через площадь
			}
			else return 0; // точка лежит на линии
		}
	}
}