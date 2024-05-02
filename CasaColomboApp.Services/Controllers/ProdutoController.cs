using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Services.Model.Produto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;

namespace CasaColomboApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoDomainService? _produtoDomainService;
        private readonly IMapper? _mapper;
        private readonly IVendaRepository? _vendaRepository;
        private readonly HttpClient _httpClient;

        public ProdutoController(IProdutoDomainService? produtoDomainService,
            IMapper? mapper, IVendaRepository? vendaRepository, IHttpClientFactory httpClientFactory)
        {
            _produtoDomainService = produtoDomainService;
            _mapper = mapper;
            _vendaRepository = vendaRepository;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:5233/usuarios/autenticar");
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
                // Mapear o modelo para a entidade Produto
                var produto = _mapper.Map<Produto>(model);

                // Mapear os modelos de lote para entidades de lote
                var lotes = _mapper.Map<List<Lote>>(model.Lote);

                // Cadastrar o produto juntamente com os lotes
                var result = _produtoDomainService.Cadastrar(produto, lotes);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoGetModel = _mapper.Map<ProdutoGetModel>(result);

                // Retornar resposta HTTP 201 (CREATED) com o modelo de resposta
               

                return StatusCode(201, new
                {
                    Message = "Produto cadastrado com sucesso",
                    produtoGetModel
                });
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
                // Mapear o modelo para a entidade Produto
                var produto = _mapper.Map<Produto>(model);




                // Atualizar o produto
                var result = _produtoDomainService.Atualizar(produto);



                // Mapear o resultado de volta para o modelo de resposta
                var produtoGetModel = _mapper.Map<ProdutoGetModel>(result);

                // Retornar resposta HTTP 200 (OK) com o modelo de resposta

                return StatusCode(201, new
                {
                    Message = "Produto atualizado com sucesso",
                    produtoGetModel
                });
            }
            catch (Exception e)
            {
                // Retornar resposta HTTP 500 (INTERNAL SERVER ERROR) em caso de exceção
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
                // Consulta todos os produtos
                var produtos = _produtoDomainService.Consultar();

                // Mapeia os produtos para os modelos de resposta incluindo os lotes
                var produtosModel = _mapper.Map<List<ProdutoGetModel>>(produtos, opt => opt.Items["IncludeLotes"] = true);

                return Ok(produtosModel);
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


                // Consulta o produto pelo ID
                var produto = _produtoDomainService.ObterPorId(id);




                // Mapeia o produto para o modelo de resposta incluindo os lotes
                var produtoModel = _mapper.Map<ProdutoGetModel>(produto, opt => opt.Items["IncludeLotes"] = true);

                return Ok(produtoModel);

            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }




        }

        [HttpGet("{id}/lotes")]
        [ProducesResponseType(typeof(List<LoteGetModel>), 200)]
        public IActionResult GetLotesByProdutoId(Guid id)
        {
            try
            {
                // Consulta os lotes associados ao produto pelo ID do produto
                var lotes = _produtoDomainService.ConsultarLote(id);



                // Mapeia os lotes para os modelos de resposta
                var lotesModel = _mapper.Map<List<LoteGetModel>>(lotes);

                return Ok(lotesModel);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }







        [HttpDelete("{produtoId}/lotes/{loteId}")]
        public IActionResult DeleteLote(Guid produtoId, Guid loteId)
        {
            try
            {
                // Chamar o serviço de domínio para excluir o lote do produto
                _produtoDomainService.ExcluirLote(produtoId, loteId); // Alterado para usar loteId

                // Retornar resposta HTTP 200 (OK) em caso de sucesso
                return Ok();
            }
            catch (Exception e)
            {
                // Retornar resposta HTTP 500 (INTERNAL SERVER ERROR) em caso de exceção
                return StatusCode(500, new { e.Message });
            }
        }



        [HttpPost("venda")]
        public async Task<IActionResult> ConfirmarVenda(string matricula, string senha, Guid Id, [FromBody] VendaModel venda)
        {
            try
            {
                if (await AutenticarUsuario(matricula, senha))
                {
                    _produtoDomainService.ConfirmarVenda(Id, venda.QuantidadeVendida);
                    return Ok(new { message = "Venda confirmada com sucesso!" });
                }
                else
                {
                    return StatusCode(401, new { error = "Falha na autenticação do usuário." });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }

        private async Task<bool> AutenticarUsuario(string matricula, string senha)
        {
            try
            {
                var usuarioModel = new { Matricula = matricula, Senha = senha };
                var json = JsonSerializer.Serialize(usuarioModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/usuarios/autenticar", content); // Substitua "rota-da-autenticacao" pela rota de autenticação da sua API
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpGet("venda")]
        [ProducesResponseType(typeof(VendaGetModel), 200)]
        
        public IActionResult GetVendaAll()
        {
            try
            {
                var venda = _vendaRepository.GetAll();

                var vendasModel = _mapper.Map<List<VendaGetModel>>(venda);
                return Ok(vendasModel);
            }
            catch(Exception e)
            {
                return StatusCode(500, new {erro = e.Message});
            }




        }



    }
}
