using System;

namespace Rectangles
{
	public static class RectanglesTask
	{
		// Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
		public static bool AreIntersected(Rectangle r1, Rectangle r2)
		{
			var minPointRight = Math.Min(r1.Right, r2.Right);
			var maxPointLeft = Math.Max(r1.Left, r2.Left);
			var minPointBottom = Math.Min(r1.Bottom, r2.Bottom);
			var maxPointTop = Math.Max(r1.Top, r2.Top);

			return (minPointRight >= maxPointLeft) && (minPointBottom >= maxPointTop);
		}

		// Площадь пересечения прямоугольников
		public static int IntersectionSquare(Rectangle r1, Rectangle r2)
		{
			if (AreIntersected(r1, r2))
			{
				//мин сторона прямоугльника, которая образована результатом пересечения
				var minHeight1 = Math.Min(r1.Bottom - r2.Top, r2.Bottom - r1.Top);
				var minWidth1 = Math.Min(r1.Right - r2.Left, r2.Right - r1.Left);
				// сравниваем со стронами прямоугольников (если треугольники вложены)
				var minHeight2 = Math.Min(r1.Height, minHeight1);
				var minHeight3 = Math.Min(r2.Height, minHeight2);
				var minWidth2 = Math.Min(r1.Width, minWidth1);
				var minWidth3 = Math.Min(r2.Width, minWidth2);
				return minHeight3 * minWidth3;
			}
			else
				return 0;
		}

		// Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
		// Иначе вернуть -1
		// Если прямоугольники совпадают, можно вернуть номер любого из них.
		public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
		{
			if ((r1.Left >= r2.Left) && (r1.Right <= r2.Right) && (r1.Top >= r2.Top) && (r1.Bottom <= r2.Bottom))
				return 0;
			else if ((r1.Left <= r2.Left) && (r1.Right >= r2.Right) && (r1.Top <= r2.Top) && (r1.Bottom >= r2.Bottom))
				return 1;
			else
				return -1;
		}
	}
}