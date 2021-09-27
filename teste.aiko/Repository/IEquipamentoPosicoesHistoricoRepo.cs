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
    
    public class IEquipamentoPosicoesHistoricoRepo : IGenerica<EquipmentPositionHistory>
    {

        private readonly Contexto _contexto;

       
        public IEquipamentoPosicoesHistoricoRepo(Contexto contexto)
        {
            _contexto = contexto;
        }

        public async Task<int> CriarAsync(EquipmentPositionHistory entity)
        {
            await _contexto.AddAsync(entity);
            return await SalvarAsync();
        }

        public async Task<int> EditarAsync(EquipmentPositionHistory entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            return await SalvarAsync();

        }

        public Task<int> ExcluirAsync(EquipmentPositionHistory entity)
        {
            _contexto.Set<EquipmentPositionHistory>().Remove(entity);
            return SalvarAsync();
        }

        public async Task<EquipmentPositionHistory> GetAsync(Guid id)
        {
            return await _contexto.Set<EquipmentPositionHistory>().FindAsync(id);
        }

        public async Task<int> SalvarAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public async Task<List<EquipamentoPosicoesHistoricoOutput>> ListarAsync()
        {
            return await _contexto.EquipmentPositionHistories.OrderByDescending(x => x.Date).Include(x => x.Equipment).Select(x => new EquipamentoPosicoesHistoricoOutput((Guid)x.Id, x.EquipmentId, x.Equipment.Name, x.Date.ToString("dd-MM-yy hh:MM:ss"), x.Lat, x.Lon)).ToListAsync();
        }

        public async Task<EquipmentPositionHistory> GetPosicaoActualEquipamentoAsync(Guid id)
        {
            return await _contexto.EquipmentPositionHistories.OrderByDescending(x => x.Date).Where(x => x.EquipmentId == id).FirstOrDefaultAsync();
        }
    }
}
