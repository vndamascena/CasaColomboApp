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
    public class VendaMap: IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("HISTORICOVENDA");

            builder.HasKey(v => v.VendaID);

            builder.Property(v => v.VendaID).HasColumnName("ID");

            builder.Property(v => v.LoteId).HasColumnName("LOTEID").IsRequired();

            builder.Property(v => v.Nomeproduto).HasColumnName("NOMEPRODUTO");
            builder.Property(v => v.Marca).HasColumnName("MARCA");

            builder.Property(v => v.UsuarioId).HasColumnName("USUARIOID").IsRequired();

            builder.Property(v => v.Quantidade).HasColumnName("QUANTIDADE").IsRequired();

            builder.Property(v => v.DataVenda).HasColumnName("DATAVENDA").IsRequired();

            builder.Property(v => v.NumeroLote).HasColumnName("NUMEROLOTE").IsRequired();
            


            builder.HasOne(v => v.Lote) // VENDA TEM 1 LOTE
                  .WithMany() // LOTE TEM N VENDAS
                  .HasForeignKey(v => v.LoteId) // Chave estrangeira
                  .OnDelete(DeleteBehavior.Cascade); // Excluir em cascata
        }

    }
}
