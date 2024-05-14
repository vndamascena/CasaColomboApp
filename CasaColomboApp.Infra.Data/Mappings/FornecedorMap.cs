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
    internal class FornecedorMap : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.ToTable("FORNECEDOR");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).HasColumnName("ID");

            builder.Property(f => f.Nome).HasColumnName("NOME").HasMaxLength(15).IsRequired();

            builder.Property(f => f.Cnpj).HasColumnName("CNPJ").HasMaxLength(20).IsRequired();

            builder.Property(p => p.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();

            builder.Property(p => p.DataHoraAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();


            builder.HasIndex(f => f.Cnpj).IsUnique();
        }
    }
}
