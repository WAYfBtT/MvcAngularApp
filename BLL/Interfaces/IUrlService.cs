using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUrlService
    {
        public Task<int> AddAsync(UrlModel model);
        public Task<UrlModel> GetByIdAsync(int id);
        public Task<IEnumerable<UrlModel>> GetAllAsync();
        public Task<IEnumerable<UrlModel>> GetAllAsync(int skip, int take);
        public Task DeleteByIdAsync(int id);
        public Task DeleteAsync(UrlModel model);
        public Task<UrlModel> GetByShortUrlAsync(string shortUrl);
        public Task<IEnumerable<UrlModel>> GetByUserIdAsync(int userId);
    }
}
