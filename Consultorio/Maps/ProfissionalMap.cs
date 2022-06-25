using Consultorio.Map;
using Consultorio.Models.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Consultorio.Maps
{
    public class ProfissionalMap : BaseMap<Profissional>
    {
        public ProfissionalMap() : base("tb_profissional")
        {
                
        }
        public override void Configure(EntityTypeBuilder<Profissional> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Nome).HasColumnName("nome_profissional").HasColumnType("varchar(50)").IsRequired();
            builder.Property(x => x.Ativo).HasColumnName("ativo").HasDefaultValue(true);

            builder.HasMany(x => x.Especialidades).WithMany(x => x.Profissionais).UsingEntity<ProfissionalEspecialidade>(
                x => x.HasOne(p => p.Especialidade).WithMany().HasForeignKey(x => x.EspecialidadeId),
                x => x.HasOne(p => p.Profissionais).WithMany().HasForeignKey(x => x.ProfissionalId),
                x =>
                {
                    x.ToTable("tb_profissional_especialidade");

                    x.HasKey(p => new { p.EspecialidadeId, p.ProfissionalId });

                    x.Property(x => x.EspecialidadeId).HasColumnName("id_especialidade").IsRequired();
                    x.Property(x => x.ProfissionalId).HasColumnName("id_profissional").IsRequired();
                }
                );
        }
    }
}
