using Consultorio.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListaFiltroController: ControllerBase
    {
        List<ListaFiltro> listaFiltros = new List<ListaFiltro>();
        public ListaFiltroController()
        {
            //listaFiltros.Add(new ListaFiltro { Codigo = 1, TipoRegra = 2});
       

        }

        [HttpGet]
        public async Task<IActionResult> ListaFiltro()
        {

            var lista = new List<ListaFiltro>();
            var familias = new List<string>();
            familias.Add("Maculino");
            familias.Add("femenino");
            lista.Add(new ListaFiltro
            {
                Codigo = 1,
                NomeRegra = "Encarte",
                TipoRegra = 1,
                ListaFamilia = familias
            });

            familias = new List<string>();
            familias.Add("Langerie");
            familias.Add("outro");
            lista.Add(new ListaFiltro
            {
                Codigo = 2,
                NomeRegra = "Promocao",
                TipoRegra = 78,
                ListaFamilia = familias
            });

            familias = new List<string>();
            familias.Add("boco");
            familias.Add("patricia");
            lista.Add(new ListaFiltro
            {
                Codigo = 3,
                NomeRegra = "Basico",
                TipoRegra = 8,
                ListaFamilia = familias
            });

            return  Ok(lista);
        }
    }
}
