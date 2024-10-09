using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Services
{
    public class DepositosDomainService:IDepositosDomainService
    {
        private readonly IDepositosRepository _depositosRepository;

        public DepositosDomainService(IDepositosRepository depositosRepository)
        {
            _depositosRepository = depositosRepository;
        }

        public Depositos Cadastrar(Depositos depositos)
        {
            _depositosRepository?.Add(depositos);
            depositos = _depositosRepository?.GetById(depositos.Id);
            return depositos;
        }

        public List<Depositos> Consultar()
        {
           return _depositosRepository.GetAll();
        }

        public Depositos ObterPorId(int id)
        {
            return _depositosRepository?.GetById(id);
        }
    }
}
