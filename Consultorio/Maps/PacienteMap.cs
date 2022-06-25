using Consultorio.Map;
using Consultorio.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Maps
{
    public class PacienteMap : BaseMap<Paciente>
    {
        public PacienteMap() : base("tb_paciente")
        {}
        public override void Configure(EntityTypeBuilder<Paciente> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("nome_paciente").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Cpf).HasColumnName("cpf_paciente").HasColumnType("varchar(11)").IsRequired();
            builder.Property(x => x.Email).HasColumnName("email_paciente").HasColumnType("varchar(70)");
            builder.Property(x => x.Celular).HasColumnName("celular_paciente").HasColumnType("varchar(25)").IsRequired();

        }
    }
}
