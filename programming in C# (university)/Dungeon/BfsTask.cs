using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon
{
    public class BfsTask
    {
        public static IEnumerable<SinglyLinkedList<Point>> FindPaths(Map map, Point start, Point[] chests)
        {
            Queue<Point> queueStartPoint = new Queue<Point>();//�������������� �������
            queueStartPoint.Enqueue(start);//��������� � ������� �����
            HashSet<Point> visitedPoints = new HashSet<Point>();//�������������� ���������� ��� ������ ���������� �����
            visitedPoints.Add(start);//�������� ���������� �� ���������� ������ �����
            Dictionary<Point, SinglyLinkedList<Point>> waysTreePoints
                = new Dictionary<Point, SinglyLinkedList<Point>>();//�������������� �������
            waysTreePoints.Add(start, new SinglyLinkedList<Point>(start));//���. � ��. �����(����) � ����(��������), ������� �������� � ���� ����
            while (queueStartPoint.Count != 0)//���� � ������� ���� �����
            {
                var startPoint = queueStartPoint.Dequeue();//������� �� ������� �����
                if (GetLocationPointMap(map,startPoint)) continue;//��������� �� ����������� �� �����
                if (GetInformationPoint(map, startPoint)) continue;//��������� ������ �� ����������(�����/�� ������)
                for (var y = -1; y <= 1; y++)//��������� ���������� 6 �� 6
                    for (var x = -1; x <= 1; x++)//�� ����������� ������
                    {
                        if (x != 0 && y != 0) continue;//���� ��� �� �(0;0)(�� ������� �����=��������), �� ������� � ������ �����
                        var nearPoint = new Point { X = startPoint.X + x, Y = startPoint.Y + y };//���������� ����������� �����
                        if (visitedPoints.Contains(nearPoint)) continue;//���� �� ��� �������� ��� �����, �� ������� � ���������
                        queueStartPoint.Enqueue(nearPoint);//��������� ���(�����������) ����� � �������
                        visitedPoints.Add(nearPoint);//��������� ���(�����������) ����� � ����������
                        waysTreePoints.Add(nearPoint, new SinglyLinkedList<Point>(nearPoint, waysTreePoints[startPoint]));//��������� ����� � �� �����
                    }
            }
            foreach (var chest in chests)//��������� �� ��������
            {
                if (waysTreePoints.ContainsKey(chest))//���� �� �����-�� �� ������ �������� ������
                    yield return waysTreePoints[chest];//�� ���������� ���� �� ����� ������ �� �������
            }
        }

        //��������� ����� �� �����������������(�� ����� ��� ����� ����)
        public static bool GetLocationPointMap(Map map, Point point) => 
            point.X < 0 || point.X >= map.Dungeon.GetLength(0) || point.Y < 0 || point.Y >= map.Dungeon.GetLength(1);
        //��������� �������� �� ������ �����
        public static bool GetInformationPoint(Map map, Point startPoint) => 
            map.Dungeon[startPoint.X, startPoint.Y] != MapCell.Empty;
    }
}