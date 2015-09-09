using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACO_Examen
{
    class Values
    {
        public double[,] tablevisibility { get; set; }
        public double[,] tablepheromones { get; set; }
        public int[] taboo { get; set; }
        public int[] bestsolutiontotal { get; set; }
        public int[] bestsolution { get; set; }
    }
}
