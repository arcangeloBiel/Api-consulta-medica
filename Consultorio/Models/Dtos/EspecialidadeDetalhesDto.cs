using System.Collections.Generic;

namespace Consultorio.Models.Dtos
{
    public class EspecialidadeDetalhesDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public List<PacienteDto> Profissionais { get; set; }
    }
}
