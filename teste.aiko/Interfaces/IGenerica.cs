using System;
using System.Threading.Tasks;

namespace teste.aiko.Interfaces
{
    
    public interface IGenerica<T> where T : class
    {
        Task<T>  GetAsync(Guid id);
        
        Task<int> CriarAsync(T entity);
        
        Task<int> EditarAsync(T entity);
        
        Task<int> ExcluirAsync(T entity);
        
        Task<int> SalvarAsync();
    }
}
