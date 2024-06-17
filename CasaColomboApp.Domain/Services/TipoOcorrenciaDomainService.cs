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
    public class TipoOcorrenciaDomainService : ITipoOcorrenciaDomainService
    {
        private readonly ITipoOcorrenciaRepository _tipoOcorrenciaRepository;

        public TipoOcorrenciaDomainService
            (ITipoOcorrenciaRepository? tipoOcorrenciaRepository)
        {
            _tipoOcorrenciaRepository = tipoOcorrenciaRepository;
        }

        public TipoOcorrencia Atualizar(TipoOcorrencia tipoOcorrencia)
        {
            var registro = ObterPorId(tipoOcorrencia.Id.Value);
            if (registro == null) 
                throw new ApplicationException("Tipo Ocorrencia nao encontrada");

            var tipoOcorrenciaAtaulizado = new TipoOcorrencia
            {
            
                Id = tipoOcorrencia.Id,
                Nome = tipoOcorrencia.Nome


            };


            _tipoOcorrenciaRepository?.Update(tipoOcorrenciaAtaulizado);
            return _tipoOcorrenciaRepository.GetById(tipoOcorrencia.Id.Value);



        }

        public TipoOcorrencia Cadastrar(TipoOcorrencia tipoOcorrencia)
        {
            _tipoOcorrenciaRepository?.Add(tipoOcorrencia);
            tipoOcorrencia = _tipoOcorrenciaRepository?.GetById(tipoOcorrencia.Id.Value);

            return tipoOcorrencia;
        }

        public List<TipoOcorrencia> Consultar()
        {
            return _tipoOcorrenciaRepository?.GetAll();
        }

        public TipoOcorrencia Delete(int id)
        {
            var tipoOcorrencia = ObterPorId(id);
            if (tipoOcorrencia == null)
                throw new ApplicationException("Tipo de serviçonao encontrado.");

            _tipoOcorrenciaRepository?.Delete(tipoOcorrencia);
            return tipoOcorrencia;
        }

        public TipoOcorrencia ObterPorId(int id)
        {
            return _tipoOcorrenciaRepository?.GetById(id);
        }
    }
}
