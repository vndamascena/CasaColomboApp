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
    internal class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("CATEGORIA");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("ID");

            builder.Property(c => c.Nome).HasColumnName("NOME").HasMaxLength(25).IsRequired();

            builder.Property(p => p.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();

            builder.Property(p => p.DataHoraAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();

            builder.HasIndex(c => c.Nome).IsUnique();
        }
    }
}
