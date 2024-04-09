using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IProdutoDomainService
    {

        Produto Cadastrar(Produto produto);
        Produto Atualizar(Produto produto);
        Produto Inativar(Guid id);

        List<Produto> Consultar();


        Produto ObterPorId(Guid id);
    }
}
