using System;

namespace func_rocket
{
    public class ControlTask
    {
        public static Turn ControlRocket(Rocket rocket, Vector target)
        {
            var angle = 0.0;

            //������, ������������ ������ � �����(��)
            var vectorDistance = new Vector(target.X - rocket.Location.X,
                                      target.Y - rocket.Location.Y);

            var firstAngle = vectorDistance.Angle - rocket.Direction;//���� ����� �������� ������ � ��
            var moduleFirstAngle = Math.Abs(vectorDistance.Angle - rocket.Direction);//������ ����� ����� �������� ������ � ��
            
            var secondAngle = vectorDistance.Angle - rocket.Velocity.Angle;//���� ����� �������� �������� ������ � ��
            var moduleSecondAngle = Math.Abs(vectorDistance.Angle - rocket.Velocity.Angle);//������ ����� ����� �������� �������� ������ � ��

            if (moduleFirstAngle < 0.5 || moduleSecondAngle < 0.5)//���� ���� �� ������� ������ ������ 0,5
                angle = (firstAngle + secondAngle) / 2;//�� �������� ���� ����� �������� ����� �����
            else//�����
                angle = firstAngle;//�������� ���� ����� ���� ����� �������� ������ � ��

            if (angle != 0)//���� �������� ���� �� ����� 0
            {
                if (angle < 0) return Turn.Left;//���� ���� ������ 0, �� ��������� ������� �� ����
                else return Turn.Right;//���� ���� ������ 0, �� ��������� ������� �� �����
            }
            else//���� ���� ����� 0
                return Turn.None;//�� �� �������� ����������� �������
        }
    }
}