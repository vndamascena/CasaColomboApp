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
    public class FornecedorGeralMap : IEntityTypeConfiguration<FornecedorGeral>
    {
       

        public void Configure(EntityTypeBuilder<FornecedorGeral> builder)
        {
            builder.ToTable("FORNECEDORGERAL");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id).HasColumnName("ID");

            builder.Property(f => f.Nome).HasColumnName("NOME").HasMaxLength(15).IsRequired();

            builder.Property(f => f.Vendedor).HasColumnName("VENDEDOR");
            builder.Property(f => f.ForneProdu).HasColumnName("FORNECEDORPRODUTO");
            builder.Property(f => f.Tipo).HasColumnName("TIPO");
            builder.Property(f => f.TelFor).HasColumnName("TELFORNECEDOR");
            builder.Property(f => f.TelVen).HasColumnName("TELVENDEDOR");

            builder.Property(p => p.DataHoraCadastro).HasColumnName("DATAHORACADASTRO").IsRequired();

            builder.Property(p => p.DataHoraAlteracao).HasColumnName("DATAHORAALTERACAO").IsRequired();

            
        }
    }
}
