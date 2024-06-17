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
    internal class BaixaOcorrenciamMap : IEntityTypeConfiguration<BaixaOcorrencia>
    {
        public void Configure(EntityTypeBuilder<BaixaOcorrencia> builder)
        {

            builder.ToTable("BAIXAOCORRENCIA");

            builder.HasKey(v => v.BaixaId);

            builder.Property(v => v.BaixaId).HasColumnName("ID");

            builder.Property(v => v.TipoOcorrenciaId).HasColumnName("TIPOOCORRENCIAID").IsRequired();

            builder.Property(v => v.CodProduto).HasColumnName("CODPRODUTO");

            builder.Property(v => v.Produto).HasColumnName("PRODUTO");
            builder.Property(v => v.Fornecedo).HasColumnName("NUMEROLOTE");

            builder.Property(v => v.NumeroNota).HasColumnName("NUMERONOTA");

            builder.Property(v => v.UsuarioId).HasColumnName("USUARIOID");
            builder.Property(v => v.Observacao).HasColumnName("OBSERVACAO");
            builder.Property(v => v.DataTime).HasColumnName("DATA");


            builder.HasOne(v => v.Ocorrencia) // VENDA TEM 1 LOTE
                  .WithMany() // LOTE TEM N VENDAS
                  .HasForeignKey(v => v.TipoOcorrenciaId) // Chave estrangeira
                  .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata
        }
    }
}

