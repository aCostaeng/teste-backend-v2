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
    public class IEquipamentoModeloEstadoHoraRepo : IGenerica<EquipmentModelStateHourlyEarning>
    {
        private readonly Contexto _contexto;
        private readonly IEquipamentoRepo _equipamentoRepo;

        public IEquipamentoModeloEstadoHoraRepo(Contexto context, IEquipamentoRepo equipamentoRepo)
        {
            _contexto = context;
            _equipamentoRepo = equipamentoRepo;
        }

        public async Task<int> CriarAsync(EquipmentModelStateHourlyEarning entity)
        {
            await _contexto.AddAsync(entity);
            return await SalvarAsync();
        }

        public async Task<int> EditarAsync(EquipmentModelStateHourlyEarning entity)
        {
            _contexto.Entry(entity).State = EntityState.Modified;
            return await SalvarAsync();

        }

        public Task<int> ExcluirAsync(EquipmentModelStateHourlyEarning entity)
        {
             _contexto.Set<EquipmentModelStateHourlyEarning>().Remove(entity);
            return SalvarAsync();
        }

        public async Task<EquipmentModelStateHourlyEarning> GetAsync(Guid id)
        {
            return await _contexto.Set<EquipmentModelStateHourlyEarning>().FindAsync(id);
        }

        public async Task<int> SalvarAsync()
        {
            return await _contexto.SaveChangesAsync();
        }

        public async Task<IEnumerable<EquipamentoModeloEstadoHoraOutput>> ListaAsync()
        {
            return await _contexto.EquipmentModelStateHourlyEarnings.OrderBy(x => x.EquipmentModelId).Select(x => new EquipamentoModeloEstadoHoraOutput(x.Id, x.EquipmentStateId, x.EquipmentModelId, x.Value, x.EquipmentModel.Name, x.EquipmentState.Name)).ToListAsync();
        }
        
    }
}
