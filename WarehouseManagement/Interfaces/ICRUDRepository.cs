using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagement.Models;

namespace WarehouseManagement.Interfaces
{
    public interface ICRUDRepository<T> where T : class
    {
        Task Create(T t);
        Task<T> Read(int id);
        Task<List<T>> ReadAll();
        Task Update(T t);
        Task Delete(int id);
    }
}
