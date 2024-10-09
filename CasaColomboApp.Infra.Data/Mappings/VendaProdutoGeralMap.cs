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
            builder.Property(v => v.ProdutoDepositoId).HasColumnName("DEPOSITOID").IsRequired();

            builder.Property(v => v.NomeProduto).HasColumnName("NOMEPRODUTO");
            builder.Property(v => v.NomeDeposito).HasColumnName("NOMEDEPOSITO");
            builder.Property(v => v.Marca).HasColumnName("MARCA");

            builder.Property(v => v.UsuarioId).HasColumnName("USUARIOID").IsRequired();

            builder.Property(v => v.QuantidadeVendida).HasColumnName("QUANTIDADE").IsRequired();

            builder.Property(v => v.UploadRelatorioVenda).HasColumnName("DATAUPLOADVENDA");
            builder.Property(v => v.DataVenda).HasColumnName("DATAVENDA");
            builder.Property(v => v.DataVendaManual).HasColumnName("DATAVENDAMANUAL");
            builder.Property(v => v.CodigoSistema).HasColumnName("CODIGO").IsRequired();

            builder.HasOne(v => v.ProdutoDeposito).WithMany()
                .HasForeignKey(v => v.ProdutoDepositoId)
                .OnDelete(DeleteBehavior.Cascade);

           
          

        }
    }
}
