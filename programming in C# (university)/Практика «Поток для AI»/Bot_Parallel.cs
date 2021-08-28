using System;
using System.Linq;
using System.Threading.Tasks;

namespace rocket_bot
{
    public partial class Bot
    {
        public Rocket GetNextMove(Rocket rocket)
        {//�������������� ����������
            var probableWays = new Tuple<Turn, double>[threadsCount];//��������� ����(������ ���� Tuple)
            var bestWays = new Task<Tuple<Turn, double>>[threadsCount];//��� ����������� ����� ������
            for (var i = 0; i < threadsCount; i++)//����������� �� ���-�� �������
                bestWays[i] = DoParallelSearch(rocket);//����������� ����������� ���
            foreach (var way in bestWays)//����������� �� ����������� �����
                way.Start();//��������� ���� (������)
            for (var i = 0; i < threadsCount; i++)//����� ����������� �� ���-�� �������
                probableWays[i] = bestWays[i].Result;//� ���������� ���������� �������� �������� ����������
            var maxProbableWay = probableWays.Max(m => m.Item2);//������� ������������ �������� � ������������������
            var move = probableWays.Where(n => n.Item2 == maxProbableWay);//������� ��� �������� ������ ���� ��������
            return rocket.Move(move.First().Item1, level);//���������� �������� ���������� � ������ Move
        }

        private Task<Tuple<Turn, double>> DoParallelSearch(Rocket rocket)
        {//�������������� ����������
            var searchRandom = new Random(random.Next());//�������� ����� �����
            var searchIterationsCount = iterationsCount / threadsCount;//��������� ����� ����������� ���-�� �������� ������
            return new Task<Tuple<Turn, double>>(//���� ����������� ��������� ��� ��� ������
                () => SearchBestMove(rocket, searchRandom, searchIterationsCount)//� ������� ������ SearchBestMove
                    );//� ���������� ���������� ����������
        }
    }
}