using CasaColomboApp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Substitua a string de conexão padrão pelo MySQL
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDCasaColomboDeposito;Integrated Security=True;");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new FornecedorMap());
            modelBuilder.ApplyConfiguration(new DepositoMap());
            modelBuilder.ApplyConfiguration(new LoteMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
            
            modelBuilder.ApplyConfiguration(new FornecedorOcorrenciaMap());
            modelBuilder.ApplyConfiguration(new TipoOcorrenciaMap());
            modelBuilder.ApplyConfiguration(new OcorrenciaMap());
            modelBuilder.ApplyConfiguration(new BaixaOcorrenciamMap());
        }
    }
}
