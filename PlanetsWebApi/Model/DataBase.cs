using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetsWebApi.Model
{
    public static class DataBase
    {
        public static List<Planeta> getPlaneatas() {
            List<Planeta> lista = new List<Planeta>();
            Planeta A = new Planeta();
            A.Nombre = "Vulcanos";
            A.DistanciaDelSol = 1000;
            A.anguloDia = 5.0;
            A.sentidoHorario = false;
            A.ejeX = 0;
            A.ejeY = 1000;

            Planeta B = new Planeta();
            B.Nombre = "Ferangis";
            B.DistanciaDelSol = 500;
            B.anguloDia = 1.0;
            B.sentidoHorario = true;
            B.ejeX = 0;
            B.ejeY = 500;

            Planeta C = new Planeta();
            C.Nombre = "Betasoides";
            C.DistanciaDelSol = 2000;
            C.anguloDia = 3.0;
            C.sentidoHorario = true;
            C.ejeX = 0;
            C.ejeY = 2000;
            lista.Add(A);
            lista.Add(B);
            lista.Add(C);
            return lista;

        }

        public static Planeta moverPlaneta(Planeta planeta, int dias) {
            //x2+y2=DIstanciadelSol
            double AnguloInicial= 90;
            double AnguloMovimiento = (planeta.anguloDia * dias);
            int años = (int)Math.Floor(AnguloMovimiento / 360);
            if (años > 0)
            {
                AnguloMovimiento = AnguloMovimiento - (360 * años);
            }
            double AnguloAlpha = 0;
            if (planeta.sentidoHorario) {
                AnguloAlpha = AnguloInicial - AnguloMovimiento;
                if (AnguloAlpha < 0) {
                    AnguloAlpha = 360 - (AnguloAlpha * -1);
                }
            }
            if (!planeta.sentidoHorario)
            {
                AnguloAlpha = AnguloInicial + AnguloMovimiento;
                if (AnguloAlpha > 360)
                {
                    AnguloAlpha = 360 - AnguloAlpha;
                }
            }
           
            double X = (Math.Cos(AnguloAlpha)*planeta.DistanciaDelSol);
            double Y = (Math.Sin(AnguloAlpha)*planeta.DistanciaDelSol);
            planeta.ejeX = X;
            planeta.ejeY = Y;
            return planeta;
        }

        public static List<Planeta> calcularCoordenadas(List<Planeta> planetas,int dias) {
            List<Planeta> calculado = new List<Planeta>();
            foreach (Planeta x in planetas) {
                calculado.Add(moverPlaneta(x,dias));
            }
            return calculado;
        }
        public static bool calcularLluvia(List<Planeta> planetas)
        {
            Planeta A = planetas[0];
            Planeta B = planetas[1];
            Planeta C = planetas[2];

            double dx = B.ejeX - A.ejeX;
            double dy= B.ejeY - B.ejeY;
            double ex= C.ejeX - A.ejeY;
            double ey= C.ejeY - C.ejeX;

            double w1 = (ex * (A.ejeY - 0) + ey * (0 - A.ejeX))/(dx*ey-dy*ex);
            double w2 = (0-A.ejeY-w1*dy)/ey;
            if ((w1>=0.0)&&(w2>=0.0)&&((w1+w2)<=1.0)) {
                return true;
            }
            return false;
        }
        public static double calcularDiametro(List<Planeta> planetas) {
            Planeta A = planetas[0];
            Planeta B = planetas[1];
            Planeta C = planetas[2];
            //A.ejeX = Math.Abs(A.ejeX);
            //A.ejeY = Math.Abs(A.ejeY);
            //B.ejeX = Math.Abs(B.ejeX);
            //B.ejeY = Math.Abs(B.ejeY);
            //C.ejeX = Math.Abs(C.ejeX);
            //C.ejeY = Math.Abs(C.ejeY);

            double AB = Math.Sqrt(((A.ejeX - A.ejeY) * (A.ejeX - A.ejeY)) + ((B.ejeX - B.ejeY) * (B.ejeX - B.ejeY)));
            double BC = Math.Sqrt(((B.ejeX - B.ejeY) * (B.ejeX - B.ejeY)) + ((C.ejeX - C.ejeY) * (C.ejeX - C.ejeY)));
            double CA = Math.Sqrt(((C.ejeX - C.ejeY) * (C.ejeX - C.ejeY)) + ((A.ejeX - A.ejeY) * (A.ejeX - A.ejeY)));
            
            return AB + BC + CA;
        }
        public static bool calcularSequia(List<Planeta> planetas)
        {
            
            Planeta A = planetas[0];
            Planeta B = planetas[1];
            Planeta C = planetas[2];
            if (vectorAlineado(A.ejeX, A.ejeY, B.ejeX, B.ejeY, C.ejeX, C.ejeY))
            {
                if (vectorAlineado(A.ejeX, A.ejeY, B.ejeX, B.ejeY, 0, 0))
                {
                    return true;
                }
            }
            return false;

        }
        public static bool calcularPresionOptima(List<Planeta> planetas)
        {
            Planeta A = planetas[0];
            Planeta B = planetas[1];
            Planeta C = planetas[2];
            if (vectorAlineado(A.ejeX, A.ejeY, B.ejeX, B.ejeY, C.ejeX, C.ejeY))
            {
                if (!vectorAlineado(A.ejeX, A.ejeY, 0,0,B.ejeX, B.ejeY)) {
                    return true;
                }
            }
            return false;
        }
        public static bool vectorAlineado(double Ax, double Ay, double Bx, double By, double Cx, double Cy) {
            double vecABx = Bx - Ax;
            double vecABy = By - Ay;
            double vecBCx = Cx-Bx;
            double vecBCy = Cy-By;

            double ProporcionX = vecABx / vecBCx;
            double ProporcionY = vecABy / vecBCy;
            if (Math.Round(ProporcionX,2) == Math.Round(ProporcionY,2))
            {
                return true;
            }
            else {
                return false;
            }

        }
    }
}
