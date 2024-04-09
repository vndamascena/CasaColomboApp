using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Services
{
    public class DepositoDomainService : IDepositoDomainService
    {
        private readonly IDepositoRepository? _depositoRepository;
        public DepositoDomainService

            (IDepositoRepository? depositoRepository)

        {
            _depositoRepository = depositoRepository;
        }
        public List<Deposito> Consultar()
        {
            return _depositoRepository?.GetAll();
        }

      
    }
}
