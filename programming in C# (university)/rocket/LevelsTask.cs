using System;
using System.Collections.Generic;

namespace func_rocket
{
    public class LevelsTask
    {
        static readonly Physics standardPhysics = new Physics();

        public static IEnumerable<Level> CreateLevels()
        {//������� ������� "Zero"
            yield return new Level("Zero",//�������� ������
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//��������� ��������� ������
                new Vector(600, 200),//��������� ��������� ������ ����
                (size, v) => Vector.Zero, //����������� ������� ����������
                standardPhysics);//�������� �����, ������� ����������� ������
                                 //������� ������� "Heave"
            yield return new Level("Heavy",//�������� ������
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//��������� ��������� ������
                new Vector(600, 200),//��������� ��������� ������ ����
                (size, v) => new Vector(0, 0.9), standardPhysics);//����������� ������� ����������;� �������� �����, ������� ����������� ������
            //������� ������� "Up"
            yield return new Level("Up",//�������� ������
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//��������� ��������� ������
                new Vector(700, 500),//��������� ��������� ������ ����
                (size, v) => new Vector(0, -300 / (size.Height - v.Y + 300.0)),//����������� ������� ����������
                standardPhysics);//�������� �����, ������� ����������� ������
            //������� ������� "WhiteHole"
            yield return new Level("WhiteHole",//�������� ������
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//��������� ��������� ������
                new Vector(600, 200),//��������� ��������� ������ ����
                (size, v) =>//����������� ������� ����������
                {
                    var vecToWhiteHole = new Vector(v.X - 600, v.Y - 200);//������ � ��(������ �����)
                    var vectLength = vecToWhiteHole.Length;//����� ������� � ��
                    return (vecToWhiteHole.Normalize() * 140 * vectLength / (Math.Pow(vectLength, 2) + 1));
                    //������� ����������� ������� ����������
                },
                standardPhysics);//�������� �����, ������� ����������� ������
            //������� ������� "BlackHole"
            yield return new Level("BlackHole",//�������� ������
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//��������� ��������� ������
                new Vector(600, 200),//��������� ��������� ������ ����
                (size, v) =>//����������� ������� ����������
                {
                    var vectorBlackHolePos = new Vector(400, 350);//��������� ������ ����������(��)
                    var lengToBlckHole = (vectorBlackHolePos - v).Length;//����� �������, ����������� �� � ���� ����� �� �����
                    var vector = new Vector(vectorBlackHolePos.X - v.X, vectorBlackHolePos.Y - v.Y);//����� �������, ����������� �� � ���� ����� �� �����
                    return new Vector(vector.X, vector.Y).Normalize() * 300 * lengToBlckHole /
                    (Math.Pow(lengToBlckHole, 2) + 1);//������� ����������� ������� ����������
                },
                standardPhysics);//�������� �����, ������� ����������� ������
            //������� ������� "BlackAndWhite"
            yield return new Level("BlackAndWhite",//�������� ������
                new Rocket(new Vector(200, 500), Vector.Zero, -0.5 * Math.PI),//��������� ��������� ������
                new Vector(600, 200),//��������� ��������� ������ ����
                (size, v) =>//����������� ������� ����������
                {
                    var vectorToWhiteHole = new Vector(v.X - 600, v.Y - 200);//������ ��-������ �����
                    var vectorToBlackHole = new Vector(400, 350);//������ ������� �����(��)
                    var lengToBlackHole = (vectorToBlackHole - v).Length;//����� �������, ����������� �� � ���� ����� �� �����
                    var lengVecToWhtHole = vectorToWhiteHole.Length;//����� ������� ��-������ �����
                    return (vectorToWhiteHole.Normalize() * 140 * lengVecToWhtHole / (Math.Pow(lengVecToWhtHole, 2) + 1) +
                    new Vector(vectorToBlackHole.X - v.X, vectorToBlackHole.Y - v.Y).Normalize() * 300 *
                    lengToBlackHole / (Math.Pow(lengToBlackHole, 2) + 1)) / 2;//������� ����������� ������� ����������
                }
                , standardPhysics);//�������� �����, ������� ����������� ������
        }
    }
}