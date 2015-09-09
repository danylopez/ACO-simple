using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACO_Examen
{
    class ACO
    {
        public Values initializearrays(int[,] table)
        {
            Values val = new Values();
            val.tablevisibility = new double[6, 15];
            val.tablepheromones = new double[6, 15];
            int i, j;
           
            for (i = 0; i < 6; i++)
            {
                for (j = 0; j < 15; j++)
                {
                    val.tablevisibility[i, j] = 1 / (double)table[i, j];
                    val.tablepheromones[i, j] = 0.01;
                }
            }
            return val;
        }

        public Values buildsolution(double alpha, double beta, Values val)
        {
            int i, j;
            double probability = 0, numerator = 0, denominator = 0, rndvalue;
            Random rnd = new Random();
            val.taboo = new int[15];
            double[] range = new double[7] { 0, 0, 0, 0, 0, 0, 0 };

            for (j = 0; j < 15; j++)
            {
                range[0] = 0;
                for (i = 0; i < 6; i++)
                {
                    numerator = caculatenumerator(i, j, alpha, beta, val);
                    denominator = caculatedenominator(i, j, alpha, beta, val);
                    probability = numerator / denominator;
                    range[i+1] = range[i] + probability;
                }
                range[6] = 1;

                rndvalue = rnd.NextDouble();
                for (i = 1; i < 7; i++)
                {
                    if (rndvalue < range[i] && rndvalue > range[i - 1])
                    {
                        val.taboo[j] = i-1;
                        break;
                    }
                }
            }
            return val;
        }

        public double caculatenumerator(int i, int j, double alpha, double beta, Values val)
        {
            double numerator = 0;
            numerator = ((Math.Pow(val.tablepheromones[i, j], alpha)) * (Math.Pow(val.tablevisibility[i, j], beta)));
            return numerator;
        }

        public double caculatedenominator(int i, int j, double alpha, double beta, Values val)
        {
            double denominator = 0;
            for (int k = 0; k < 6; k++)
            {
                denominator += ((Math.Pow(val.tablepheromones[i, j], alpha)) * (Math.Pow(val.tablevisibility[i, j], beta)));
            }
            return denominator;
        }

        public Values updatepheromone(double rho, Values val)
        {
            double numerator = 0, denominator = 0;
            for (int i = 0; i < 15; i++)
            {
                numerator = val.tablepheromones[val.bestsolution[i], i];
                denominator = val.tablepheromones[val.bestsolutiontotal[i], i];
                val.tablepheromones[val.bestsolutiontotal[i], i] = (1 - rho) * val.tablepheromones[val.bestsolutiontotal[i], i] + denominator / numerator;
            }
            return val;
        }

    }
}
