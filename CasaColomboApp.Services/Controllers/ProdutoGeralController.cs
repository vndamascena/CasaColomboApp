using AutoMapper;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Text;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Services;
using CasaColomboApp.Services.Model.Produto.Piso;
using CasaColomboApp.Services.Model.ProdutoGeral;
using CasaColomboApp.Infra.Data.Repositories;
using System.Globalization;
using System.Net.Http;

namespace CasaColomboApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoGeralController : ControllerBase
    {
        
        private int _nextImageId = 1;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly IQuantidadeProdutosDepositosRepository _quantidadeProdutosDepositosRepository;
        private readonly string _imageFolderPath;
        private readonly IProdutoGeralDomainService _produtoGeralDomainService;
        private readonly IVendaProdutoGeralRepository _vendaProdutoGeralRepository;

        public ProdutoGeralController( IMapper mapper, IQuantidadeProdutosDepositosRepository quantidadeProdutosDepositosRepository, 
            IVendaProdutoGeralRepository vendaProdutoGeralRepository, IProdutoGeralDomainService produtoGeralDomainService, IHttpClientFactory httpClientFactory)


        {
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            _mapper = mapper;
            _vendaProdutoGeralRepository = vendaProdutoGeralRepository;
            _quantidadeProdutosDepositosRepository = quantidadeProdutosDepositosRepository;
            _produtoGeralDomainService = produtoGeralDomainService;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://colombo01-001-site2.gtempurl.com/usuarios/autenticar");
           
        }


        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile, int produtoGeralId)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return BadRequest("Nenhuma imagem foi enviada.");
            }

            // Cria o diretório, se ainda não existir
            if (!Directory.Exists(_imageFolderPath))
            {
                Directory.CreateDirectory(_imageFolderPath);
            }

            // Busca todos os arquivos no diretório para calcular o próximo número
            var existingFiles = Directory.GetFiles(_imageFolderPath);
            int nextImageNumber = existingFiles.Length + 1;  // O próximo número será o total de arquivos + 1

            // Gera o nome do arquivo com o número crescente
            string fileName = $"{nextImageNumber}_{Path.GetFileName(imageFile.FileName)}";
            string filePath = Path.Combine(_imageFolderPath, fileName);

            // Salva a imagem no diretório
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Gera o caminho relativo para salvar no banco de dados
            string relativeFilePath = $"/images/{fileName}";

            // Salva o caminho da imagem no banco de dados
            await SalvarCaminhoImagemNoBanco(produtoGeralId, relativeFilePath);

            return Ok(new { Message = "Imagem carregada com sucesso.", ImageUrlGeral = relativeFilePath });
        }

        private async Task SalvarCaminhoImagemNoBanco(int produtoGeralId, string relativeFilePath)
        {
            string connectionString = @"Data Source=SQL8010.site4now.net;Initial Catalog=db_aa8a78_casacol;User Id=db_aa8a78_casacol_admin;Password=colombo24";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE PRODUTOGERAL SET IMAGEMURL = @FilePath WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FilePath", relativeFilePath);
                    command.Parameters.AddWithValue("@ID", produtoGeralId);

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

        /// <summary>
        /// Serviço para realização do cadastro de um produto.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProdutoGeralGetModel), 201)]
        public async Task<IActionResult> Post([FromBody] ProdutoGeralPostModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade Produto
                var produtoGeral = _mapper.Map<ProdutoGeral>(model);

                // Mapear os modelos de lote para entidades de lote
                var quantidadeProdutosDepositos = _mapper.Map<List<QuantidadeProdutosDepositos>>(model.QuantidadeProdutoDeposito);

                // Cadastrar o produto juntamente com os lotes
                var result = _produtoGeralDomainService.Cadastrar(produtoGeral, quantidadeProdutosDepositos, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoGeralGetModel = _mapper.Map<ProdutoGeralGetModel>(result);

                // Retornar resposta HTTP 201 (CREATED) com o modelo de resposta
                return StatusCode(201, new
                {
                    Message = "Produto cadastrado com sucesso",
                    produtoGeralGetModel
                });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(ProdutoGeralGetModel), 201)]
        public async Task<IActionResult> Put([FromBody] ProdutoGeralPutModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade Produto
                var produtoGeral = _mapper.Map<ProdutoGeral>(model);

                // Atualizar o produto
                var result = _produtoGeralDomainService.Atualizar(produtoGeral, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoGeralGetModel = _mapper.Map<ProdutoGeralGetModel>(result);

                // Retornar resposta HTTP 200 (OK) com o modelo de resposta
                return StatusCode(201, new
                {
                    Message = "Produto atualizado com sucesso",
                    produtoGeralGetModel
                });
            }
            catch (Exception e)
            {
                // Retornar resposta HTTP 500 (INTERNAL SERVER ERROR) em caso de exceção
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _produtoGeralDomainService?.Excluir(id);  // Chamar o método de exclusão
                return Ok(new { Message = "Produto excluído com sucesso." });
            }
            catch (ApplicationException e)
            {
                // HTTP 400 (BAD REQUEST)
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                // HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ProdutoGeralGetModel>), 200)]
        public IActionResult GetAll()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("pt-BR");
            try
            {
                // Consulta todos os produtos
                var produtosGeral = _produtoGeralDomainService.Consultar();

                // Mapeia os produtos para os modelos de resposta incluindo os lotes
                var produtosGeralModel = _mapper.Map<List<ProdutoGeralGetModel>>(produtosGeral, opt => opt.Items["IncludeProdutosGeral"] = true);

                return Ok(produtosGeralModel);
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
        [ProducesResponseType(typeof(ProdutoGeralGetModel), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                // Consulta o produto pelo ID
                var produtoGeral = _produtoGeralDomainService.ObterPorId(id);

                // Mapeia o produto para o modelo de resposta incluindo os lotes
                var produtoGeralModel = _mapper.Map<ProdutoGeralGetModel>(produtoGeral, opt => opt.Items["IncludeProdutosGeral"] = true);

                return Ok(produtoGeralModel);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpGet("depositoQuantidade")]
        [ProducesResponseType(typeof(QuantidadeProdutosDepositosGetModel), 200)]
        public IActionResult GetLoteAll()
        {
            try
            {
                var quantidadeProdutodeposito = _quantidadeProdutosDepositosRepository.GetAll();
                var quantidadeProdutoDepositoModel = _mapper.Map<List<QuantidadeProdutosDepositosGetModel>>(quantidadeProdutodeposito);
                return Ok(quantidadeProdutoDepositoModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }

        [HttpGet("{id}/quantidadeDeposito")]
        [ProducesResponseType(typeof(List<QuantidadeProdutosDepositosGetModel>), 200)]
        public IActionResult GetLotesByProdutoId(int id)
        {
            try
            {
                // Consulta os lotes associados ao produto pelo ID do produto
                var quantidadeProdutosDepositos = _produtoGeralDomainService.ConsultarQuantidadeProdutoDeposito(id);

                // Mapeia os lotes para os modelos de resposta
                var quantidadeProdutosDepositosGetModels = _mapper.Map<List<QuantidadeProdutosDepositosGetModel>>(quantidadeProdutosDepositos);

                return Ok(quantidadeProdutosDepositosGetModels);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

        [HttpDelete("{produtoGeralId}/quantidadeProdutoDeposito/{quantidadeProdutoDepositoId}")]
        public IActionResult DeleteQuantidadeProdutoDeposito(int produtoGeralId, int quantidadeProdutoDepositoId)
        {
            try
            {
                // Chamar o serviço de domínio para excluir o lote do produto
                _produtoGeralDomainService.ExcluirQuantidadeProdutoDeposito(produtoGeralId, quantidadeProdutoDepositoId); // Alterado para usar loteId

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
        public async Task<IActionResult> ConfirmarVenda(string matricula, string senha, int Id, [FromBody] VendaProdutoGeralPostModel venda)
        {
            try
            {
                if (await AutenticarUsuario(matricula, senha))
                {
                    _produtoGeralDomainService.ConfirmarVenda(Id, venda.QuantidadeVendida, matricula);
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

        [HttpGet("venda")]
        [ProducesResponseType(typeof(VendaProdutoGeralGetModel), 200)]
        public IActionResult GetVendaAll()
        {
            try
            {
                var venda = _vendaProdutoGeralRepository.GetAll();
                var vendasModel = _mapper.Map<List<VendaProdutoGeralGetModel>>(venda);
                return Ok(vendasModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }
    }
}
