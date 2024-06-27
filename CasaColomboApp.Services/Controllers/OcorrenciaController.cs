using AutoMapper;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Domain.Services;
using CasaColomboApp.Services.Model.Ocorrencias;
using CasaColomboApp.Services.Model.Produto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Services.Model.Ocorrencia;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace CasaColomboApp.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OcorrenciaController : ControllerBase
    {
        private readonly IOcorrenciaDomainService? _ocorrenciaDomainService;
        private readonly IMapper? _mapper;
        private readonly HttpClient _httpClient;
        private readonly IBaixaOcorrenciaRepository _baixaOcorrenciaRepository;

        public OcorrenciaController
            (IOcorrenciaDomainService? ocorrenciaDomainService, IMapper mapper, IHttpClientFactory httpClientFactory, IBaixaOcorrenciaRepository baixaOcorrencia)
        {
            _mapper = mapper;
            _ocorrenciaDomainService = ocorrenciaDomainService;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://colombo01-001-site2.gtempurl.com/usuarios/autenticar");
            _baixaOcorrenciaRepository = baixaOcorrencia;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OcorrenciaGetModel>), 200)]

        public IActionResult GetAll()
        {
            try
            {
                var ocorrencia = _ocorrenciaDomainService.Consultar();
                var OcorrenciaModel = _mapper.Map<List<OcorrenciaGetModel>>(ocorrencia);


                return Ok(OcorrenciaModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OcorrenciaGetModel), 200)]
        public IActionResult GetById(int id)
        {
            try
            {
                // Consulta a ocorrencia pelo ID
                var ocorrencia = _ocorrenciaDomainService.ObterPorId(id);

                // Mapeia a ocorrencia para o modelo de resposta 
                var ocorrenciaModel = _mapper.Map<OcorrenciaGetModel>(ocorrencia);

                return Ok(ocorrenciaModel);
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }


        

        [HttpPost]
        [ProducesResponseType(typeof(OcorrenciaGetModel), 201)]
        public async Task<IActionResult> Post([FromBody] OcorrenciaPostModel model, string matricula, string senha)
        {
            try
            {
                // Verifique se o usuário está autorizado
                if (!(await AutenticarUsuario(matricula, senha)))
                {
                    return StatusCode(401, new { error = "Usuário não autorizado." });
                }

                // Mapear o modelo para a entidade ocorrencia
                var ocorrencia = _mapper.Map<Ocorrencia>(model);

                var result = _ocorrenciaDomainService.Cadastrar(ocorrencia, matricula);

                // Mapear o resultado de volta para o modelo de resposta
                var ocorrenciaGetModel = _mapper.Map<OcorrenciaGetModel>(result);

                // Retornar resposta HTTP 201 (CREATED) com o modelo de resposta
                return StatusCode(201, new
                {
                    Message = "Ocorrência cadastrado com sucesso",
                    ocorrenciaGetModel
                });
            }
            catch (Exception e)
            {
                //HTTP 500 (INTERNAL SERVER ERROR)
                return StatusCode(500, new { e.Message });
            }
        }

       


        [HttpPost("baixaOcorrencia")]
        public async Task<IActionResult> BaixaOcorrencia(string matricula, string senha, int Id, [FromBody] BaixaOcorrenciaPostModel baixa)
        {
            try
            {
                if (await AutenticarUsuario(matricula, senha))
                {
                    _ocorrenciaDomainService.BaixaOcorrencia(Id, matricula);
                    return Ok(new { message = "Baixa confirmada com sucesso!" });
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

        [HttpGet("baixaOcorrencia")]
        [ProducesResponseType(typeof(List<BaixaOcorrenciaGetModel>), 200)]
        public IActionResult BaixaOcorrenciaAll()
        {
            try
            {
                var baixaOcorrencia = _ocorrenciaDomainService.ConsultarBaixa();
                var baixaOcorrenciaModel = _mapper.Map<List<BaixaOcorrenciaGetModel>>(baixaOcorrencia);
                return Ok(baixaOcorrenciaModel);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { erro = e.Message });
            }
        }


        private async Task<bool> AutenticarUsuario(string matricula, string senha)
        {
            try
            {
                var usuarioModel = new { Matricula = matricula, Senha = senha };
                var json = JsonSerializer.Serialize(usuarioModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/usuarios/autenticar", content); // Substitui "rota-da-autenticacao" pela rota de autenticação da API
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch
            {
                return false;
            }
        }




    }
}
