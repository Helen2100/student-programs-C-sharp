using System;

namespace func_rocket
{
    public class ControlTask
    {
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var angle = 0.0;

            //Вектор, «соединяющий» ракету с целью(РЦ)
            var vectorDistance = new Vector(target.X - rocket.Location.X,
                                      target.Y - rocket.Location.Y);

            var firstAngle = vectorDistance.Angle - rocket.Direction;//угол между вектором ракеты и РЦ
            var moduleFirstAngle = Math.Abs(vectorDistance.Angle - rocket.Direction);//модуль угола между вектором ракеты и РЦ
            
            var secondAngle = vectorDistance.Angle - rocket.Velocity.Angle;//угол между вектором скорости ракеты и РЦ
            var moduleSecondAngle = Math.Abs(vectorDistance.Angle - rocket.Velocity.Angle);//модуль угола между вектором скорости ракеты и РЦ

            if (moduleFirstAngle < 0.5 || moduleSecondAngle < 0.5)//если один из модулей уголов меньше 0,5
                angle = (firstAngle + secondAngle) / 2;//то итоговый угол равен половине суммы углов
            else//иначе
                angle = firstAngle;//итоговый угол равен углу между вектором ракеты и РЦ

            if (angle != 0)//если итоговый угол не равен 0
            {
                if (angle < 0) return Turn.Left;//если угол меньше 0, то повернуть объекта на лево
                else return Turn.Right;//если угол больше 0, то повернуть объекта на право
            }
            else//если угол равен 0
                return Turn.None;//то не изменяем направление объекта
        }
    }
}