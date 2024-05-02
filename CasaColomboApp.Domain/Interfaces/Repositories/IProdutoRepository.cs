﻿using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IBaseRepository<Produto,Guid>
    {
        List<Produto> GetAll(bool ativo);
        List<Lote> GetLotesByProdutoId(Guid produtoId);

    }
}