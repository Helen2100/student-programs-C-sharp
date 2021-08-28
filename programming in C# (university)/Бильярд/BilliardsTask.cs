using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directionRadians">Угол направелния движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns>угол откакивания шара от стены</returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            double res = 2 * wallInclinationRadians - directionRadians;
            return res;
        }

        static void Main()
        {
            string corner;
            corner = Console.ReadLine();
            double directionRadians = double.Parse(corner);
            string cornerElse = Console.ReadLine();
            double wallInclinationRadians = double.Parse(cornerElse);
            Console.WriteLine(BounceWall(directionRadians, wallInclinationRadians));
        }
    }
}