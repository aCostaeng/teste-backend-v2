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
    
    public class IEquipamentoModeloRepo : IGenerica<EquipmentModel>
    {

        private readonly Contexto _contexto;
        
        public IEquipamentoModeloRepo(Contexto contexto)
        {
            this._contexto = contexto;
        }

        public async Task<int> CriarAsync(EquipmentModel entity)
        {
            await _contexto.AddAsync(entity);
            return await SalvarAsync();
        }

        public async Task<int> EditarAsync(EquipmentModel entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            return await SalvarAsync();

        }

        public Task<int> ExcluirAsync(EquipmentModel entity)
        {
            _contexto.Set<EquipmentModel>().Remove(entity);
            return SalvarAsync();
        }

        public async Task<EquipmentModel> GetAsync(Guid id)
        {
            return await _contexto.Set<EquipmentModel>().FindAsync(id);
        }

        public async Task<int> SalvarAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public async Task<List<EquipmentModel>> ListarAsync()
        {
            return await _contexto.EquipmentModels.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
