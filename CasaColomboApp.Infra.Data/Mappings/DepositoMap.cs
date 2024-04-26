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
    internal class DepositoMap: IEntityTypeConfiguration<Deposito>
    {
        public void Configure(EntityTypeBuilder<Deposito> builder)
        {
            builder.ToTable("DEPOSITO");

            builder.HasKey(c => c.DepositoId);

            builder.Property(c => c.DepositoId).HasColumnName("ID");

            builder.Property(c => c.NomeDeposito).HasColumnName("NOMEDEPOSITO").HasMaxLength(30).IsRequired();

            builder.HasIndex(c => c.NomeDeposito).IsUnique();

        }
    }
}
