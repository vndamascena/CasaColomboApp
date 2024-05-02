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
        Categoria Delete(int id);
        List<Categoria> Consultar();

        Categoria ObterPorId(int id);

    }
}
