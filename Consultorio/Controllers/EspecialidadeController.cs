using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using Consultorio.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Consultorio.Controllers
{

    [ApiController]
        [Route("api/[controller]")]
        public class EspecialidadeController: ControllerBase
    {
        private readonly IEspecialidadeRepository _repository;
        private readonly IMapper _mapper;

        public EspecialidadeController(IEspecialidadeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetEspecialidade()
        {
            var especialidade = await _repository.GetEspecialidade();

            return especialidade.Any() ? Ok(especialidade) : NotFound("Especialidade não encontrados");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEspecialidadelId(int id)
        {

            if (id <= 0) return BadRequest("Especialidade Invalido");

            var especialidade = await _repository.GetEspecialidadeById(id);

            var especialidadeRetorno = _mapper.Map<EspecialidadeDetalhesDto>(especialidade);

            return especialidadeRetorno != null ? Ok(especialidadeRetorno) : NotFound("Especialidade não encontrados");
        }

        [HttpPost]
        public async Task<IActionResult> PostEspecialidade(EspecialidadeAdicionarDto especialidade)
        {
            if (string.IsNullOrEmpty(especialidade.Nome)) return BadRequest("Dados Inválidos!");
            var especialidadeAdiconar = _mapper.Map<Especialidade>(especialidade);
            _repository.Add(especialidadeAdiconar);

            return await _repository.SaveChangesAsync()
                ? Ok("Especialidade adicionado com sucesso")
                : BadRequest("Erro ao salvar o especialidade");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidade(int id, EspecialidadeAdicionarDto especialidade)
        {

            if (id <= 0)
            {
                return BadRequest("Especialidade não encontrado");
            }

            var especialidadeBanco = await _repository.GetEspecialidadeById(id);
            var especialidadeAtualizado = _mapper.Map(especialidade, especialidadeBanco);
            _repository.Update(especialidadeAtualizado);

            return await _repository.SaveChangesAsync()
                 ? Ok("Especialidade Atualizado com sucesso")
                 : BadRequest("Erro ao atualizar o especialidade");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidade(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Especialidade inválido");
            }

            var especialidadeExclui = await _repository.GetEspecialidadeById(id);

            if (especialidadeExclui == null)
            {
                return NotFound("Especialdade não encontrado");
            }

            _repository.Delete(especialidadeExclui);

            return await _repository.SaveChangesAsync()
                 ? Ok("Especialidade Deletado com sucesso")
                 : BadRequest("Erro ao deletado o especialidade");
        }

    }
}
