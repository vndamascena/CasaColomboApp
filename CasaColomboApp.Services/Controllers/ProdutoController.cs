using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Services.Model.Produto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaColomboApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoDomainService? _produtoDomainService;
        private readonly IMapper? _mapper;

        public ProdutoController(IProdutoDomainService? produtoDomainService, IMapper? mapper)
        {
            _produtoDomainService = produtoDomainService;
            _mapper = mapper;
        }

        /// <summary>
        /// Serviço para realização do cadastro de um produto.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProdutoGetModel), 201)]
        public IActionResult Post([FromBody] ProdutoPostModel model)
        {
            try
            {
                //cadastrando o produto
                var produto = _mapper?.Map<Produto>(model);
                var result = _produtoDomainService?.Cadastrar(produto);

                //HTTP 201 (CREATED)
                return StatusCode(201, _mapper.Map<ProdutoGetModel>(result));
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço para alterar os dados de um produto.
        /// </summary>
        [HttpPut]
        public IActionResult Put([FromBody] ProdutoPutModel model)
        {
            try
            {
                //capturando os dados que serão atualizados do produto
                var produto = new Produto
                {
                    Id = model.Id,
                    Codigo = model.Codigo,
                    Nome = model.Nome,
                    Marca = model.Marca,
                    Lote = model.Lote,
                    Quantidade = model.Quantidade,
                    Descricao = model.Descricao,
                    CategoriaId = model.CategoriaId,
                    FornecedorId = model.FornecedorId,
                    DepositoId = model.DepositoId,
                    
                    
                };

                //atualizando o produto
                var result = _produtoDomainService?.Atualizar(produto);

                //HTTP 201 (OK)
                return StatusCode(200, _mapper?.Map<ProdutoGetModel>(result));
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

        /// <summary>
        /// Serviço para excluir um produto.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var result = _produtoDomainService?.Inativar(id);
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

        /// <summary>
        /// Serviço para consultar todos os produtos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutoGetModel>), 200)]
        public IActionResult GetAll()
        {
            try
            {
                var result = _mapper?.Map<List<ProdutoGetModel>>(_produtoDomainService?.Consultar());
                return Ok(result);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        

        /// <summary>
        /// Serviço para consultar 1 produto através do ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProdutoGetModel), 200)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var result = _mapper?.Map<ProdutoGetModel>(_produtoDomainService?.ObterPorId(id));
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
