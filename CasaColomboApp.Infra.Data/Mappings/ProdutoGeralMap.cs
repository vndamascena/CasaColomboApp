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
    public class ProdutoGeralMap : IEntityTypeConfiguration<ProdutoGeral>
    {
        public void Configure(EntityTypeBuilder<ProdutoGeral> builder)
        {
            builder.ToTable("PRODUTOGERAL");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id).HasColumnName("ID");

            builder.Property(p => p.CodigoSistema).HasColumnName("CODIGOSISTEMA").IsRequired();
            builder.Property(p => p.NomeProduto).HasColumnName("NOME").HasMaxLength(255).IsRequired();
            builder.Property(p => p.QuantidadeProd).HasColumnName("QUANTIDADE");
            builder.Property(p => p.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();
            builder.Property(p => p.MarcaProduto).HasColumnName("MARCA").HasMaxLength(50);
            builder.Property(p => p.DataHoraAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();
            builder.Property(p => p.CategoriaId).HasColumnName("IDCATEGORIA").IsRequired();
            builder.Property(p => p.FornecedorGeralId).HasColumnName("IDFORNECEDOR").IsRequired();
           




            builder.Property(p => p.ImagemUrlGeral).HasColumnName("IMAGEMURL");




            #region Relacionamentos

            builder.HasOne(p => p.Categoria) //Produto TEM 1 Categoria
                .WithMany(c => c.ProdutosGeral) //Categoria TEM N Produtos
                .HasForeignKey(p => p.CategoriaId) //Chave estrangeira
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Fornecedor) //Produto TEM 1 Fornecedor
               .WithMany(f => f.ProdutosGeral) //Fornecedor TEM N Produtos
               .HasForeignKey(p => p.FornecedorGeralId) //Chave estrangeira
               .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.ProdutoDeposito) // ProdutoGeral TEM N ProdutoDeposito
                .WithOne(pd => pd.ProdutoGeral) // ProdutoDeposito TEM 1 ProdutoGeral
                .HasForeignKey(pd => pd.ProdutoGeralId) // Chave estrangeira em ProdutoDeposito
                .OnDelete(DeleteBehavior.Cascade); // Define o comportamento de exclusão

            #endregion
        }
    }
}
