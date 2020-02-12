using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();
        Task<T> Get(int id);
        Task Create(T item);
        T Update(T item);
        Task Delete(int id);
    }
}
