using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IDepositosDomainService
    {
        List<Depositos> Consultar();
        Depositos Cadastrar(Depositos depositos);
        Depositos ObterPorId(int id);
    }
}
