﻿using CasaColomboApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Infra.Data.Mappings
{
    internal class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTO");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("ID");

            builder.Property(p => p.Codigo).HasColumnName("CODIGO").HasMaxLength(150).IsRequired();

            builder.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();

            builder.Property(p => p.Marca).HasColumnName("MARCA");

            builder.Property(p => p.Quantidade).HasColumnName("QUANTIDADE").IsRequired();

            builder.Property(p => p.Lote).HasColumnName("LOTE");

            builder.Property(p => p.Descricao).HasColumnName("DESCRICAO").HasMaxLength(500).IsRequired();

            builder.Property(p => p.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();

            builder.Property(p => p.DataHoraAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();

            builder.Property(p => p.Ativo).HasColumnName("ATIVO").IsRequired();

            builder.Property(p => p.CategoriaId).HasColumnName("CATEGORIAID").IsRequired();

            builder.Property(p => p.FornecedorId).HasColumnName("FORNECEDORID").IsRequired();

            builder.Property(p => p.DepositoId).HasColumnName("DEPOSITOID").IsRequired();

            builder.Property(p => p.ImagemUrl).HasColumnName("IMAGEMURL").HasMaxLength(255);



            #region Relacionamentos

            builder.HasOne(p => p.Categoria) //Produto TEM 1 Categoria
                .WithMany(c => c.Produtos) //Categoria TEM N Produtos
                .HasForeignKey(p => p.CategoriaId) //Chave estrangeira
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Fornecedor) //Produto TEM 1 Fornecedor
               .WithMany(f => f.Produtos) //Fornecedor TEM N Produtos
               .HasForeignKey(p => p.FornecedorId) //Chave estrangeira
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Deposito) //Produto TEM 1 Deposito
               .WithMany(f => f.Produtos) //Deposito TEM N Produtos
               .HasForeignKey(p => p.DepositoId) //Chave estrangeira
               .OnDelete(DeleteBehavior.Restrict);


            #endregion

        }
    }
}
