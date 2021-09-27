using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using teste.aiko.Modelos;
using teste.aiko.Repository;
using teste.aiko.Utils;
using teste.aiko.ViewModel;

namespace teste.aiko.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class EquipamentoEstadoHistoricoController : Controller
    {
        private readonly IEquipamentoEstadoHistoricoRepo _equipamentoEstadoHistoricoRepo;
        private GerarNovoIdGuid GuidId = new();


        public EquipamentoEstadoHistoricoController(IEquipamentoEstadoHistoricoRepo equipamentoEstadoHistoricoRepo)
        {
            _equipamentoEstadoHistoricoRepo = equipamentoEstadoHistoricoRepo;
        }

        
      
        [HttpPost]
        [Route("api/v1/criar")]
        public async Task<IActionResult> CriarAsync([FromBody] EquipamentoEstadoHistoricoInput equipamentoEstadoHistoricoInput)
        {
            EquipmentStateHistory equipmentState = new()
            {
                Id = GuidId.Guid,
                EquipmentId = equipamentoEstadoHistoricoInput.IdEquipamento,
                EquipmentStateId = equipamentoEstadoHistoricoInput.IdEstado,
                Date = DateTime.Now
            }; 
            return await _equipamentoEstadoHistoricoRepo.CriarAsync(equipmentState) > 0 ? new StatusCodeResult(201) : new BadRequestResult();
        }

        [HttpGet]
        [Route("api/v1/listar")]
        public async Task<IActionResult> ListarAsync()
        {
            return new OkObjectResult(await _equipamentoEstadoHistoricoRepo.ListarAsync());
        }

        [HttpGet]
        [Route("api/v1/listar/{id}")]
        public async Task<IActionResult> ListarHistorricoPeloEstadoAsync(Guid id)
        {
            return new OkObjectResult(await _equipamentoEstadoHistoricoRepo.ListarHistoricoEstadoEquipamentoAsync(id));
        }


        [HttpDelete]
        [Route("api/v1/excluir/{id}")]
        public async Task<IActionResult> EscluitAsync(Guid id)
        {
            EquipmentStateHistory equipmentStateHistory = await _equipamentoEstadoHistoricoRepo.GetAsync(id);
            return await _equipamentoEstadoHistoricoRepo.ExcluirAsync(equipmentStateHistory) > 0 ? new OkResult() : new BadRequestResult();
        }

        [HttpGet]
        [Route("api/v1/estadoActualEquipamento/id")]
        public async Task<IActionResult> GetPosicaoActualEquipamentoAsync(Guid id)
        {
            return new OkObjectResult(await _equipamentoEstadoHistoricoRepo.GetEstadoActualEquipamentoAsync(id));
        }


        [HttpGet]
        [Route("HistoricoEquipamento")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult HistoricoEquipamento()
        {
            return View();
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult EquipamentoEstadoHistorico()
        {
            return View();
        }


    }
}
