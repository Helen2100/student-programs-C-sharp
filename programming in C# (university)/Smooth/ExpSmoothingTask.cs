using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            var elementNumber = 0;//����� ��������
            var valuePreviousElement = 0.0;//�������� ����������� ��������
            foreach (var element in data)
            {
                elementNumber++;//�������� ����� ��������
                if (elementNumber == 1)//���� �� ������
                    valuePreviousElement = element.OriginalY;//�������� ��� ���� ����������(��� ����������� ���.��������)
                else//����� ��������� ���.�������� � ���� ����������(����������� ����� ��� ����������)
                    valuePreviousElement = alpha * element.OriginalY + (1 - alpha) * valuePreviousElement;
                var newElement = element.WithExpSmoothedY(valuePreviousElement);//������������ � ���������� ���� ����������
                yield return newElement;//������� �������� ����������
            }
        }
    }
}