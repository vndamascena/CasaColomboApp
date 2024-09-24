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
    public class VendaProdutoGeralMap : IEntityTypeConfiguration<VendaProdutoGeral>
    {
        public void Configure(EntityTypeBuilder<VendaProdutoGeral> builder)
        {
            builder.ToTable("VENDAPRODUTOGERAL");
            builder.HasKey(v => v.VendaProdutoGeralId);
            builder.Property(v => v.VendaProdutoGeralId).HasColumnName("ID");
            builder.Property(v => v.QuantidadeProdutoID).HasColumnName("DEPOSITOID").IsRequired();

            builder.Property(v => v.NomeProduto).HasColumnName("NOMEPRODUTO");
            builder.Property(v => v.Marca).HasColumnName("MARCA");

            builder.Property(v => v.UsuarioId).HasColumnName("USUARIOID").IsRequired();

            builder.Property(v => v.QuantidadeVendida).HasColumnName("QUANTIDADE").IsRequired();

            builder.Property(v => v.DataVenda).HasColumnName("DATAVENDA").IsRequired();

            builder.Property(v => v.CodigoSistema).HasColumnName("CODIGO").IsRequired();

            builder.HasOne(v => v.QuantidadeProdutosDepositos).WithMany()
                .HasForeignKey(v => v.QuantidadeProdutoID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
