using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Services
{
    public class CategoriaDomainService : ICategoriaDomainService
    {
        private readonly ICategoriaRepository? _categoriaRepository;

        public CategoriaDomainService

            (ICategoriaRepository? categoriaRepository)

        {
            _categoriaRepository = categoriaRepository;
        }

        public Categoria Atualizar(Categoria categoria)
        {
            var registro = ObterPorId(categoria.Id.Value);

            if (registro == null)
                throw new ApplicationException("Categoria não encontrada para edição.");

            var categoriaAtualizado = new Categoria
            {
                Id = categoria.Id,
                
                Nome = categoria.Nome,
               
                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now
            };

            _categoriaRepository?.Update(categoriaAtualizado);
            return _categoriaRepository?.GetById(categoria.Id.Value);
        }

        public Categoria Cadastrar(Categoria categoria)
        {
            _categoriaRepository?.Add(categoria);
            categoria = _categoriaRepository?.GetById(categoria.Id.Value);

            return categoria;
        }

        public List<Categoria> Consultar()
        {
            return _categoriaRepository?.GetAll();
        }

        public Categoria Delete(int id)
        {
            var categoria = ObterPorId(id);

            if (categoria == null)
                throw new ApplicationException("categoria não encontrado para exclusão.");

            
            categoria.DataHoraAlteracao = DateTime.Now;

            _categoriaRepository.Delete(categoria);

            return categoria;
        }

        public Categoria ObterPorId(int id)
        {
           return _categoriaRepository?.GetById(id);
        }
    }
}
