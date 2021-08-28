using System.ComponentModel;

namespace Mazes
{
	public static class EmptyMazeTask
	{
		private static void Move(Robot robot, int distance, Mazes.Direction turn)
		{
			var step = distance - 3;
			for (int i = 0; i < step; i++)
				robot.MoveTo(turn);
		}

		public static void MoveOut(Robot robot, int width, int height)
		{
			Move(robot, height, Direction.Down);
			Move(robot, width, Direction.Right);
		}
	}
}