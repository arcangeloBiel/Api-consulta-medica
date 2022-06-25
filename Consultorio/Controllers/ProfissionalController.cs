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
    public class ProfissionalController: ControllerBase
    {
        private readonly IProfissionalRepository _repository;
        private readonly IMapper _mapper;

        public ProfissionalController(IProfissionalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfissionais()
        {
            var pacientes = await _repository.GetProfissionais();

            return pacientes.Any() ? Ok(pacientes) : NotFound("Profissional não encontrados");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfissionalId(int id)
        {

            if (id <= 0) return BadRequest("Profissional Invalido");

            var profissional = await _repository.GetProfissionalById(id);

            var profissionalRetorno =  _mapper.Map<ProfissionalDetalhesDto>(profissional);

            return profissionalRetorno != null ? Ok(profissionalRetorno) : NotFound("Pacientes não encontrados");
        }

        [HttpPost]
        public async Task<IActionResult> PostProfissional(ProfissionalAdicionarDto profissional)
        {
            if (string.IsNullOrEmpty(profissional.Nome)) return BadRequest("Dados invàlidos");
            var profissionalAdicionar = _mapper.Map<Profissional>(profissional);
            _repository.Add(profissionalAdicionar);
            return await _repository.SaveChangesAsync() ? Ok("Profissional cadastrado com sucesso"): BadRequest("Erro ao adicionar o profissional");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfissional(int id, ProfissionalAdicionarDto profissional)
        {

            if (id <= 0)
            {
                return BadRequest("Profissional não encontrado");
            }

            var profissionalBanco = await _repository.GetProfissionalById(id);
            var profissionalAtualizado = _mapper.Map(profissional, profissionalBanco);
            _repository.Update(profissionalAtualizado);

            return await _repository.SaveChangesAsync()
                 ? Ok("Profissional Atualizado com sucesso")
                 : BadRequest("Erro ao atualizar o Profissional");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfissional(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Profissional inválido");
            }

            var profissionalExclui = await _repository.GetProfissionalById(id);

            if (profissionalExclui == null)
            {
                return NotFound("Profissional não encontrado");
            }

            _repository.Delete(profissionalExclui);

            return await _repository.SaveChangesAsync()
                 ? Ok("Profissional Deletado com sucesso")
                 : BadRequest("Erro ao deletado o Profissional");
        }

        [HttpPost("adicionar-profissional")]
        public async Task<IActionResult> PostProfissionalEspecialidade(ProfissionalEspecialidadeAdicionarDto profissional)
        {
            int profissionalId = profissional.ProfissionalId;
            int especialidadeId = profissional.EspecialidadeId;
            if (profissionalId <=0 || especialidadeId <=0) return BadRequest("Dados inválidos");

            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);
            if(profissionalEspecialidade != null) return Ok("Especialidade já cadastrada");

            var profissionalEspecialidadeAdicionar = new ProfissionalEspecialidade
            {
                ProfissionalId = profissionalId,
                EspecialidadeId = especialidadeId
            };
            //salvo em memoria
            _repository.Add(profissionalEspecialidadeAdicionar);

            //salvo no banco
            return await _repository.SaveChangesAsync()
                ? Ok("Profissional adicionado com sucesso")
                : BadRequest("Erro ao adicionado o Profissional");

        }

        [HttpDelete("{profissionalId}/deletar-especialidade/{especialidadeId}")]
        public async Task<IActionResult> DeleteProfissionalEspecialidade(int profissionalId, int especialidadeId)
        {
            if (profissionalId <= 0 || especialidadeId <= 0) return BadRequest("Dados inválidos");
            var profissionalEspecialidade = await _repository.GetProfissionalEspecialidade(profissionalId, especialidadeId);
            if (profissionalEspecialidade == null) return Ok("Especialidade não cadastrada");

            _repository.Delete(profissionalEspecialidade);

            return await _repository.SaveChangesAsync()
                 ? Ok("Especialidade do profissional Deletado com sucesso")
                 : BadRequest("Erro ao deletado Especialidade do Profissional");
        }

    }
}
