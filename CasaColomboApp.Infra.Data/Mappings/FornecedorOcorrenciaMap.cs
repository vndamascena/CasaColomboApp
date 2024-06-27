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
    public class FornecedorOcorrenciaMap : IEntityTypeConfiguration<FornecedorOcorrencia>
    {
       

        public void Configure(EntityTypeBuilder<FornecedorOcorrencia> builder)
        {
            builder.ToTable("FORNECEDOROCORRENCIA");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).HasColumnName("ID");

            builder.Property(f => f.Nome).HasColumnName("NOME").HasMaxLength(15).IsRequired();

           

            builder.Property(p => p.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();

            builder.Property(p => p.DataHoraAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();

            
        }
    }
}
