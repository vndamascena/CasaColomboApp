using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Services.Model.Produto.Piso;
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
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoPisoController : ControllerBase
    {
        private readonly IProdutoPisoDomainService _produtoPisoDomainService;
        private readonly IMapper _mapper;
        private readonly IVendaRepository _vendaRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly HttpClient _httpClient;
        private readonly string _imageFolderPath;
        private int _nextImageId = 1; 

        public ProdutoPisoController(IProdutoPisoDomainService produtoPisoDomainService,
            IMapper mapper, IVendaRepository vendaRepository, ILoteRepository loteRepository, IHttpClientFactory httpClientFactory)
        {
            _produtoPisoDomainService = produtoPisoDomainService;
            _mapper = mapper;
            _vendaRepository = vendaRepository;
            _loteRepository = loteRepository;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://colombo01-001-site2.gtempurl.com/usuarios/autenticar");
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, int produtoPisoId)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Nenhuma imagem foi enviada.");
            }

            
            string fileName = $"{_nextImageId}_{Path.GetFileName(imageFile.FileName)}";
            _nextImageId++; 

            string filePath = Path.Combine(_imageFolderPath, fileName);

            
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }

            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            
            string relativeFilePath = $"/images/{fileName}";
            await SalvarCaminhoImagemNoBanco(produtoPisoId, relativeFilePath);

            return Ok(new { Message = "Imagem carregada com sucesso.", ImageUrl = relativeFilePath });
        }

        private async Task SalvarCaminhoImagemNoBanco(int produtoPisoId, string relativeFilePath)
        {
            string connectionString = @"Data Source=SQL8010.site4now.net;Initial Catalog=db_aa8a78_casacol;User Id=db_aa8a78_casacol_admin;Password=colombo24";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE PRODUTOPISO SET IMAGEMURL = @FilePath WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilePath", relativeFilePath);
                    command.Parameters.AddWithValue("@ID", produtoPisoId);

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
        [ProducesResponseType(typeof(ProdutoPisoGetModel), 201)]
        public async Task<IActionResult> Post([FromBody] ProdutoPisoPostModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade Produto
                var produtoPiso = _mapper.Map<ProdutoPiso>(model);

                // Mapear os modelos de lote para entidades de lote
                var lotes = _mapper.Map<List<Lote>>(model.Lote);

                // Cadastrar o produto juntamente com os lotes
                var result = _produtoPisoDomainService.Cadastrar(produtoPiso, lotes, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoPisoGetModel = _mapper.Map<ProdutoPisoGetModel>(result);

                // Retornar resposta HTTP 201 (CREATED) com o modelo de resposta
                return StatusCode(201, new
                {
                    Message = "Produto cadastrado com sucesso",
                    produtoPisoGetModel
                });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProdutoPisoGetModel), 201)]
        public async Task<IActionResult> Put([FromBody] ProdutoPisoPutModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade Produto
                var produtoPiso = _mapper.Map<ProdutoPiso>(model);

                // Atualizar o produto
                var result = _produtoPisoDomainService.Atualizar(produtoPiso, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoPisoGetModel = _mapper.Map<ProdutoPisoGetModel>(result);

                // Retornar resposta HTTP 200 (OK) com o modelo de resposta
                return StatusCode(201, new
                {
                    Message = "Produto atualizado com sucesso",
                    produtoPisoGetModel
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
                var result = _produtoPisoDomainService?.Inativar(id);
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
        [ProducesResponseType(typeof(List<ProdutoPisoGetModel>), 200)]
        public IActionResult GetAll()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
            try
            {
                // Consulta todos os produtos
                var produtosPiso = _produtoPisoDomainService.Consultar();

                // Mapeia os produtos para os modelos de resposta incluindo os lotes
                var produtosPisoModel = _mapper.Map<List<ProdutoPisoGetModel>>(produtosPiso, opt => opt.Items["IncludeLotes"] = true);

                return Ok(produtosPisoModel);
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
        [ProducesResponseType(typeof(ProdutoPisoGetModel), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                // Consulta o produto pelo ID
                var produtoPiso = _produtoPisoDomainService.ObterPorId(id);

                // Mapeia o produto para o modelo de resposta incluindo os lotes
                var produtoPisoModel = _mapper.Map<ProdutoPisoGetModel>(produtoPiso, opt => opt.Items["IncludeLotes"] = true);

                return Ok(produtoPisoModel);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("lotes")]
        [ProducesResponseType(typeof(LoteGetModel), 200)]
        public IActionResult GetLoteAll()
        {
            try
            {
                var lote = _loteRepository.GetAll();
                var loteModel = _mapper.Map<List<LoteGetModel>>(lote);
                return Ok(loteModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }

        [HttpGet("{id}/lotes")]
        [ProducesResponseType(typeof(List<LoteGetModel>), 200)]
        public IActionResult GetLotesByProdutoId(int id)
        {
            try
            {
                // Consulta os lotes associados ao produto pelo ID do produto
                var lotes = _produtoPisoDomainService.ConsultarLote(id);

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

        [HttpDelete("{produtoPisoId}/lotes/{loteId}")]
        public IActionResult DeleteLote(int produtoPisoId, int loteId)
        {
            try
            {
                // Chamar o serviço de domínio para excluir o lote do produto
                _produtoPisoDomainService.ExcluirLote(produtoPisoId, loteId); // Alterado para usar loteId

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
        public async Task<IActionResult> ConfirmarVenda(string matricula, string senha, int Id, [FromBody] VendaPisoModel venda)
        {
            try
            {
                if (await AutenticarUsuario(matricula, senha))
                {
                    _produtoPisoDomainService.ConfirmarVenda(Id, venda.QuantidadeVendida, matricula);
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
        [ProducesResponseType(typeof(VendaPisoGetModel), 200)]
        public IActionResult GetVendaAll()
        {
            try
            {
                var venda = _vendaRepository.GetAll();
                var vendasModel = _mapper.Map<List<VendaPisoGetModel>>(venda);
                return Ok(vendasModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }
    }
}
