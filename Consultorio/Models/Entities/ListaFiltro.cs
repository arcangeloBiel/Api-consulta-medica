using System.Collections.Generic;

namespace Consultorio.Models.Entities
{
    public class ListaFiltro
    {
     
        public int Codigo { get; set; }
        public string NomeRegra { get; set; }
        public int TipoRegra { get; set; }
        public List<string> ListaFamilia { get; set; }
    }
}
