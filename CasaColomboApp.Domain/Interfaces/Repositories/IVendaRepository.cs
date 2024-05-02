using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IVendaRepository : IBaseRepository<Venda, int>
    {
        // Adicione métodos adicionais específicos para o modelo de dados de vendas, se necessário
        List<Venda> GetVendasByLoteId(int loteId);
        List<Venda> GetVendasByUsuarioId(string matricula);
        // Outros métodos podem ser adicionados conforme necessário
    }
}
