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
        private readonly string _imageFolderPath;
        private readonly IProdutoGeralDomainService _produtoGeralDomainService;
        private readonly IVendaProdutoGeralRepository _vendaProdutoGeralRepository;
        private readonly IProdutoDepositoRepository _produtoDepositoRepository;

        public ProdutoGeralController( IMapper mapper, 
            IVendaProdutoGeralRepository vendaProdutoGeralRepository, 
            IProdutoGeralDomainService produtoGeralDomainService, IHttpClientFactory httpClientFactory,
            IProdutoDepositoRepository produtoDepositoRepository)


        {
            _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagesProdutoGeral");
            _mapper = mapper;
            _vendaProdutoGeralRepository = vendaProdutoGeralRepository;
            _produtoDepositoRepository = produtoDepositoRepository;
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
                // Verificar se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade ProdutoGeral
                var produtoGeral = _mapper.Map<ProdutoGeral>(model);

                // Criar a lista de depósitos com as quantidades
                var depositosSelecionados = model.Depositos
                    .Select(d => (d.DepositoId, d.Quantidade))
                    .ToList();

                // Chamar o serviço de domínio para cadastrar o produto e suas quantidades em depósitos
                var resultado = _produtoGeralDomainService.Cadastrar(produtoGeral, depositosSelecionados, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoGeralGetModel = _mapper.Map<ProdutoGeralGetModel>(resultado);

                // Retornar resposta HTTP 201 (CREATED) com o modelo de resposta
                return StatusCode(201, new
                {
                    Message = "Produto cadastrado com sucesso",
                    produtoGeralGetModel
                });
            }
            catch (Exception e)
            {
                // Retornar erro HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }



        [HttpPut]
        [ProducesResponseType(typeof(ProdutoGeralGetModel), 200)]
        public async Task<IActionResult> Put([FromBody] ProdutoGeralPutModel model, string matricula, string senha)
        {
            try
            {
                // Verificar se o usuário está autorizado
                if (!(await IsUserAuthorized(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade ProdutoGeral
                var produtoGeral = _mapper.Map<ProdutoGeral>(model);

                // Chamar o serviço de domínio para atualizar o produto
                var resultado = _produtoGeralDomainService.Atualizar(produtoGeral, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var produtoGeralGetModel = _mapper.Map<ProdutoGeralGetModel>(resultado);

                // Retornar resposta HTTP 200 (OK) com o modelo de resposta
                return StatusCode(200, new
                {
                    Message = "Produto atualizado com sucesso",
                    produtoGeralGetModel
                });
            }
            catch (Exception e)
            {
                // Retornar erro HTTP 500 (INTERNAL SERVER ERROR)
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
            try
            {
                // Consulta todos os produtos
                var produtosGeral = _produtoGeralDomainService.Consultar();

                // Mapeia os produtos para os modelos de resposta
                var produtosGeralModel = _mapper.Map<List<ProdutoGeralGetModel>>(produtosGeral);

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

                // Mapeia o produto para o modelo de resposta incluindo as quantidades em depósitos
                var produtoGeralModel = _mapper.Map<ProdutoGeralGetModel>(produtoGeral);

                return Ok(produtoGeralModel);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }


        [HttpGet("produtoDepositos")]
        [ProducesResponseType(typeof(ProdutoDepositoGetModel), 200)]
        public IActionResult GetLoteAll()
        {
            try
            {
                var deposito = _produtoDepositoRepository.GetAll();
                var depositoModel = _mapper.Map<List<ProdutoDepositoGetModel>>(deposito);
                return Ok(depositoModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }

        [HttpGet("{id}/produtoDeposito")]
        [ProducesResponseType(typeof(List<ProdutoDepositoGetModel>), 200)]
        public IActionResult GetLotesByProdutoId(int id)
        {
            try
            {
                // Consulta os lotes associados ao produto pelo ID do produto
                var lotes = _produtoGeralDomainService.ConsultarQuantidadeProdutoDeposito(id);

                // Mapeia os lotes para os modelos de resposta
                var lotesModel = _mapper.Map<List<ProdutoDepositoGetModel>>(lotes);

                return Ok(lotesModel);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
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

        [HttpPost("upload-venda")]
        public async Task<IActionResult> UploadVenda(string matricula, string senha, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { error = "Arquivo inválido." });
            }

            try
            {
                // Log para verificar a solicitação recebida
                Console.WriteLine($"Matricula: {matricula}, Senha: {senha}, Arquivo: {file.FileName}");

                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    // Agora a lista inclui o campo Deposito, mas ele será usado apenas para validação
                    var linhas = new List<(string Deposito, string DataVenda, string CodigoSistema, int QuantidadeVendida)>();
                    var erros = new List<string>(); // Lista para armazenar os erros
                    string linha;
                    int linhaNumero = 0; // Para rastrear o número da linha

                    // Ler o arquivo linha por linha
                    while ((linha = reader.ReadLine()) != null)
                    {
                        linhaNumero++;
                        var campos = linha.Split(',');

                        // Validação do formato da linha (espera-se quatro campos: CodigoSistema, QuantidadeVendida, DataVenda, Deposito)
                        if (campos.Length != 4)
                        {
                            erros.Add($"Erro na linha {linhaNumero}: Formato inválido. Esperado: Deposito, DataVenda, CodigoSistema, QuantidadeVendida.");
                            continue; // Continua para a próxima linha
                        }

                        var deposito = campos[0].Trim().ToUpper();
                        var dataVenda = campos[1].Trim();
                        var codigoSistema = campos[2].Trim();

                        // Validar se a quantidade vendida é um número inteiro
                        if (!int.TryParse(campos[3], out int quantidadeVendida))
                        {
                            erros.Add($"Erro na linha {linhaNumero}: Quantidade inválida. Esperado um número inteiro.");
                            continue; // Continua para a próxima linha
                        }

                        // Adicionar a venda à lista com o depósito (que será usado para validação)
                        linhas.Add((deposito, dataVenda, codigoSistema, quantidadeVendida));
                    }

                    foreach (var venda in linhas)
                    {
                        try
                        {
                            var produtoDeposito = _produtoDepositoRepository.ObterPorCodigoSistemaEDeposito(venda.CodigoSistema, venda.Deposito);

                            if (produtoDeposito == null)
                            {
                                erros.Add($"Erro: Produto {venda.CodigoSistema} não está associado ao depósito {venda.Deposito}.");
                                continue; // Continua para a próxima venda
                            }

                            // Chamando o serviço de upload de venda
                            _produtoGeralDomainService.UploadVenda(produtoDeposito.Id, venda.QuantidadeVendida, matricula, venda.DataVenda);
                        }
                        catch (Exception ex)
                        {
                            erros.Add($"Erro ao processar a venda do produto {venda.CodigoSistema}: {ex.Message}");
                            // Continua para a próxima venda
                        }
                    }

                    if (erros.Any())
                    {
                        // Retorna um status 207 (Multi-Status) para indicar que algumas operações falharam
                        return StatusCode(207, new { message = "Vendas processadas.", erros });
                    }
                }

                return Ok(new { message = "Vendas carregadas com sucesso!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }






    }



}

