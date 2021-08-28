using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {//отправить ЭТО
            double x = 0;
            double y = 0;

            x = PointX(shoulder, x, Manipulator.UpperArm);
            y = PointY(shoulder, y, Manipulator.UpperArm);
            var elbowPos = new PointF((float)x, (float)y);

            float n = 0;
            if (x == 0)
                n = 1;
            if (y == 150)
                n = 2;
            var v = 0;
            if (Math.Sqrt(x * x + y * y) == Manipulator.UpperArm)
                v = 1;

            x = PointX(shoulder + elbow + Math.PI, x, Manipulator.Forearm);
            y = PointY(shoulder + elbow + Math.PI, y, Manipulator.Forearm);
            var wristPos = new PointF((float)x, (float)y);

            if (x == 120)
                n = 3;
            if (y == 150)
                n = 4;
            var f = Math.Sqrt((x - elbowPos.X) * (x - elbowPos.X) + (y - elbowPos.Y) * (y - elbowPos.Y));
            if (Math.Sqrt((x - elbowPos.X) * (x - elbowPos.X) + (y - elbowPos.Y) * (y - elbowPos.Y)) == Manipulator.Forearm)
                v = 2;

            x = PointX(wrist + shoulder + elbow + 2 * Math.PI, x, Manipulator.Palm); 
            y = PointY(wrist + shoulder + elbow + 2 * Math.PI, y, Manipulator.Palm);
            var palmEndPos = new PointF((float)x, (float)y);

            if (x == 180)
                n = 5;
            if (y == 150)
                n = 6;
            if (Math.Sqrt((x - wristPos.X) * (x - wristPos.X) + (y - wristPos.Y) * (y - wristPos.Y)) == Manipulator.Palm)
                v = 3;

            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static double PointX(double corner, double x, double shoulderLength)
        {
            var cos = Math.Cos(corner);
            var cosR = Math.Round(Math.Cos(corner));
            var l = shoulderLength;
            x += Math.Round(Math.Cos(corner)) * shoulderLength;
            return x;
        }

        private static double PointY(double corner, double y, double shoulderLength)
        {
            var sin = Math.Sin(corner);
            var sinR = Math.Round(Math.Sin(corner));
            var l = shoulderLength;
            y += Math.Round(Math.Sin(corner)) * shoulderLength;
            return y;
        }
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(Manipulator.UpperArm, Math.Sqrt(joints[0].X * joints[0].X + joints[0].Y * joints[0].Y));
            Assert.AreEqual(Manipulator.Forearm, Math.Sqrt((joints[1].X - joints[0].X) * (joints[1].X - joints[0].X) + (joints[1].Y - joints[0].Y) * (joints[1].Y - joints[0].Y)));
            Assert.AreEqual(Manipulator.Palm, Math.Sqrt((joints[2].X - joints[1].X) * (joints[2].X - joints[1].X) + (joints[2].Y - joints[1].Y) * (joints[2].Y - joints[1].Y)));
        }
    }
}