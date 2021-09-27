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
    public class EquipamentoModeloController : Controller
    {
        private readonly IEquipamentoModeloRepo _equipamentoModeloRepo;
        private GerarNovoIdGuid GuidId = new();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipamentoModeloRepo"></param>
        public EquipamentoModeloController(IEquipamentoModeloRepo equipamentoModeloRepo)
        {
            _equipamentoModeloRepo = equipamentoModeloRepo;
        }

  
        [HttpPost]
        [Route("api/v1/criar")]
        public async Task<IActionResult> CriarAsync([FromBody] EquipamentoModeloInput equipamentoModeloInput)
        {
            EquipmentModel equipmentModel = new()
            {
                Id = GuidId.Guid,
                Name = equipamentoModeloInput.Nome
            };
            return await _equipamentoModeloRepo.CriarAsync(equipmentModel) > 0 ? new StatusCodeResult(201) : new BadRequestResult();
        }

        [HttpDelete]
        [Route("api/v1/excluir/{id}")]
        public async Task<IActionResult> ApagarAsync(Guid id)
        {
            EquipmentModel equipmentModel = await _equipamentoModeloRepo.GetAsync(id);
            return await _equipamentoModeloRepo.ExcluirAsync(equipmentModel) > 0 ? new OkResult() : new BadRequestResult();
        }

        [HttpPut]
        [Route("api/v1/editar")]
        public async Task<IActionResult> EditarAsync([FromBody] EquipamentoModeloOutput equipamentoModeloOutput)
        {
            EquipmentModel equipmentModel = await _equipamentoModeloRepo.GetAsync(equipamentoModeloOutput.Id);
            equipmentModel.Name = equipamentoModeloOutput.Nome;
            return await _equipamentoModeloRepo.EditarAsync(equipmentModel) > 0 ? new StatusCodeResult(200) : new BadRequestResult();
        }

        [HttpGet]
        [Route("api/v1/listar")]
        public async Task<IActionResult> ListarAsync()
        {
            return new OkObjectResult(await _equipamentoModeloRepo.ListarAsync());
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult EquipamentoModelo()
        {
            return View();
        }
    }
}
