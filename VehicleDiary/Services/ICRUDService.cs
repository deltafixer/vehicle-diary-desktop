using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleDiary.Services
{
    public interface ICRUDService<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get<S>(S id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete<S>(S id);
    }
}
