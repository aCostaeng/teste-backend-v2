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
    
    [Route("[controller]")]
    [ApiController]
    public class EquipamentoEstadoController : Controller
    {
        
        private readonly IEquipamentoEstadoRepo _equipamentoEstadoRepo;
        private GerarNovoIdGuid GuidId = new();

        
        public EquipamentoEstadoController(IEquipamentoEstadoRepo equipamentoEstadoRepo)
        {
            _equipamentoEstadoRepo = equipamentoEstadoRepo;
        }
        
        
        [HttpPost]
        [Route("api/v1/criar")]
        public async Task<IActionResult> PostAsync([FromBody] EquipamentoEstadoInput equipamentoEstadoInput)
        {
            EquipmentState equipmentState = new ()
            {
                Id = GuidId.Guid,
                Color = equipamentoEstadoInput.Cor,
                Name = equipamentoEstadoInput.Nome
            };
            return await _equipamentoEstadoRepo.CriarAsync(equipmentState) > 0 ? new StatusCodeResult(201) : new BadRequestResult();
        }

       
        [HttpGet]
        [Route("api/v1/listar")]
        public async Task<IActionResult> GetAsync()
        {
            return new OkObjectResult(await _equipamentoEstadoRepo.ListarAsync());
        }

      
        [HttpDelete]
        [Route("api/v1/excluir/{id}")]
        public async Task<IActionResult> ExcluirAsync(Guid id)
        {
            EquipmentState equipmentState = await _equipamentoEstadoRepo.GetAsync(id);
            if (equipmentState == null) return new BadRequestResult();
            return await _equipamentoEstadoRepo.ExcluirAsync(equipmentState) > 0 ? new OkResult() : new BadRequestResult();
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult EquipamentoEstado()
        {
            return View();
        }
    }
}
