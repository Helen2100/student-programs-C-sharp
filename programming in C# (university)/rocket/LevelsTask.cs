using System;
using System.Collections.Generic;

namespace func_rocket
{
    public class LevelsTask
    {
        static readonly Physics standardPhysics = new Physics();

        public static IEnumerable<Level> CreateLevels()
        {//создаем уровень "Zero"
            yield return new Level("Zero",//название уровня
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//начальное положение ракеты
                new Vector(600, 200),//начальное положение чёрной дыры
                (size, v) => Vector.Zero, //направление вектора гравитации
                standardPhysics);//вызываем метод, который передвигает ракету
                                 //создаем уровень "Heave"
            yield return new Level("Heavy",//название уровня
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//начальное положение ракеты
                new Vector(600, 200),//начальное положение чёрной дыры
                (size, v) => new Vector(0, 0.9), standardPhysics);//направление вектора гравитации;и вызываем метод, который передвигает ракету
            //создаем уровень "Up"
            yield return new Level("Up",//название уровня
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//начальное положение ракеты
                new Vector(700, 500),//начальное положение чёрной дыры
                (size, v) => new Vector(0, -300 / (size.Height - v.Y + 300.0)),//направление вектора гравитации
                standardPhysics);//вызываем метод, который передвигает ракету
            //создаем уровень "WhiteHole"
            yield return new Level("WhiteHole",//название уровня
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//начальное положение ракеты
                new Vector(600, 200),//начальное положение чёрной дыры
                (size, v) =>//направление вектора гравитации
                {
                    var vecToWhiteHole = new Vector(v.X - 600, v.Y - 200);//вектор к ЦП(белому пятну)
                    var vectLength = vecToWhiteHole.Length;//длина вектора к ЦП
                    return (vecToWhiteHole.Normalize() * 140 * vectLength / (Math.Pow(vectLength, 2) + 1));
                    //формула направления вектора гравитации
                },
                standardPhysics);//вызываем метод, который передвигает ракету
            //создаем уровень "BlackHole"
            yield return new Level("BlackHole",//название уровня
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//начальное положение ракеты
                new Vector(600, 200),//начальное положение чёрной дыры
                (size, v) =>//направление вектора гравитации
                {
                    var vectorBlackHolePos = new Vector(400, 350);//положение центра притяжения(ЦП)
                    var lengToBlckHole = (vectorBlackHolePos - v).Length;//длина вектора, соединяющий ЦП и ранд точку на карте
                    var vector = new Vector(vectorBlackHolePos.X - v.X, vectorBlackHolePos.Y - v.Y);//коорд вектора, соединяющий ЦП и ранд точку на карте
                    return new Vector(vector.X, vector.Y).Normalize() * 300 * lengToBlckHole /
                    (Math.Pow(lengToBlckHole, 2) + 1);//формула направления вектора гравитации
                },
                standardPhysics);//вызываем метод, который передвигает ракету
            //создаем уровень "BlackAndWhite"
            yield return new Level("BlackAndWhite",//название уровня
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//начальное положение ракеты
                new Vector(600, 200),//начальное положение чёрной дыры
                (size, v) =>//направление вектора гравитации
                {
                    var vectorToWhiteHole = new Vector(v.X - 600, v.Y - 200);//вектор ЦП-белого пятна
                    var vectorToBlackHole = new Vector(400, 350);//вектор черного пятна(ЧП)
                    var lengToBlackHole = (vectorToBlackHole - v).Length;//длина вектора, соединяющий ЧП и ранд точку на карте
                    var lengVecToWhtHole = vectorToWhiteHole.Length;//длина вектора ЦП-белого пятна
                    return (vectorToWhiteHole.Normalize() * 140 * lengVecToWhtHole / (Math.Pow(lengVecToWhtHole, 2) + 1) +
                    new Vector(vectorToBlackHole.X - v.X, vectorToBlackHole.Y - v.Y).Normalize() * 300 *
                    lengToBlackHole / (Math.Pow(lengToBlackHole, 2) + 1)) / 2;//формула направления вектора гравитации
                }
                , standardPhysics);//вызываем метод, который передвигает ракету
        }
    }
}