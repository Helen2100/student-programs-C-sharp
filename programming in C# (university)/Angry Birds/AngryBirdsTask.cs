using System;
namespace AngryBirds
{
    public static class AngryBirdsTask
    {
        // Ниже — это XML документация, её использует ваша среда разработки,
        // чтобы показывать подсказки по использованию методов.
        // Но писать её естественно не обязательно.
        /// <param name="v">Начальная скорость</param>
        /// <param name="distance">Расстояние до цели</param>
        /// <returns>Угол прицеливания в радианах от 0 до Pi/2</returns>
        public static double FindSightAngle(double v, double distance)
        {
            double g = 9.8;
            double result = ((distance * g) / (v * v));
            result = Math.Asin(result);
            result = 0.5 * result;
            return result;
        }

        static void main ()
        {
            string speed = Console.ReadLine();
            double v;
            v = double.Parse(speed);
            string rast;
            rast = Console.ReadLine();
            double distant;
            distant = double.Parse(rast);
            Console.WriteLine(FindSightAngle(v, distant));
        }
    }
}