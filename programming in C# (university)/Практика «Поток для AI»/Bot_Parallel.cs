using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {//инициализируем переменные
            var probableWays = new Tuple<Turn, double>[threadsCount];//возможные ходы(массив типа Tuple)
            var bestWays = new Task<Tuple<Turn, double>>[threadsCount];//для оптимальных ходов ракеты
            for (var i = 0; i < threadsCount; i++)//пробегаемся по кол-ву потоков
                bestWays[i] = DoParallelSearch(rocket);//высчитываем оптимальный ход
            foreach (var way in bestWays)//пробегаемся по оптимальным ходам
                way.Start();//запускаем путь (задачу)
            for (var i = 0; i < threadsCount; i++)//снова пробегаемся по кол-ву потоков
                probableWays[i] = bestWays[i].Result;//и запоминаем полученное итоговое значение переменной
            var maxProbableWay = probableWays.Max(m => m.Item2);//находим максимальное значение в последовательности
            var move = probableWays.Where(n => n.Item2 == maxProbableWay);//убираем все элементы меньше макс значения
            return rocket.Move(move.First().Item1, level);//возвращаем значение полученное в методе Move
        }

        private Task<Tuple<Turn, double>> DoParallelSearch(Rocket rocket)
        {//инициализируем переменные
            var searchRandom = new Random(random.Next());//выбираем новое число
            var searchIterationsCount = iterationsCount / threadsCount;//благодаря циклу уменьшается кол-во итераций поиска
            return new Task<Tuple<Turn, double>>(//ищем оптимальный следующий ход для ракеты
                () => SearchBestMove(rocket, searchRandom, searchIterationsCount)//с помощью метода SearchBestMove
                    );//и возвращаем полученную переменную
        }
    }
}