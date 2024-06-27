using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorOcorrencia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaColomboApp.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedorOcorrenciaController : ControllerBase
    {
        private readonly IFornecedorOcorrenciaDomainService? _fornecedorOcorrenciaDomainService;
        private readonly IMapper? _mapper;
        public FornecedorOcorrenciaController

        (IFornecedorOcorrenciaDomainService? fornecedorOcorrenciaDomainService, IMapper? mapper)

        {
            _fornecedorOcorrenciaDomainService = fornecedorOcorrenciaDomainService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<FornecedorOcorrenciaGetModel>), 200)]
        public IActionResult GetAll()
        {
            var fornecedores = _fornecedorOcorrenciaDomainService?.Consultar();
            var result = _mapper?.Map<List<FornecedorOcorrenciaGetModel>>(fornecedores);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult PutModel([FromBody] FornecedorOcorrenciaPutModel model)
        {
            try
            {
                //capturando os dados que serão atualizados do fornecedor
                var fornecedor = new FornecedorOcorrencia
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    

                };

                //atualizando o fornecedor
                var result = _fornecedorOcorrenciaDomainService?.Atualizar(fornecedor);

                //HTTP 201 (OK)
                return StatusCode(200, _mapper?.Map<FornecedorOcorrenciaGetModel>(result));
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
        [ProducesResponseType(typeof(FornecedorOcorrenciaGetModel), 201)]
        public IActionResult PostModel([FromBody] FornecedorOcorrenciaPostModel model)
        {
            try
            {
                //cadastrando o produto
                var fornecedor = _mapper?.Map<FornecedorOcorrencia>(model);
                var result = _fornecedorOcorrenciaDomainService?.Cadastrar(fornecedor);

                //HTTP 201 (CREATED)
                return StatusCode(201, _mapper.Map<FornecedorOcorrenciaGetModel>(result));
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
                var result = _fornecedorOcorrenciaDomainService?.Delete(id);
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
        [ProducesResponseType(typeof(FornecedorOcorrenciaGetModel), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                var result = _mapper?.Map<FornecedorOcorrenciaGetModel>(_fornecedorOcorrenciaDomainService?.ObterPorId(id));
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
