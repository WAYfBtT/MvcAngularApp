using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<TModel, TKey>
        where TModel : class
    {
        public Task<TKey> AddAsync(TModel model);
        public Task<TModel> GetByIdAsync(TKey id);
        public Task<IEnumerable<TModel>> GetAllAsync();
        public Task<IEnumerable<TModel>> GetAllAsync(int skip, int take);
        public Task<TModel> UpdateAsync(TModel model);
        public Task DeleteByIdAsync(TKey id);
        public Task DeleteAsync(TModel model);
    }
}
