using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teste.aiko.Interfaces;
using teste.aiko.Modelos;

namespace teste.aiko.Repository
{
   
    public class IEquipamentoEstadoRepo : IGenerica<EquipmentState>
    {
        private readonly Contexto _contexto;

      
        public IEquipamentoEstadoRepo(Contexto context)
        {
            this._contexto = context;
        }

        public async Task<int> CriarAsync(EquipmentState entity)
        {
            await _contexto.AddAsync(entity);
            return await SalvarAsync();
        }

        public async Task<int> EditarAsync(EquipmentState entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            return await SalvarAsync();

        }

        public Task<int> ExcluirAsync(EquipmentState entity)
        {
            _contexto.Set<EquipmentState>().Remove(entity);
            return SalvarAsync();
        }

        public async Task<EquipmentState> GetAsync(Guid id)
        {
            return await _contexto.Set<EquipmentState>().FindAsync(id);
        }

        public async Task<List<EquipmentState>> ListarAsync()
        {
            return await _contexto.EquipmentStates.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<int> SalvarAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

    }
}
