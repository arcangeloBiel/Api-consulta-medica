using AutoMapper;
using Consultorio.Models.Dtos;
using Consultorio.Models.Entities;
using System.Linq;

namespace Consultorio.Helpers
{
    public class ConsultorioProfile: Profile
    {
        public ConsultorioProfile()
        {
            //mapeamento de consultas          
            CreateMap<Consulta, ConsultaDto>()
                .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade.Nome))
                 .ForMember(dest => dest.Profissional, opt => opt.MapFrom(src => src.Profissional.Nome));
            CreateMap<Consulta, ConsultaDetalhesDto>();
            CreateMap<ConsultaAdicionarDto, Consulta>();
            CreateMap<ConsultaAtualizarDto, Consulta>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //Mapeamento de paciente
            CreateMap<PacienteAdicionarDto, Paciente>();          
            CreateMap<Paciente, PacienteDetalhesDto>();
            CreateMap<PacienteAtualizarDto, Paciente>().ForAllMembers(opts => opts.Condition((src,dest,srcMember) => srcMember != null));
            CreateMap<Paciente, PacienteDto>();

            //mapeamento do profissional
            CreateMap<Profissional, ProfissionalDetalhesDto>()
                .ForMember(dest => dest.TotalConsultas, opt => opt.MapFrom(src => src.Consultas.Count()))
                .ForMember(dest => dest.Especialidades, opt=> opt.MapFrom(src => src.Especialidades.Select(x => x.Nome).ToArray()));

            CreateMap<ProfissionalAdicionarDto, Profissional>();
            CreateMap<Profissional, ProfissionalDto>();

            CreateMap<ProfissionalAtualizarDto, Profissional>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            //mapeamento do especialidade
            CreateMap<Especialidade, EspecialidadeDetalhesDto>()
                 .ForMember(dest => dest.Profissionais, opt => opt.MapFrom(src => src.Profissionais));
            CreateMap<EspecialidadeAdicionarDto, Especialidade>();
            CreateMap<Especialidade, EspecialidadeDto>();


        }

    }
}
