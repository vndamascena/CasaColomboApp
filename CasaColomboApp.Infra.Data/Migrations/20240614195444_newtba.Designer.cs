﻿// <auto-generated />
using System;
using CasaColomboApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CasaColomboApp.Infra.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240614195444_newtba")]
    partial class newtba
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.BaixaOcorrencia", b =>
                {
                    b.Property<int>("BaixaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BaixaId"));

                    b.Property<int>("CodProduto")
                        .HasColumnType("int")
                        .HasColumnName("CODPRODUTO");

                    b.Property<DateTime>("DataTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATA");

                    b.Property<string>("Fornecedo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NUMEROLOTE");

                    b.Property<string>("NumeroNota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NUMERONOTA");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OBSERVACAO");

                    b.Property<int?>("OcorrenciaId")
                        .HasColumnType("int");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PRODUTO");

                    b.Property<int>("TipoOcorrenciaId")
                        .HasColumnType("int")
                        .HasColumnName("TIPOOCORRENCIAID");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USUARIOID");

                    b.HasKey("BaixaId");

                    b.HasIndex("OcorrenciaId");

                    b.HasIndex("TipoOcorrenciaId");

                    b.ToTable("BAIXAOCORRENCIA", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Categoria", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("DataHoraAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORAALTERACAO");

                    b.Property<DateTime>("DataHoraCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORACADASTRO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("CATEGORIA", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Deposito", b =>
                {
                    b.Property<int>("DepositoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepositoId"));

                    b.Property<string>("NomeDeposito")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)")
                        .HasColumnName("NOMEDEPOSITO");

                    b.HasKey("DepositoId");

                    b.HasIndex("NomeDeposito")
                        .IsUnique();

                    b.ToTable("DEPOSITO", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Fornecedor", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("CNPJ");

                    b.Property<DateTime>("DataHoraAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORAALTERACAO");

                    b.Property<DateTime>("DataHoraCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORACADASTRO");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.ToTable("FORNECEDOR", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Lote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Ala")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ALA");

                    b.Property<string>("Codigo")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)")
                        .HasColumnName("CODIGO");

                    b.Property<DateTime>("DataUltimaAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORAALTERACAO");

                    b.Property<string>("NomeProduto")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NOMEPRODUTO");

                    b.Property<string>("NumeroLote")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("NUMERO LOTE");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int")
                        .HasColumnName("PRODUTOID");

                    b.Property<int>("QuantidadeLote")
                        .HasColumnType("int")
                        .HasColumnName("QUANTIDADE");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("LOTE", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Ocorrencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CodProduto")
                        .HasMaxLength(7)
                        .HasColumnType("int")
                        .HasColumnName("CODPRODUTO");

                    b.Property<DateTime>("DataTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATA");

                    b.Property<string>("Fornecedo")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("FORNECEDO");

                    b.Property<string>("NumeroNota")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NUMERONOTA");

                    b.Property<string>("Observacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("OBSERVACAO");

                    b.Property<string>("Produto")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("PRODUTO");

                    b.Property<int>("TipoOcorrenciaId")
                        .HasColumnType("int")
                        .HasColumnName("TIPOOCORRENCIA");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USUARIOID");

                    b.HasKey("Id");

                    b.HasIndex("TipoOcorrenciaId");

                    b.ToTable("OCORRENCIA", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit")
                        .HasColumnName("ATIVO");

                    b.Property<int?>("CategoriaId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("CATEGORIAID");

                    b.Property<DateTime>("DataHoraAlteracao")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORAALTERACAO");

                    b.Property<DateTime>("DataHoraCadastro")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAHORACADASTRO");

                    b.Property<int?>("DepositoId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("DEPOSITOID");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)")
                        .HasColumnName("DESCRICAO");

                    b.Property<int?>("FornecedorId")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("FORNECEDORID");

                    b.Property<string>("ImagemUrl")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("IMAGEMURL");

                    b.Property<string>("Marca")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("MARCA");

                    b.Property<decimal?>("MetroQCaixa")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("MERTROQCAIXA");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NOME");

                    b.Property<int?>("PecasCaixa")
                        .HasColumnType("int")
                        .HasColumnName("PECASCAIXA");

                    b.Property<string>("Pei")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("PEI");

                    b.Property<decimal?>("PrecoCaixa")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PRECOCAIXA");

                    b.Property<decimal?>("PrecoMetroQ")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PRECOMETRO");

                    b.Property<int?>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("QUANTIDADE");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("DepositoId");

                    b.HasIndex("FornecedorId");

                    b.ToTable("PRODUTO", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.TipoOcorrencia", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Nome")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.ToTable("TIPOOCORRENCIA ", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Venda", b =>
                {
                    b.Property<int>("VendaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VendaID"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2")
                        .HasColumnName("DATAVENDA");

                    b.Property<int>("LoteId")
                        .HasColumnType("int")
                        .HasColumnName("LOTEID");

                    b.Property<int?>("LoteId1")
                        .HasColumnType("int");

                    b.Property<string>("Nomeproduto")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NOMEPRODUTO");

                    b.Property<string>("NumeroLote")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NUMEROLOTE");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("QUANTIDADE");

                    b.Property<string>("UsuarioId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USUARIOID");

                    b.HasKey("VendaID");

                    b.HasIndex("LoteId");

                    b.HasIndex("LoteId1");

                    b.ToTable("HISTORICOVENDA", (string)null);
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.BaixaOcorrencia", b =>
                {
                    b.HasOne("CasaColomboApp.Domain.Entities.Ocorrencia", null)
                        .WithMany("BaixaOcorrencias")
                        .HasForeignKey("OcorrenciaId");

                    b.HasOne("CasaColomboApp.Domain.Entities.Ocorrencia", "Ocorrencia")
                        .WithMany()
                        .HasForeignKey("TipoOcorrenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ocorrencia");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Lote", b =>
                {
                    b.HasOne("CasaColomboApp.Domain.Entities.Produto", "Produto")
                        .WithMany("Lote")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Ocorrencia", b =>
                {
                    b.HasOne("CasaColomboApp.Domain.Entities.TipoOcorrencia", "TipoOcorrencia")
                        .WithMany("Ocorrencias")
                        .HasForeignKey("TipoOcorrenciaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TipoOcorrencia");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Produto", b =>
                {
                    b.HasOne("CasaColomboApp.Domain.Entities.Categoria", "Categoria")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CasaColomboApp.Domain.Entities.Deposito", "Deposito")
                        .WithMany("Produtos")
                        .HasForeignKey("DepositoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CasaColomboApp.Domain.Entities.Fornecedor", "Fornecedor")
                        .WithMany("Produtos")
                        .HasForeignKey("FornecedorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Deposito");

                    b.Navigation("Fornecedor");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Venda", b =>
                {
                    b.HasOne("CasaColomboApp.Domain.Entities.Lote", "Lote")
                        .WithMany()
                        .HasForeignKey("LoteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CasaColomboApp.Domain.Entities.Lote", null)
                        .WithMany("Vendas")
                        .HasForeignKey("LoteId1");

                    b.Navigation("Lote");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Categoria", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Deposito", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Fornecedor", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Lote", b =>
                {
                    b.Navigation("Vendas");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Ocorrencia", b =>
                {
                    b.Navigation("BaixaOcorrencias");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.Produto", b =>
                {
                    b.Navigation("Lote");
                });

            modelBuilder.Entity("CasaColomboApp.Domain.Entities.TipoOcorrencia", b =>
                {
                    b.Navigation("Ocorrencias");
                });
#pragma warning restore 612, 618
        }
    }
}