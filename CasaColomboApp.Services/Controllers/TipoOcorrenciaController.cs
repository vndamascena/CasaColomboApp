using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Domain.Services;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Produto;
using CasaColomboApp.Services.Model.TipoOcorrencia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaColomboApp.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoOcorrenciaController : ControllerBase
    {
        private readonly ITipoOcorrenciaDomainService? _tipoOcorrenciaDomainService;
        private readonly IMapper? _mapper;

        public TipoOcorrenciaController
            (ITipoOcorrenciaDomainService? tipoOcorrenciaDomainService, IMapper? mapper)
        {
            _tipoOcorrenciaDomainService = tipoOcorrenciaDomainService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<TipoOcorrenciaGetModel>), 200)]
        public IActionResult GetAll()
        {
            var tipoocorrencia = _tipoOcorrenciaDomainService?.Consultar();
            var result = _mapper?.Map<List<TipoOcorrenciaGetModel>>(tipoocorrencia);
            return Ok(result);
            

        }

        [HttpPost]
        [ProducesResponseType(typeof(TipoOcorrenciaGetModel), 201)]
        public IActionResult PostModel([FromBody] TipoOcorrenciaPostModel model)
        {
            try
            {
                var tipoocorrencia = _mapper?.Map<TipoOcorrencia>(model);
                var result = _tipoOcorrenciaDomainService?.Cadastrar(tipoocorrencia);

                return StatusCode(201, _mapper.Map<TipoOcorrenciaGetModel>(result));

            }
            catch (Exception e) 
            {
                return StatusCode(500, new { message = e.Message });            
            
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(TipoOcorrenciaGetModel), 201)]
        public IActionResult PutModel([FromBody] TipoOcorrenciaPutModel model)
        {
            try
            {

                var tipoOcorrencia = _mapper?.Map<TipoOcorrencia>(model);

                
                var result = _tipoOcorrenciaDomainService?.Atualizar(tipoOcorrencia);

                var tipoOcorreciaGetModel = _mapper.Map<TipoOcorrenciaGetModel>(result);

                //HTTP 201 (OK)
                return StatusCode(200, new
                {
                    Message = "Produto atualizado com sucesso",
                    tipoOcorreciaGetModel
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            try
            {
                var result = _tipoOcorrenciaDomainService?.Delete(id);
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




    }
}
