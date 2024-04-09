using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface ICategoriaDomainService
    {
        Categoria Cadastrar(Categoria categoria);
        Categoria Atualizar(Categoria categoria);
        Categoria Delete(Guid id);
        List<Categoria> Consultar();

        Categoria ObterPorId(Guid id);

    }
}
