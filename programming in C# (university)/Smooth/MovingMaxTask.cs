using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<double> newElements = new Queue<double>();//������� ��� �������� �����
            LinkedList<double> maxElements = new LinkedList<double>();//���� ��� ���� �����
			var times = 0;//�������

			foreach (var element in data)
			{//���������� �������� ��� �����
                if (times < windowWidth)//���� ���� �� ���������
                    times++;//����������� �������

				else if (maxElements.First.Value.Equals(newElements.Dequeue()))
					//��� ���� ������ ������� ������� (����. �����) ����� ������� �������� ����� (����)
					maxElements.RemoveFirst();//�� ������� ���� ���� �� �����

                newElements.Enqueue(element.OriginalY);//������ � ������� �����

				while ((maxElements.Count > 0) && (element.OriginalY >= maxElements.Last.Value))
					//���� ������ ����� ������ ���������� ���� ���a � �����(� ���� ���� ���� �� ����)
					maxElements.RemoveLast();//�� ������� ��������� ���� �����

				maxElements.AddLast(element.OriginalY);//��������� ����� � ���� ����
				DataPoint newMaxElement = element.WithMaxY(maxElements.First.Value);//���������� ��� DataPoint
				yield return newMaxElement;//����� ���� �����
			}
		}
	}
}