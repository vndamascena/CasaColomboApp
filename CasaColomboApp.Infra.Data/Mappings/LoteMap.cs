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
    public class LoteMap : IEntityTypeConfiguration<Lote>
    {

        public void Configure(EntityTypeBuilder<Lote> builder)
        {
            builder.ToTable("LOTE");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("ID");

            builder.Property(p => p.Codigo).HasColumnName("CODIGO").HasMaxLength(5);

            builder.Property(l => l.NumeroLote).HasColumnName("NUMERO LOTE").HasMaxLength(10);

            builder.Property(l => l.QuantidadeLote).HasColumnName("QUANTIDADE");

            builder.Property(l => l.Ala).HasColumnName("ALA");

            builder.Property(l => l.DataUltimaAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();

            builder.Property(l => l.ProdutoId).HasColumnName("PRODUTOID").IsRequired();

            builder.HasOne(p => p.Produto) //LOTE TEM 1 PRODUTO
                   .WithMany(f => f.Lote) //PRODUTO TEM N LOTES
                   .HasForeignKey(p => p.ProdutoId) //Chave estrangeira
                   .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata


        }
    }
}
