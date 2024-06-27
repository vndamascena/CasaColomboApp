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
using System.Globalization;
using System.ComponentModel;
using Microsoft.Data.SqlClient;

namespace CasaColomboApp.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoDomainService _produtoDomainService;
        private readonly IMapper _mapper;
        private readonly IVendaRepository _vendaRepository;
        private readonly HttpClient _httpClient;
        private readonly string _imageFolderPath;
        private int _nextImageId = 1; // This variable will store the next image ID

        public ProdutoController(IProdutoDomainService produtoDomainService,
            IMapper mapper, IVendaRepository vendaRepository, IHttpClientFactory httpClientFactory)
        {
            _produtoDomainService = produtoDomainService;
            _mapper = mapper;
            _vendaRepository = vendaRepository;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://colombo01-001-site2.gtempurl.com/usuarios/autenticar");
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, int produtoId)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Nenhuma imagem foi enviada.");
            }

            // Generate a unique filename using an auto-incrementing integer
            string fileName = $"{_nextImageId}_{Path.GetFileName(imageFile.FileName)}";
            _nextImageId++; // Increment the counter for the next image

            string filePath = Path.Combine(_imageFolderPath, fileName);

            // Ensure the destination directory exists
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }

            // Save the image to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Save the relative image path in the database
            string relativeFilePath = $"/images/{fileName}";
            await SalvarCaminhoImagemNoBanco(produtoId, relativeFilePath);

            return Ok(new { Message = "Imagem carregada com sucesso.", ImageUrl = relativeFilePath });
        }

        private async Task SalvarCaminhoImagemNoBanco(int produtoId, string relativeFilePath)
        {
            string connectionString = @"Data Source=SQL8010.site4now.net;Initial Catalog=db_aa8a78_casacol;User Id=db_aa8a78_casacol_admin;Password=colombo24";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE PRODUTO SET IMAGEMURL = @FilePath WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilePath", relativeFilePath);
                    command.Parameters.AddWithValue("@ID", produtoId);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        [HttpGet]
        [Route("images/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            string filePath = Path.Combine(_imageFolderPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "image/jpeg");
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Serviço para realização do cadastro de um produto.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProdutoGetModel), 201)]
        public async Task<IActionResult> Post([FromBody] ProdutoPostModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

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

        [HttpPut]
        [ProducesResponseType(typeof(ProdutoGetModel), 201)]
        public async Task<IActionResult> Put([FromBody] ProdutoPutModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

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


        private async Task<bool> IsUserAuthorized(string matricula, string senha)
        {
            // Lista de usuários autorizados para autenticação
            Dictionary<string, string> usuariosAutorizados = new Dictionary<string, string>
            {
                { "65", "1723" },   // Exemplo: Matricula e senha do usuário 1
                { "1", "2816" }, // Exemplo: Matricula e senha do usuário 2
                 { "5", "1005" },   // Exemplo: Matricula e senha do usuário 1
                { "2", "1470" },
            };

            // Verifica se as credenciais fornecidas estão na lista de usuários autorizados
            if (usuariosAutorizados.ContainsKey(matricula) && usuariosAutorizados[matricula] == senha)
            {
                try
                {
                    // Realiza a chamada à API de autenticação apenas para os usuários autorizados
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
            else
            {
                // Se as credenciais do usuário não estiverem na lista branca, retorne false
                return false;
            }
        }


        /// <summary>
        /// Serviço para excluir um produto.
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
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
        public IActionResult GetById(int id)
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
        public IActionResult GetLotesByProdutoId(int id)
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
        public IActionResult DeleteLote(int produtoId, int loteId)
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
        public async Task<IActionResult> ConfirmarVenda(string matricula, string senha, int Id, [FromBody] VendaModel venda)
        {
            try
            {
                if (await AutenticarUsuario(matricula, senha))
                {
                    _produtoDomainService.ConfirmarVenda(Id, venda.QuantidadeVendida, matricula);
                    return Ok(new { message = "Venda confirmada com sucesso!" });
                }
                else
                {
                    return StatusCode(401, new { error = "Matricula ou senha incorreta, tente novamente" });
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
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }
    }
}
