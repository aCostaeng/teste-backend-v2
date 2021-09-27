using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using teste.aiko.ViewModel;
using System;
using teste.aiko.Utils;
using teste.aiko.Repository;
using System.Threading.Tasks;
using teste.aiko.Modelos;

namespace teste.aiko.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class EquipamentoPosicoesHistoricoController : Controller
    {
        private readonly IEquipamentoPosicoesHistoricoRepo _equipamentoHistoricoPosicoesRepo;
        private GerarNovoIdGuid GuidId = new();

        public EquipamentoPosicoesHistoricoController(IEquipamentoPosicoesHistoricoRepo equipamentoHistoricoPosicoesRepo)
        {
            _equipamentoHistoricoPosicoesRepo = equipamentoHistoricoPosicoesRepo;
        }

        [HttpPost]
        [Route("api/v1/criar")]
        public async Task<IActionResult> CriarAsync([FromBody] EquipamentoPosicoesHistoricoInput historicoPosicoesInput)
        {
            EquipmentPositionHistory equipmentPosition = new()
            {
                Id = GuidId.Guid,
                EquipmentId = historicoPosicoesInput.IdEquipamnto,
                Lat = historicoPosicoesInput.Lat,
                Lon = historicoPosicoesInput.Lon,
                Date = historicoPosicoesInput.Data
            };
            return await _equipamentoHistoricoPosicoesRepo.CriarAsync(equipmentPosition) > 0 ? new StatusCodeResult(201) : new BadRequestResult();
        }


        [HttpDelete]
        [Route("api/v1/excluir/{id}")]
        public async Task<IActionResult> ApagarAsync(Guid id)
        {
            EquipmentPositionHistory equipmentPositionHistory = await _equipamentoHistoricoPosicoesRepo.GetAsync(id);
            return await _equipamentoHistoricoPosicoesRepo.ExcluirAsync(equipmentPositionHistory) > 0 ? new OkResult() : new BadRequestResult();
        }


        [HttpGet]
        [Route("listar")]
        public async Task<IActionResult> ListarAsync()
        {
            return new OkObjectResult(await _equipamentoHistoricoPosicoesRepo.ListarAsync());
        }

        [HttpGet]
        [Route("api/v1/posicaoActualEquipamento/id")]
        public async Task<IActionResult> GetPosicaoActualEquipamentoAsync(Guid id)
        {
            return new OkObjectResult(await _equipamentoHistoricoPosicoesRepo.GetPosicaoActualEquipamentoAsync(id));
        }


        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult EquipamentoPosicoesHistorico()
        {
            return View();
        }
        
    }
}
