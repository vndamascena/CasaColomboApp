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
    public class QuantidadeProdutosDepositosMap : IEntityTypeConfiguration<QuantidadeProdutosDepositos>
    {
        public void Configure(EntityTypeBuilder<QuantidadeProdutosDepositos> builder)
        {
            builder.ToTable("QUANTIDADEPRODUTODEPOSITO");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("ID");

            builder.Property(p => p.CodigoSistema).HasColumnName("CODIGOSISTEMA").IsRequired();
            builder.Property(v => v.UsuarioId).HasColumnName("USUARIOID").IsRequired();
            builder.Property(l => l.NomeProduto).HasColumnName("NOMEPRODUTO");
            builder.Property(l => l.DataUltimaAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();
            builder.Property(l => l.Quantidade).HasColumnName("QUANTIDADE");
            builder.Property(l => l.ProdutoGeralId).HasColumnName("PRODUTOID").IsRequired();
            builder.Property(l => l.DataEntrada).HasColumnName("DATAENTRADA").IsRequired();

            builder.HasOne(p => p.ProdutoGeral) //LOTE TEM 1 PRODUTO
                  .WithMany(f => f.QuantidadeProdutoDeposito) //PRODUTO TEM N LOTES
                  .HasForeignKey(p => p.ProdutoGeralId) //Chave estrangeira
                  .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata

        }
    }
}
