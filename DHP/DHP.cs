using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DHP
{
    class DHP
    {
        private double[,] matrix;
        private int[] x, y;
        private Dictionary<int, double> gvxDiff, gvyDiff;

        public DHP()
        {
            x = initCordinate("x");
            y = initCordinate("y");
            if (x == null || y == null || x.Length != y.Length)
            {
                throw new Exception("Incorrect cordinate!");
            }
            matrix = new double[x.Length, y.Length];
            for (int v = 0; v < x.Length; v++)
            {
                for (int n = 0; n < y.Length; n++)
                {
                    matrix[v, n] = this.cas(2 * Math.PI * v * n / x.Length);
                }
            }
        }

        private int[] initCordinate(string axis)
        {

            Console.WriteLine("Enter {0}n: ", axis.ToUpper());
            var cordString = Console.ReadLine();
            var cordStringSplited = cordString.Split(',');
            int[] cordinates = new int[cordStringSplited.Length];
            for (int i = 0; i < cordStringSplited.Length; i++)
            {
                cordinates[i] = int.Parse(cordStringSplited[i]);
            }
            return cordinates;
        }
        public void PrintMatrix()
        {
            Console.WriteLine();
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x.Length; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public void Encode(int Mx, int My)
        {
            if (Mx < 0 || Mx >= x.Length || My < 0 || My >= x.Length)
            {
                throw new Exception("Incorrect Mx or My");
            }
            var gvx = new double[x.Length];
            var gvy = new double[x.Length];

            for (int i = 0; i < x.Length; i++)
            {
                gvx[i] = 0;
                gvy[i] = 0;
                for (int j = 0; j < x.Length; j++)
                {
                    gvx[i] += matrix[i, j] * x[j];
                    gvy[i] += matrix[i, j] * y[j];
                }
                gvx[i] = Math.Round(gvx[i], 2);
                gvy[i] = Math.Round(gvy[i], 2);
            }

            Console.WriteLine("gvx: {" + join(gvx, ", ") + "}\ngvy: {" + join(gvy, ", ") + "}");
            gvxDiff = getDiffGv(gvx, Mx);
            gvyDiff = getDiffGv(gvy, My);
            Console.WriteLine("gvx': {" + join(gvxDiff.ToArray(), ", ") + "}\ngvy': {" + join(gvyDiff.ToArray(), ", ") + "}");
        }

        public void Decode()
        {
            var xDecode = decodeByGv(gvxDiff);
            var yDecode = decodeByGv(gvyDiff);

            Console.WriteLine("{x} = { " + join(xDecode, ", ") + "  }\n{y} = { " + join(yDecode, ", ") + " }");

        }

        public void PrintOriginalPicture()
        {
            Console.WriteLine("original picture");
            printPicture(x, y);
        }

        public void PrintDecodePicture()
        {
            Console.WriteLine("decode picture");
            printPicture(decodeByGv(gvxDiff), decodeByGv(gvyDiff));
        }

        private void printPicture(int[] xCoordinate, int[] yCoordinate)
        {
            var xMax = xCoordinate.Max();
            var yMax = yCoordinate.Max();
            for (int i = 0; i <= xMax; i++)
            {
                for (int j = 0; j <= yMax; j++)
                    Console.Write(isPoint(i, xCoordinate, j, yCoordinate) ? '*' : ' ');
                Console.WriteLine();
            }
        }

        private bool isPoint(int x, int[] xCoordinate, int y, int[] yCoordinate)
        {
            for (int i = 0; i < xCoordinate.Length; i++)
                if (xCoordinate[i] == x && yCoordinate[i] == y)
                    return true;
            return false;
        }

        private int[] decodeByGv(Dictionary<int, double> gv)
        {
            int[] result = new int[x.Length];
            double[] decode = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {

                foreach (var item in gv)
                {
                    decode[i] += matrix[i, item.Key] * item.Value;
                }

                decode[i] /= x.Length;
            }
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = (int)Math.Round(decode[i]);
            }
            return result;
        }
        private string join(Array array, string spliter)
        {
            string result = "";
            foreach (var item in array)
            {
                result += item + spliter;
            }
            return result.Substring(0, result.Length - spliter.Length);
        }

        private Dictionary<int, double> getDiffGv(double[] gv, int count)
        {
            Dictionary<int, double> result = new Dictionary<int, double>();
            var copy = gv;
            while (result.Count < count)
            {
                double max = getMax(copy);
                for (int i = 0; i < gv.Length; i++)
                {
                    if (max == gv[i])
                    {
                        result.Add(i, max);
                    }
                }
                copy = copy.Where(item => item != max).ToArray();
            }

            return result;
        }

        private double getMax(double[] gv)
        {
            double max = gv[0];
            foreach (var item in gv)
            {
                if (Math.Abs(item) > Math.Abs(max))
                {
                    max = item;
                }
            }

            return max;
        }
        private double cas(double value)
        {
            return Math.Round(Math.Sin(value) + Math.Cos(value), 2);
        }
    }
}
