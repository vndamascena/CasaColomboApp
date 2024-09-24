using CasaColomboApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Infra.Data.Mappings
{
    internal class OcorrenciaMap : IEntityTypeConfiguration<Ocorrencia>
    {
        public void Configure(EntityTypeBuilder<Ocorrencia> builder)
        {
            builder.ToTable("OCORRENCIA");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("ID");

            builder.Property(t => t.CodProduto).HasColumnName("CODPRODUTO").HasMaxLength(7);

            builder.Property(p => p.Ativo).HasColumnName("ATIVO").IsRequired();

            builder.Property(t => t.Produto).HasColumnName("PRODUTO").HasMaxLength(70);

            builder.Property(t => t.FornecedorGeralId).HasColumnName("FORNECEDO").HasMaxLength(70);

            builder.Property(t => t.NumeroNota).HasColumnName("NUMERONOTA");

            builder.Property(t => t.UsuarioId).HasColumnName("USUARIOID");

            builder.Property(o => o.TipoOcorrenciaId).HasColumnName("TIPOOCORRENCIA");
            builder.Property(o => o.LojaId).HasColumnName("LOJA");

            builder.Property(o => o.Observacao).HasColumnName("OBSERVACAO");


            builder.Property(o => o.DataTime).HasColumnName("DATA");

            builder.HasOne(p => p.TipoOcorrencia) //Ocorrenca TEM 1 Tipo ocorrencia
             .WithMany(f => f.Ocorrencia) //tipo ocorrencia TEM N Ocorrencia
             .HasForeignKey(p => p.TipoOcorrenciaId) //Chave estrangeira
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.FornecedorGeral) //Ocorrenca TEM 1 Tipo ocorrencia
            .WithMany(f => f.Ocorrencia) //tipo ocorrencia TEM N Ocorrencia
            .HasForeignKey(p => p.FornecedorGeralId) //Chave estrangeira
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Loja) //Ocorrenca TEM 1 Tipo ocorrencia
            .WithMany(f => f.Ocorrencia) //tipo ocorrencia TEM N Ocorrencia
            .HasForeignKey(p => p.LojaId) //Chave estrangeira
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
