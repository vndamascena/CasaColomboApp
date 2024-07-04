using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorOcorrencia;
using CasaColomboApp.Services.Model.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace CasaColomboApp.Services.Controllers
{
    
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


                if (model == null)
                {
                    return BadRequest("Model cannot be null.");
                }
                //capturando os dados que serão atualizados do fornecedor
                var fornecedor = new FornecedorOcorrencia
                {

                    Id = model.Id,
                    Nome = model.Nome,
                    Vendedor = model.Vendedor,
                    ForneProdu = model.ForneProdu,
                    Tipo = model.Tipo,
                    TelVen = model.TelVen,
                    TelFor = model.TelFor,
                    DataHoraAlteracao = DateTime.Now



                };

                //atualizando o fornecedor
                var result = _fornecedorOcorrenciaDomainService?.Atualizar(fornecedor);

                //HTTP 201 (OK)
                return StatusCode(201, new
                {
                    Message = "Fornecedor atualizado com sucesso",
                    result
                });
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
                return StatusCode(201, new
                {
                    Message = "Fornecedor cadastrado com sucesso",
                    result
                });
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
