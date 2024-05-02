using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Domain.Services;
using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Fornecedor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CasaColomboApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorDomainService? _fornecedorDomainService;
        private readonly IMapper? _mapper;
        public FornecedorController

        (IFornecedorDomainService? fornecedorDomainService, IMapper? mapper)

        {
            _fornecedorDomainService = fornecedorDomainService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<FornecedorGetModel>), 200)]
        public IActionResult GetAll()
        {
            var fornecedores = _fornecedorDomainService?.Consultar();
            var result = _mapper?.Map<List<FornecedorGetModel>>(fornecedores);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult PutModel([FromBody] FornecedorPutModel model)
        {
            try
            {
                //capturando os dados que serão atualizados do fornecedor
                var fornecedor = new Fornecedor
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Cnpj = model.Cnpj,

                };

                //atualizando o fornecedor
                var result = _fornecedorDomainService?.Atualizar(fornecedor);

                //HTTP 201 (OK)
                return StatusCode(200, _mapper?.Map<FornecedorGetModel>(result));
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
        [ProducesResponseType(typeof(FornecedorGetModel), 201)]
        public IActionResult PostModel([FromBody] FornecedorPostModel model)
        {
            try
            {
                //cadastrando o produto
                var fornecedor = _mapper?.Map<Fornecedor>(model);
                var result = _fornecedorDomainService?.Cadastrar(fornecedor);

                //HTTP 201 (CREATED)
                return StatusCode(201, _mapper.Map<FornecedorGetModel>(result));
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
                var result = _fornecedorDomainService?.Delete(id);
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
        [ProducesResponseType(typeof(FornecedorGetModel), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _mapper?.Map<FornecedorGetModel>(_fornecedorDomainService?.ObterPorId(id));
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
