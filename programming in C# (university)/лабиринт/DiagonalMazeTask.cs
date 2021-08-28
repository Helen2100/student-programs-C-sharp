namespace Mazes
{
	public static class DiagonalMazeTask
	{
		//робот двигается или влево, или вправо
		private static void MoveSecond(Robot robot, int distance, Mazes.Direction turn)
		{
			for (int i = 0; i < distance; i++)
				robot.MoveTo(turn);
		}
		
		//робот начинает движение
		private static void Move(Robot robot, int distance, Mazes.Direction turn)
		{
			while (!robot.Finished)
			{
				MoveSecond(robot, distance, turn);//двигается в указанное направление(несколько шагов)
				if (robot.Finished) break;//если робот дошел до финиша, то ему нужно выйти из цикла
				if (turn == Direction.Right) robot.MoveTo(Direction.Down);//если он начал двгаться вправо, то для продолжения цикла ему надо сделать шаг вниз
				else robot.MoveTo(Direction.Right);////если он начал двгаться вниз, то для продолжения цикла ему надо сделать шаг вправо
			}
		}
		
		public static void MoveOut(Robot robot, int width, int height)
		{
			//выясняем какие размеры будут у коридора в лабиринте
			var widthCorridor = (width - 3) / (height - 2);
			var heightCorridor = (height - 3) / (width - 2);
			//вертикально или горизонтально расположен лабиринт?
			//также можно заметить, что если длина лабиринта больше высоты, 
			//то длина у коридора будет больше, чем высота.
			if (height > width)
				Move(robot, heightCorridor, Direction.Down);
			else
				Move(robot, widthCorridor, Direction.Right);
		}
	}
}