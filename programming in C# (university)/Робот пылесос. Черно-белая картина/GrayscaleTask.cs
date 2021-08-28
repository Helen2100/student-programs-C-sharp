namespace Recognizer
{
	public static class GrayscaleTask
	{
		/* 
		 * Переведите изображение в серую гамму.
		 * 
		 * original[x, y] - массив пикселей с координатами x, y. 
		 * Каждый канал R,G,B лежит в диапазоне от 0 до 255.
		 * 
		 * Получившийся массив должен иметь те же размеры, 
		 * grayscale[x, y] - яркость пикселя (x,y) в диапазоне от 0.0 до 1.0
		 *
		 * Используйте формулу:
		 * Яркость = (0.299*R + 0.587*G + 0.114*B) / 255
		 * 
		 * Почему формула именно такая — читайте в википедии 
		 * http://ru.wikipedia.org/wiki/Оттенки_серого
		 */

		public static double[,] ToGrayscale(Pixel[,] original)
		{
			var levelRed = 0.299;//яркость красного для преобразования картины в серую гамму
			var levelGreen = 0.587;//яркость зеленого для преобразования картины в серую гамму
			var levelBlue = 0.114;//яркость синего для преобразования картины в серую гамму
			var numberGradations = 255;//всего оттенков серого (от 0 до 255)
			var xLength = original.GetLength(0);//размер картины по оси Ох в пикселях
			var yLength = original.GetLength(1);//размер картины по оси Оу в пикселях
			var pictureBlackWhite = new double [xLength, yLength];//создаем массив для прорисовки черно-белой картины
			for (var x = 0; x < xLength; x++)
            {
				for(var y = 0; y < yLength; y++)
                {
					int b = original[x, y].B; //Уровень синего в пикселе первоначальной картины
					int r = original[x, y].R; //Уровень красного в пикселе первоначальной картины
					int g = original[x, y].G; //Уровень зеленого в пикселе первоначальной картины
					pictureBlackWhite [x,y] = (levelRed * r + levelGreen * g + levelBlue * b) / numberGradations;
					//Рассчитываем яркость серого оттенка 
				}
            }
			return pictureBlackWhite;//возвращаем черно-белую картину
		}
	}
}