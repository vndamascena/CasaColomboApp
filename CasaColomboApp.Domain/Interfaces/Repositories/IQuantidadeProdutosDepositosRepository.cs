﻿using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IQuantidadeProdutosDepositosRepository : IBaseRepository<QuantidadeProdutosDepositos, int>
    {
        List<QuantidadeProdutosDepositos> GetAll();

        QuantidadeProdutosDepositos ObterPorId(int id);

        
    }
}
