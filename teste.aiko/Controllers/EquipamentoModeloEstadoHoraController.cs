using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using teste.aiko.Repository;
using teste.aiko.Utils;
using teste.aiko.Modelos;
using teste.aiko.ViewModel;

namespace teste.aiko.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EquipamentoModeloEstadoHoraController : Controller
    {
        private readonly IEquipamentoModeloEstadoHoraRepo _modeloEstadoHoraRepo;
        private GerarNovoIdGuid GuidId = new();

        public EquipamentoModeloEstadoHoraController(IEquipamentoModeloEstadoHoraRepo modeloEstadoHoraRepo)
        {
            this._modeloEstadoHoraRepo = modeloEstadoHoraRepo;
        }

       
        [HttpPost]
        [Route("api/v1/criar")]
        public async Task<IActionResult> CriarAsync([FromBody] EquipamentoModeloEstadoHoraInput equipamentoEstadoHistoricoInput)
        {
            EquipmentModelStateHourlyEarning equipmentModelStateHourly = new()
            {
                Id = GuidId.Guid,
                EquipmentModelId = equipamentoEstadoHistoricoInput.IdModelo,
                EquipmentStateId = equipamentoEstadoHistoricoInput.IdEstado,
                Value = equipamentoEstadoHistoricoInput.Valor
            };
            return await _modeloEstadoHoraRepo.CriarAsync(equipmentModelStateHourly) > 0 ? new StatusCodeResult(201) : new BadRequestResult();
        }

       
        [HttpDelete]
        [Route("api/v1/excluir/{id}")]
        public async Task<IActionResult> EscluirAsync(Guid id)
        {
            EquipmentModelStateHourlyEarning equipmentModelStateHourly  = await _modeloEstadoHoraRepo.GetAsync(id);
            return await _modeloEstadoHoraRepo.ExcluirAsync(equipmentModelStateHourly) > 0 ? new OkResult() : new BadRequestResult();
        }

     
        [HttpPut]
        [Route("api/v1/editar/")]
        public async Task<IActionResult> EditarAsync(EquipamentoModeloEstadoHoraOutput equipamentoModeloEstadoHoraOutput)
        {
            var equipmentModelStateHourly = await _modeloEstadoHoraRepo.GetAsync(equipamentoModeloEstadoHoraOutput.Id);
            equipmentModelStateHourly.EquipmentModelId = equipamentoModeloEstadoHoraOutput.IdEquipamento;
            equipmentModelStateHourly.EquipmentStateId = equipamentoModeloEstadoHoraOutput.IdEstado;
            equipmentModelStateHourly.Value = equipamentoModeloEstadoHoraOutput.Valor;

            return await _modeloEstadoHoraRepo.ExcluirAsync(equipmentModelStateHourly) > 0 ? new OkResult() : new BadRequestResult();
        }

        [HttpGet]
        [Route("api/v1/listar")]
        public async Task<IActionResult> ListarAsync ()
        {
            return new OkObjectResult(await _modeloEstadoHoraRepo.ListaAsync());
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult EquipamentoModeloEstadoHora()
        {
            return View();
        }
    }
}
