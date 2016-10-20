// Nie wyslalem odpowiedzi

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace LightBulbs
{
    public class LightBulbs : ILightBulbs
    {
        public new static ILightBulbs GetInstance()
        {
            return new LightBulbs();
        }

        public override int CountLightsOn(bool[,] lightsBoard, int s)
        {
            lightsBoard = NextTurn(lightsBoard);
            return CountThem(lightsBoard);
        }

        private bool[,] NextTurn(bool[,] lightsBoard)
        {
            var firstLength = lightsBoard.GetLength(0);
            var secondLength = lightsBoard.GetLength(1);

            var newArray = new bool[firstLength, secondLength];

            //for (int i = 0; i < firstLength; i++)
            //{
            //    for (int j = 0; j < secondLength; j++)
            //    {
            //        GetNeighbours(lightsBoard, i, j);
            //    }
            //}

            return newArray;
        }

        private IEnumerable<Point> GetNeighbours(int i, int j)
        {
            yield return new Point(i - 1, j - 1);
            yield return new Point(i - 1, j + 1);
            yield return new Point(i - 1, j + 1);
            yield return new Point(i - 1, j + 1);
        }

        class Point
        {
            int I;
            int J;

            public Point(int x, int j)
            {
                I = x;
                J = j;
            }
        }

        private int CountThem(bool[,] lightsBoard)
        {
            int sum = 0;
            foreach (bool b in lightsBoard)
            {
                if (b)
                {
                    sum++;
                }
            }

            return sum;
        }
    }
}