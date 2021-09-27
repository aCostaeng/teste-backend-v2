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
   
    public class IEquipamentoEstadoHistoricoRepo : IGenerica<EquipmentStateHistory>
    {
        private readonly Contexto _contexto;
        
        public IEquipamentoEstadoHistoricoRepo(Contexto context)
        {
            _contexto = context;
        }

        public async Task<int> CriarAsync(EquipmentStateHistory entity)
        {
            await _contexto.AddAsync(entity);
            return await SalvarAsync();
        }

        public async Task<EquipmentStateHistory> GetAsync(Guid id)
        {
            return await _contexto.Set<EquipmentStateHistory>().FindAsync(id);
        }

        public async Task<int> EditarAsync(EquipmentStateHistory entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            return await SalvarAsync();

        }

        public Task<int> ExcluirAsync(EquipmentStateHistory entity)
        {
            _contexto.Set<EquipmentStateHistory>().Remove(entity);
            return SalvarAsync();
        }

        public async Task<int> SalvarAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public async Task<List<EquipamentoEstadoHistoricoOutput>> ListarAsync()
        {
            return await _contexto.EquipmentStateHistories.OrderBy(x => x.Equipment.Name).ThenByDescending(x=> x.Date).Include(x=> x.Equipment).Include(x => x.EquipmentState).Select(x => new EquipamentoEstadoHistoricoOutput(x.Id, x.Date.ToString("dd-MM-yy hh:MM:ss"), x.Equipment.Name, x.EquipmentState.Name)).ToListAsync();
        }

        public async Task<IEnumerable<EquipamentoEstadoHistoricoOutput>> ListarHistoricoEstadoEquipamentoAsync(Guid idEquipamento)
        {
            return await _contexto.EquipmentStateHistories.OrderByDescending(x => x.Date).Include(x => x.EquipmentState).Where(x=> x.EquipmentId == idEquipamento).Select(x => new EquipamentoEstadoHistoricoOutput(x.Id, x.Date.ToShortDateString(), x.Equipment.Name, x.EquipmentState.Name)).ToListAsync();
        }

        public async Task<EquipamentoEstadoHistoricoOutput> GetEstadoActualEquipamentoAsync(Guid idEquipamento)
        {
            return await _contexto.EquipmentStateHistories.OrderByDescending(x => x.Date).Include(x => x.EquipmentState).Where(x => x.EquipmentId == idEquipamento).Select(x => new EquipamentoEstadoHistoricoOutput(x.Id, x.Date.ToShortDateString(), x.Equipment.Name, x.EquipmentState.Name)).FirstOrDefaultAsync();
        }

    }
}
