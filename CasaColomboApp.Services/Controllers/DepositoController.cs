using AutoMapper;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Deposito;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CasaColomboApp.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepositoController : ControllerBase
    {
        private readonly IDepositoDomainService? _depositoDomainService;
        private readonly IMapper? _mapper;
        public DepositoController

        (IDepositoDomainService? depositoDomainService, IMapper? mapper)

        {
            _depositoDomainService = depositoDomainService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetDeposito() 
        {
            var deposito = _depositoDomainService?.Consultar();
            var result = _mapper?.Map<List<DepositoGetModel>>(deposito);
            return Ok(result);
        }
    }
}
