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
    public class FornecedorGeralDomainService : IFornecedorGeralDomainService
    {

        private readonly IFornecedorGeralRepository? _fornecedorGeralRepository;

        public FornecedorGeralDomainService(IFornecedorGeralRepository? fornecedorGeralRepository)

        {
            _fornecedorGeralRepository = fornecedorGeralRepository;
        }

        public FornecedorGeral Atualizar(FornecedorGeral fornecedorGeral)
        {
            var registro = ObterPorId(fornecedorGeral.Id.Value);

            if (registro == null)
                throw new ArgumentNullException("Fornecedor nao encontrado para edição");
          
                registro.Id = fornecedorGeral.Id;
                registro.Nome = fornecedorGeral.Nome;
                registro.Vendedor = fornecedorGeral.Vendedor;
                registro.ForneProdu = fornecedorGeral.ForneProdu;
                registro.Tipo = fornecedorGeral.Tipo;
                registro.TelVen = fornecedorGeral.TelVen;
                registro.TelFor = fornecedorGeral.TelFor;
                registro.DataHoraAlteracao = DateTime.Now;


            _fornecedorGeralRepository?.Update(registro);
                        

            return registro;
        }

        public FornecedorGeral Cadastrar(FornecedorGeral fornecedorGeral)
        {
            _fornecedorGeralRepository?.Add(fornecedorGeral);
            fornecedorGeral = _fornecedorGeralRepository?.GetById(fornecedorGeral.Id.Value);
            return fornecedorGeral;
        }

        public List<FornecedorGeral> Consultar()
        {
            return _fornecedorGeralRepository?.GetAll();
        }

        public FornecedorGeral Delete(int id)
        {
            var fornecedorGeral = ObterPorId(id);
            if (fornecedorGeral == null)
                  throw new ApplicationException("Fornecedor nao encontrado para exclução");



            _fornecedorGeralRepository.Delete(fornecedorGeral);
            return fornecedorGeral;

        }

        public FornecedorGeral ObterPorId(int id)
        {
            return _fornecedorGeralRepository?.GetById(id);
        }
    }
}
