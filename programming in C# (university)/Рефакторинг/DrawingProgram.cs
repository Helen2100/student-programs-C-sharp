using System;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace RefactorMe
{
    class Painter
    {
        static float x, y;
        static Graphics graphicsArts;

        public static void Prepare ( Graphics graphicsNew )
        {
            graphicsArts = graphicsNew;
            graphicsArts.SmoothingMode = SmoothingMode.None;
            graphicsArts.Clear(Color.Black);
        }

        public static void SetPosition(float x0, float y0)
        {
            x = x0; 
            y = y0;
        }

        public static void Line(Pen width, double lengthLine, double corner)
        {
            //Делает шаг длиной lengthLine в направлении corner и рисует пройденную траекторию
            var x1 = (float)(x + lengthLine * Math.Cos(corner));
            var y1 = (float)(y + lengthLine * Math.Sin(corner));
            graphicsArts.DrawLine(width, x, y, x1, y1);
            x = x1;
            y = y1;
        }

        public static void Change(double lengthLine, double corner)
        {
            x = (float)(x + lengthLine * Math.Cos(corner)); 
            y = (float)(y + lengthLine * Math.Sin(corner));
        }

        public static void DrawSide(int sz, double constPi)
        {
            Painter.Line(Pens.Yellow, sz * 0.375f, constPi);
            Painter.Line(Pens.Yellow, sz * 0.04f * Math.Sqrt(2), constPi + Math.PI / 4);
            Painter.Line(Pens.Yellow, sz * 0.375f, constPi + Math.PI);
            Painter.Line(Pens.Yellow, sz * 0.375f - sz * 0.04f, constPi + Math.PI / 2);

            Painter.Change(sz * 0.04f, constPi - Math.PI);
            Painter.Change(sz * 0.04f * Math.Sqrt(2), constPi+3 * Math.PI / 4);
        }
    }

    public class ImpossibleSquare
    {
        public static void Draw(int breadth, int heightSide, double cornerTurn, Graphics graphicsArts)
        {
            // cornerTurn пока не используется, но будет использоваться в будущем
            Painter.Prepare(graphicsArts);

            var sz = Math.Min(breadth, heightSide);

            var diagonalLength = Math.Sqrt(2) * (sz * 0.375f + sz * 0.04f) / 2;
            var x0 = (float)(diagonalLength * Math.Cos(Math.PI / 4 + Math.PI)) + breadth / 2f;
            var y0 = (float)(diagonalLength * Math.Sin(Math.PI / 4 + Math.PI)) + heightSide / 2f;

            Painter.SetPosition(x0, y0);

            Painter.DrawSide(sz, 0);
            Painter.DrawSide(sz, -Math.PI / 2);
            Painter.DrawSide(sz, Math.PI);
            Painter.DrawSide(sz, Math.PI / 2);
        }
    }
}