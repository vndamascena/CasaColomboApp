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


    public class FornecedorDomainService : IFornecedorDomainService
    {

        private readonly IFornecedorRepository? _fornecedorRepository;
        public FornecedorDomainService

            (IFornecedorRepository? fornecedorRepository)

        {
            _fornecedorRepository = fornecedorRepository;
        }

        public Fornecedor Atualizar(Fornecedor fornecedor)
        {
            var registro = ObterPorId(fornecedor.Id.Value);

            if (registro == null)
                throw new ApplicationException("Fornecedor não encontrada para edição.");

            var fornecedorAtualizado = new Fornecedor
            {
                Id = fornecedor.Id,

                Nome = fornecedor.Nome,

                Cnpj = fornecedor.Cnpj,

                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now
            };

            _fornecedorRepository?.Update(fornecedorAtualizado);
            return _fornecedorRepository?.GetById(fornecedor.Id.Value);
        }

        public Fornecedor Cadastrar(Fornecedor fornecedor)
        {
            _fornecedorRepository?.Add(fornecedor);
            fornecedor = _fornecedorRepository?.GetById(fornecedor.Id.Value);

            return fornecedor;
        }

        public List<Fornecedor> Consultar()
        {
            return _fornecedorRepository?.GetAll();
        }

        public Fornecedor Delete(int id)
        {
            var fornecedor = ObterPorId(id);

            if (fornecedor == null)
                throw new ApplicationException("Fornecedor não encontrado para exclusão.");


            fornecedor.DataHoraAlteracao = DateTime.Now;

            _fornecedorRepository?.Delete(fornecedor);
            return fornecedor;
        }

        public Fornecedor ObterPorId(int id)
        {
            return _fornecedorRepository?.GetById(id);
        }
    }
}
