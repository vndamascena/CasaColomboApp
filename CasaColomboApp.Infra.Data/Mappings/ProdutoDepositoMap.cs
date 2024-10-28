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
    public class ProdutoDepositoMap : IEntityTypeConfiguration<ProdutoDeposito>
    {


        public void Configure(EntityTypeBuilder<ProdutoDeposito> builder)
        {
            builder.ToTable("PRODUTODEPOSITO");

            // Definir 'Id' como chave primária única
            builder.HasKey(pd => pd.Id);
            builder.Property(l => l.Id).HasColumnName("ID");
            // Definir as propriedades
            builder.Property(pd => pd.Quantidade).HasColumnName("QUANTIDADE").IsRequired();
            builder.Property(v => v.UsuarioIdCadastro).HasColumnName("USUARIOIDCADASTRO").IsRequired();
            builder.Property(v => v.UsuarioIdAlteracao).HasColumnName("USUARIOIDALTERACAO");
            builder.Property(pd => pd.NomeDeposito).HasColumnName("NOMEDEPOSITO").IsRequired();
            builder.Property(pd => pd.ProdutoGeralId).HasColumnName("PRODUTOGERALID").IsRequired();
            builder.Property(pd => pd.DepositoId).HasColumnName("DEPOSITOIID").IsRequired();
            builder.Property(p => p.CodigoSistema).HasColumnName("CODIGOSISTEMA").IsRequired();
            builder.Property(p => p.NomeProduto).HasColumnName("NOMEPRODUTO").IsRequired();
            builder.Property(v => v.Marca).HasColumnName("MARCA");
            builder.Property(l => l.QtdEntrada).HasColumnName("QTDENTRADA");
            builder.Property(l => l.DataEntrada).HasColumnName("DATAENTRADA");
            builder.Property(l => l.DataUltimaAlteracao).HasColumnName("DATAHORAALTERACAO");

            // Mapeamento de relacionamentos
            builder.HasOne(pd => pd.ProdutoGeral)
                .WithMany(pg => pg.ProdutoDeposito)
                .HasForeignKey(pd => pd.ProdutoGeralId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pd => pd.Deposito)
                .WithMany(d => d.ProdutoDepositos)
                .HasForeignKey(pd => pd.DepositoId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}

