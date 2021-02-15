using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlanetsWebApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanetsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SolarController : ControllerBase
    {
        
        private readonly ILogger<SolarController> _logger;

        public SolarController(ILogger<SolarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Planeta> Get()
        {
            List<Planeta> planetas = new List<Model.Planeta>();
            planetas = DataBase.getPlaneatas();
            return planetas;
        }

        [HttpGet]
        [Route("pordia")]
        public Pronostico GetDia(int dia=1)
        {
            bool lluvia = false;
            bool presionOptima = false;
            bool sequia = false;
            Pronostico p = new Pronostico();
            List<Planeta> planetas = new List<Model.Planeta>();
            planetas = DataBase.getPlaneatas();
            planetas = DataBase.calcularCoordenadas(planetas, dia);
            lluvia = DataBase.calcularLluvia(planetas);
            p.dia = dia;
            if (lluvia) { 
                p.pronostico = "Lluvioso";
                return p;
            }

            presionOptima = DataBase.calcularPresionOptima(planetas);
            if (presionOptima) { 
                p.pronostico = "Temperatura y Presion Optima";
                return p;
            }

            sequia = DataBase.calcularSequia(planetas);
            if (sequia) { 
                p.pronostico = "Sequia";
                return p;
            }
            p.pronostico = "sin pronostico";
            return p;
        }

        [HttpGet]
        [Route("dias")]
        public List<Pronostico> GetTodos(int dia = 1)
        {
            List<Pronostico> pro = new List<Pronostico>();
            for (int i=1;i<=dia;i++) {
                bool lluvia = false;
                bool presionOptima = false;
                bool sequia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                lluvia = DataBase.calcularLluvia(planetas);
                item.dia = i;
                if (lluvia) { 
                    item.pronostico = "Lluvioso";
                    item.diametro = DataBase.calcularDiametro(planetas);
                } else {
                    presionOptima = DataBase.calcularPresionOptima(planetas);
                    if (presionOptima) { 
                        item.pronostico = "Temperatura y Presion Optima"; 
                    } else {
                        sequia = DataBase.calcularSequia(planetas);
                        if (sequia) item.pronostico = "Sequia";
                    }
                }
                pro.Add(item);
            }
            return pro;
            
        }

        [HttpGet]
        [Route("lluvia")]
        public List<Pronostico> GetPeriodoLluvia(int dia=3650)
        {
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
                {
                bool lluvia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                lluvia = DataBase.calcularLluvia(planetas);
                item.dia = i;
                if (lluvia)
                {
                    item.pronostico = "Lluvioso";
                    item.diametro = DataBase.calcularDiametro(planetas);
                    pro.Add(item);
                }
              
                
            }
            return pro;
        }

        [HttpGet]
        [Route("lluvia/totaldias")]
        public int GetTotalPeriodoLluvia(int dia = 3650)
        {
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
            {
                bool lluvia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                lluvia = DataBase.calcularLluvia(planetas);
                item.dia = i;
                if (lluvia)
                {
                    item.pronostico = "Lluvioso";
                    item.diametro = DataBase.calcularDiametro(planetas);
                    pro.Add(item);
                }


            }
            return pro.Count();
        }
        [HttpGet]
        [Route("diamaslluvia")]
        public Pronostico GetPeriodomasLluvia(int dia = 3650)
        {
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
                {
                bool lluvia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                lluvia = DataBase.calcularLluvia(planetas);
                item.dia = i;
                if (lluvia)
                {
                    item.pronostico = "Lluvioso";
                    item.diametro = DataBase.calcularDiametro(planetas);
                    pro.Add(item);
                }


            }
            return pro.OrderByDescending(x => x.diametro).FirstOrDefault();
        }
        [HttpGet]
        [Route("presion")]
        public List<Pronostico> GetPeriodoPresion(int dia=3650)
        {
            //Las condiciones óptimas de presión y temperatura se dan cuando los tres planetas están alineados entre sí pero no están alineados con el sol.
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
                {
                
                bool presionOptima = false;
                bool sequia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                presionOptima = DataBase.calcularPresionOptima(planetas);
                if (presionOptima)
                {
                    item.dia = i;
                    item.pronostico = "Temperatura y Presion Optima";
                    pro.Add(item);
                }
                else
                {
                        sequia = DataBase.calcularSequia(planetas);
                        if (sequia) item.pronostico = "Sequia";
                }
                

            }
            return pro;
        }
        [HttpGet]
        [Route("presion/totaldias")]
        public int GetTotalPeriodoPresion(int dia = 3650)
        {
            //Las condiciones óptimas de presión y temperatura se dan cuando los tres planetas están alineados entre sí pero no están alineados con el sol.
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
            {

                bool presionOptima = false;
                bool sequia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                presionOptima = DataBase.calcularPresionOptima(planetas);
                if (presionOptima)
                {
                    item.dia = i;
                    item.pronostico = "Temperatura y Presion Optima";
                    pro.Add(item);
                }
                else
                {
                    sequia = DataBase.calcularSequia(planetas);
                    if (sequia) item.pronostico = "Sequia";
                }


            }
            return pro.Count();
        }
        [HttpGet]
        [Route("sequia")]
        public List<Pronostico> GetPeriodoSequia(int dia=3650)
        {
            //Cuando los tres planetas están alineados entre sí y a su vez alineados con respecto al sol, el sistema solar experimenta un período de sequía.
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
                {
                bool sequia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);
                
                    sequia = DataBase.calcularSequia(planetas);
                    if (sequia) {
                    item.dia = i;
                    item.pronostico = "Sequia";
                        pro.Add(item);
                    }
            }
            return pro;
        }
        [HttpGet]
        [Route("sequia/totaldias")]
        public int GetTotalPeriodoSequia(int dia = 3650)
        {
            //Cuando los tres planetas están alineados entre sí y a su vez alineados con respecto al sol, el sistema solar experimenta un período de sequía.
            List<Pronostico> pro = new List<Pronostico>();
            for (int i = 1; i <= dia; i++)
            {
                bool sequia = false;
                Pronostico item = new Pronostico();
                List<Planeta> planetas = new List<Model.Planeta>();
                planetas = DataBase.getPlaneatas();
                planetas = DataBase.calcularCoordenadas(planetas, i);

                sequia = DataBase.calcularSequia(planetas);
                if (sequia)
                {
                    item.dia = i;
                    item.pronostico = "Sequia";
                    pro.Add(item);
                }
            }
            return pro.Count();
        }
    }
}
