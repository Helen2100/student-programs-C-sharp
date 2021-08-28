using System.Drawing;
using System.Linq;

namespace func_rocket
{
    public class ForcesTask
    {
        /// <summary>
        /// Создает делегат, возвращающий по ракете вектор силы тяги двигателей этой ракеты.
        /// Сила тяги направлена вдоль ракеты и равна по модулю forceValue.
        /// </summary>
        public static RocketForce GetThrustForce(double forceValue)
        {//сооздаем делегат, в котором мы создаем новый вектор с параметрами х=forceValue и у=0,
            return result => new Vector(forceValue, 0).Rotate(result.Direction);
        }// который мы поворачиваем на определенный угол

        /// <summary>
        /// Преобразует делегат силы гравитации, в делегат силы, действующей на ракету
        /// </summary>
        public static RocketForce ConvertGravityToForce(Gravity gravity, Size spaceSize)
        {
            return result => gravity(spaceSize, result.Location);
        }

        /// <summary>
        /// Суммирует все переданные силы, действующие на ракету, и возвращает суммарную силу.
        /// </summary>
        public static RocketForce Sum(params RocketForce[] forces)
        {//создаем лябда-выражение, в которой
            return rocket =>
            {//присваиваем sumForces нулевой вектор
                var sumForces = new Vector(0, 0);//переменная для векторной суммы всех сил
                foreach (var force in forces)//пробегаемся по делегату сил
                    sumForces += force(rocket);//суммируем силы, действующие на ракету,
                return sumForces;//возвращяем суммарную силу
            };
        }
    }
}