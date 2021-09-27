using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;
using teste.aiko.Filtros;
using teste.aiko.Modelos;
using teste.aiko.Repository;
using teste.aiko.Utils;
using teste.aiko.ViewModel;

namespace teste.aiko.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class EquipamentoController : Controller
    {
        private readonly IEquipamentoRepo _equipamentoRepo;
        private GerarNovoIdGuid GuidId = new (); 


        public EquipamentoController(IEquipamentoRepo equipamentoRepo)
        {
            _equipamentoRepo = equipamentoRepo;
        }

        [HttpGet]
        [Route("api/v1/{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            Equipment equipment = await _equipamentoRepo.GetAsync(id);
            if (equipment == null) return new NoContentResult();
            return new OkObjectResult(equipment);
        }

        
        [HttpPost]
        [Route("api/v1/criar")]
        [ValidadorCamposOutputFiltro]
        public async Task<IActionResult> CriarAsync([FromBody] EquipamentoInput equipamentoInput)
        {
            Equipment equipment = new()
            {
                Id = GuidId.Guid,
                Name = equipamentoInput.Nome,
                EquipmentModelId = equipamentoInput.EquipamentoModeloId
            };
            return await _equipamentoRepo.CriarAsync(equipment) > 0 ? new StatusCodeResult(201) : new BadRequestResult();
        }

        
        [HttpDelete]
        [Route("api/v1/excluir/{id}")]
        public async Task<IActionResult> ExcluirAsync(Guid id)
        {
            Equipment equipment = await _equipamentoRepo.GetAsync(id);
            if (equipment == null) return new BadRequestResult();
            return await _equipamentoRepo.ExcluirAsync(equipment) > 0 ? new OkResult() : new BadRequestResult();
        }

        [HttpPut]
        [Route("api/v1/editar")]
        public async Task<IActionResult> EditarAsync([FromBody] EquipamentoOutput equipamentoOutput)
        {
            Equipment equipment = await _equipamentoRepo.GetAsync(equipamentoOutput.Id);
            equipment.Name = equipamentoOutput.Nome;
            equipment.EquipmentModelId = equipamentoOutput.EquipamentoModeloId;
            return await _equipamentoRepo.EditarAsync(equipment) > 0 ? new StatusCodeResult(200) : new BadRequestResult();
        }
        
       
        [HttpGet]
        [Route("api/v1/listar")]
        public async Task<IActionResult> ListarAsync ()
        {
            return new OkObjectResult(await _equipamentoRepo.ListarAsync());
        }

        
        [HttpGet]
        [Route("api/v1/listar/id")]
        public async Task<IActionResult> ListarPeloIdAsync(Guid id)
        {
            return new OkObjectResult(await _equipamentoRepo.ListarPeloIdAsync(id));
        }

        [HttpGet]
        [Route("api/v1/produtividadeEquipamento/{idEquipamento}/{horasOperando}")]
        public async Task<IActionResult> GetProdutividadeEquipamentoAsync(Guid idEquipamento, float horasOperando)
        {
            var percentagemProdutividade = await _equipamentoRepo.GetProdutividadeEquipamentoAsync(idEquipamento, horasOperando);
            return new OkObjectResult(percentagemProdutividade + " %");
        }

        [HttpGet]
        [Route("api/v1/ganhosPorEquipamento/{idEquipamento}/{totalHorasOperando}/{totalHorasManuntencao}")]
        public async Task<IActionResult> GetGanhosPorEquipamentoAsync(Guid idEquipamento, float totalHorasOperando, float totalHorasManuntencao)
        {
            var ganhosPorEquipamento = await _equipamentoRepo.GetGanhosPorEquipamentoAsync(idEquipamento, totalHorasOperando, totalHorasManuntencao);
            return new OkObjectResult(ganhosPorEquipamento);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Equipamento()
        {
            return View();
        }

    }

}
