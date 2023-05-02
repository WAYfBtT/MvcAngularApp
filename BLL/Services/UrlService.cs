using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UrlService : IUrlService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IUrlShortenerService _shortenerService;

        public UrlService(IMapper mapper, IUnitOfWork unitOfWork, IUrlShortenerService shortenerService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _shortenerService = shortenerService;
        }

        public async Task<int> AddAsync(UrlModel model)
        {
            var entity = _mapper.Map<Url>(model);
            if (await _unitOfWork.UrlRepository.AnyAsync(x => x.LongUrl == entity.LongUrl))
                throw new UrlShortenerApplicationException(new Dictionary<string, List<string>> { ["LongUrl"] = new List<string> { "Such url already exists." } });
           

            entity.ShortUrl = _shortenerService.ShortenUrl(entity.LongUrl);

            _unitOfWork.UrlRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!(await _unitOfWork.UrlRepository.AnyAsync(x => x.Id == id)))
                throw new UrlShortenerApplicationException
                    (new Dictionary<string, List<string>> { ["Id"] = new List<string> { "Url with such id does not exist." } });

            await _unitOfWork.UrlRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(UrlModel model)
        {
            var entity = _mapper.Map<Url>(model);
            if (await _unitOfWork.UrlRepository.AnyAsync(x => x.Id == model.Id))
                throw new UrlShortenerApplicationException(
                    new Dictionary<string, List<string>> { ["Id"] = new List<string> { "Url with such id does not exist." } });

            _unitOfWork.UrlRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UrlModel>> GetAllAsync()
            => _mapper.Map<IEnumerable<UrlModel>>(await _unitOfWork.UrlRepository.GetAllAsync());

        public async Task<IEnumerable<UrlModel>> GetAllAsync(int skip, int take)
            => _mapper.Map<IEnumerable<UrlModel>>(await _unitOfWork.UrlRepository.GetAllAsync(skip, take));

        public async Task<UrlModel> GetByIdAsync(int id)
            => _mapper.Map<UrlModel>(
                await _unitOfWork.UrlRepository.GetByIdAsync(id));

        public async Task<UrlModel> GetByShortUrlAsync(string shortUrl)
            => _mapper.Map<UrlModel>(
                await _unitOfWork.UrlRepository.GetByShortUrlAsync(shortUrl));

        public async Task<IEnumerable<UrlModel>> GetByUserIdAsync(int userId)
            => _mapper.Map<IEnumerable<UrlModel>>(
                await _unitOfWork.UrlRepository.GetByUserIdAsync(userId)
                );
    }
}
