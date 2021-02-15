using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetsWebApi.Model
{
    public class Planeta
    {
        public string Nombre { get; set; }
        public int DistanciaDelSol { get; set; }

        public double anguloDia { get; set; }
        public bool sentidoHorario { get; set; }

        public double ejeX { get; set; }
        public double ejeY { get; set; }
    }
}
