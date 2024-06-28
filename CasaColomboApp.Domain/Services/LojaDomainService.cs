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
    public class LojaDomainService : ILojaDomainService
    {
        private readonly ILojaRepository? _lojarepository;
        public LojaDomainService(ILojaRepository? lojarepository)
        {
            _lojarepository = lojarepository;
        }
        public Loja Atualizar(Loja loja)
        {
            throw new NotImplementedException();
        }

        public Loja Cadastrar(Loja loja)
        {
            _lojarepository?.Add(loja);
            loja = _lojarepository?.GetById(loja.Id.Value);
            return loja;

            
        }

        public List<Loja> Consultar()
        {
            return _lojarepository?.GetAll();
        }

        public Loja Delete(int id)
        {
           var loja = ObterPorId(id);
            if (loja == null)
            
                throw new ApplicationException("Loja não encontrada");

            _lojarepository.Delete(loja);
            return loja;
            
        }

        public Loja ObterPorId(int id)
        {
            return _lojarepository?.GetById(id);
        }
    }
}
