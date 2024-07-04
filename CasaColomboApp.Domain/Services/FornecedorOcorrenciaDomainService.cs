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
    public class FornecedorOcorrenciaDomainService : IFornecedorOcorrenciaDomainService
    {

        private readonly IFornecedorOcorrenciaRepository? _fornecedorOcorrenciaRepository;

        public FornecedorOcorrenciaDomainService(IFornecedorOcorrenciaRepository? fornecedorOcorrenciaRepository)

        {
            _fornecedorOcorrenciaRepository = fornecedorOcorrenciaRepository;
        }

        public FornecedorOcorrencia Atualizar(FornecedorOcorrencia fornecedorOcorrencia)
        {
            var registro = ObterPorId(fornecedorOcorrencia.Id.Value);

            if (registro == null)
                throw new ArgumentNullException("Fornecedor nao encontrado para edição");
          
                registro.Id = fornecedorOcorrencia.Id;
                registro.Nome = fornecedorOcorrencia.Nome;
                registro.Vendedor = fornecedorOcorrencia.Vendedor;
                registro.ForneProdu = fornecedorOcorrencia.ForneProdu;
                registro.Tipo = fornecedorOcorrencia.Tipo;
                registro.TelVen = fornecedorOcorrencia.TelVen;
                registro.TelFor = fornecedorOcorrencia.TelFor;
                registro.DataHoraAlteracao = DateTime.Now;


            _fornecedorOcorrenciaRepository?.Update(registro);
                        

            return registro;
        }

        public FornecedorOcorrencia Cadastrar(FornecedorOcorrencia fornecedorOcorrencia)
        {
            _fornecedorOcorrenciaRepository?.Add(fornecedorOcorrencia);
            fornecedorOcorrencia = _fornecedorOcorrenciaRepository?.GetById(fornecedorOcorrencia.Id.Value);
            return fornecedorOcorrencia;
        }

        public List<FornecedorOcorrencia> Consultar()
        {
            return _fornecedorOcorrenciaRepository?.GetAll();
        }

        public FornecedorOcorrencia Delete(int id)
        {
            var fornecedorOcorrencia = ObterPorId(id);
            if (fornecedorOcorrencia == null)
                  throw new ApplicationException("Fornecedor nao encontrado para exclução");



            _fornecedorOcorrenciaRepository.Delete(fornecedorOcorrencia);
            return fornecedorOcorrencia;

        }

        public FornecedorOcorrencia ObterPorId(int id)
        {
            return _fornecedorOcorrenciaRepository?.GetById(id);
        }
    }
}
