using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste.aiko.Interfaces;
using teste.aiko.Modelos;
using teste.aiko.ViewModel;

namespace teste.aiko.Repository
{
  
    public class IEquipamentoRepo : IGenerica<Equipment>
    {
        private readonly Contexto _contexto;
        private readonly string ESTADO_OPERANDO = "Operando";
        private readonly string ESTADO_MANUNTECAO = "Manutenção";
        public IEquipamentoRepo(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> CriarAsync(Equipment entity)
        {
            await _contexto.AddAsync(entity);
            return await SalvarAsync();
        }

        public async Task<int> EditarAsync(Equipment entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            return await SalvarAsync();

        }

        public Task<int> ExcluirAsync(Equipment entity)
        {
            _contexto.Set<Equipment>().Remove(entity);
            return SalvarAsync();
        }

        public async Task<Equipment> GetAsync(Guid id)
        {
            return await _contexto.Set<Equipment>().FindAsync(id);
        }

        public async Task<int> SalvarAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<EquipamentoOutput>> ListarPeloIdAsync(Guid id)
        {
            return await _contexto.Equipment.OrderBy(x => x.Name).Include(x => x.EquipmentModel).Where(x => x.Id == id).Select(x => new EquipamentoOutput(x.Id, x.Name, x.EquipmentModel.Name)).ToListAsync();
        }

        public async Task<IEnumerable<EquipamentoOutput>> ListarAsync()
        {
            return await _contexto.Equipment.OrderBy(x => x.Name).Include(x => x.EquipmentModel).Select(x => new EquipamentoOutput(x.Id, x.Name, x.EquipmentModel.Name)).ToListAsync();
        }

        public async Task<float> GetProdutividadeEquipamentoAsync(Guid idEquipamento, float horasOperando)
        {
            var equipamento = await GetAsync(idEquipamento);
            if (equipamento == null) return 0;
            var equipamentoEstado = _contexto.EquipmentStateHistories.Where(x => x.EquipmentState.Name.Equals(ESTADO_OPERANDO) && x.EquipmentId == equipamento.Id).FirstOrDefault();
            if (equipamentoEstado == null) return 0;
            return horasOperando / 24 * 100;
        }

        public async Task<float> GetGanhosPorEquipamentoAsync (Guid idEquipamento, float totalHorasOperando, float totalHorasManuntencao)
        {
            var equipamento = await GetAsync(idEquipamento);
            if (equipamento == null) return 0;
            var equipamentoGanhosManuntencao = _contexto.EquipmentModelStateHourlyEarnings.Where(x => x.EquipmentState.Name.Equals(ESTADO_MANUNTECAO) && x.EquipmentModelId == equipamento.EquipmentModelId).FirstOrDefault();
            var equipamentoGanhosOperando = _contexto.EquipmentModelStateHourlyEarnings.Where(x => x.EquipmentState.Name.Equals(ESTADO_OPERANDO) && x.EquipmentModelId == equipamento.EquipmentModelId).FirstOrDefault();
            
            if (equipamentoGanhosManuntencao != null && equipamentoGanhosOperando != null)
                return totalHorasOperando * equipamentoGanhosOperando.Value + totalHorasManuntencao * equipamentoGanhosManuntencao.Value;
            else if (equipamentoGanhosManuntencao != null)
                return totalHorasManuntencao * equipamentoGanhosManuntencao.Value * totalHorasManuntencao;
            else if (equipamentoGanhosOperando != null)
                return totalHorasOperando * equipamentoGanhosOperando.Value;
            return 0;
        }
    }
}
