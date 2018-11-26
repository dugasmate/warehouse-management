using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Models;

namespace WarehouseManagement.Interfaces
{
    public interface ICRUDRepository<T> where T : class
    {
        Task CreateAsync(T t);
        Task<T> ReadAsync(long id);
        Task<List<T>> ReadAllAsync();
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);
    }
}
