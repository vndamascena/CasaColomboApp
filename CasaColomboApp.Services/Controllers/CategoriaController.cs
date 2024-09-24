using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Domain.Services;
using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaColomboApp.Services.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaDomainService? _categoriaDomainService;
        private readonly IMapper? _mapper;
        public CategoriaController

        (ICategoriaDomainService? categoriaDomainService, IMapper? mapper)

        {
            _categoriaDomainService = categoriaDomainService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaGetModel>), 200)]
        public IActionResult GetAll()
        {
            var categorias = _categoriaDomainService?.Consultar();
            var result = _mapper?.Map<List<CategoriaGetModel>>(categorias);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult PutModel([FromBody] CategoriaPutModel model)
        {
            try
            {
                
                var categoria = new Categoria
                {
                    Id = model.Id,

                    Nome = model.Nome,
                   
                };

                
                var result = _categoriaDomainService?.Atualizar(categoria);

                //HTTP 201 (OK)
                return StatusCode(200, _mapper?.Map<CategoriaGetModel>(result));
            }
            catch (ApplicationException e)
            {
                //HTTP 400 (BAD REQUEST)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }

            
        }
        [HttpPost]
        [ProducesResponseType(typeof(CategoriaGetModel), 201)]
        public IActionResult PostModel([FromBody] CategoriaPostModel model)
        {
            try
            {
                
                var categoria = _mapper?.Map<Categoria>(model);
                var result = _categoriaDomainService?.Cadastrar(categoria);

                //HTTP 201 (CREATED)
                return StatusCode(201, _mapper.Map<CategoriaGetModel>(result));
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _categoriaDomainService?.Delete(id);
                return Ok(result);
            }
            catch (ApplicationException e)
            {
                //HTTP 400 (BAD REQUEST)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoriaGetModel), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _mapper?.Map<CategoriaGetModel>(_categoriaDomainService?.ObterPorId(id));
                return Ok(result);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
