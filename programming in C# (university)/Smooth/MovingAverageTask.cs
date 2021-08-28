using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
			var newElement = 0.0;//���������� �������
			var sumElement = 0.0;//����� ���������
			Queue<double> newData = new Queue<double>();//������� ��� ������ �����(���.) ���������
			foreach (var element in data)
			{
                if (newData.Count != windowWidth)//���� ������� ������ � ����
                {
                    sumElement += element.OriginalY;//��������� ���� ������� � ����������� ����������
					newElement = sumElement / (newData.Count + 1);//����������� ����������� ��������
				}
				else//����� ����������� ����������� (�� ������������ �������) �������� 
					newElement += (element.OriginalY - newData.Dequeue()) / windowWidth;
                var newDataResult = element.WithAvgSmoothedY(newElement);//������������ � ���������� ���� ��
				yield return newDataResult;//������� �������� ����� ����������
				newData.Enqueue(element.OriginalY);//���������� ����� �������� �������� � �������
			}
		}
	}
}