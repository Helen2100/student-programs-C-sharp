namespace Mazes
{
	public static class SnakeMazeTask
	{
		private static void Move(Robot robot, int distance, Mazes.Direction turn)
		{
			var step=0;
			if (turn == Direction.Down) step = 2;
			else step = distance - 3;
			for (int i = 0; i < step; i++)
				robot.MoveTo(turn);
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			while (!robot.Finished)
            {
				Move(robot, width, Direction.Right);
				Move(robot, 0, Direction.Down);
				Move(robot, width, Direction.Left);
				if (robot.Finished) break;
				else Move(robot, 0, Direction.Down);
			}
		}
	}
}