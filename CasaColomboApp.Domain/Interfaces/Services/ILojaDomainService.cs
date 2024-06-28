using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface ILojaDomainService
    {
        Loja Cadastrar(Loja loja);
        Loja Atualizar(Loja loja);
        Loja Delete(int id);
        List<Loja> Consultar();
        Loja ObterPorId(int id);
    }
}
