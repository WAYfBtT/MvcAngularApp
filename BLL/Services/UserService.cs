using AutoMapper;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> AddAsync(UserModel model)
        {
            var entity = _mapper.Map<User>(model);
            if (await _unitOfWork.UserRepository.AnyAsync(x => x.UsernameNormalized == entity.UsernameNormalized))
                throw new UrlShortenerApplicationException("User with such username already exist.");
            _unitOfWork.UserRepository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (await _unitOfWork.UserRepository.AnyAsync(x => x.Id == id))
                throw new UrlShortenerApplicationException("User with such id does not exist.");

            await _unitOfWork.UserRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserModel model)
        {
            var entity = _mapper.Map<User>(model);
            if (await _unitOfWork.UserRepository.AnyAsync(x => x.Id == model.Id))
                throw new UrlShortenerApplicationException("User with such id does not exist.");

            _unitOfWork.UserRepository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserModel>> GetAllAsync()
            => _mapper.Map<IEnumerable<UserModel>>(
                await _unitOfWork.UserRepository.GetAllAsync());

        public async Task<IEnumerable<UserModel>> GetAllAsync(int skip, int take)
            => _mapper.Map<IEnumerable<UserModel>>(
                await _unitOfWork.UserRepository.GetAllAsync(skip, take));

        public async Task<UserModel> GetByIdAsync(int id)
            => _mapper.Map<UserModel>(
                await _unitOfWork.UserRepository.GetByIdAsync(id));

        public async Task<UserModel> GetByIdWithDetailsAsync(int id)
            => _mapper.Map<UserModel>(
                await _unitOfWork.UserRepository.GetByIdWithDetailsAsync(id));

        public async Task<UserModel> GetByUserNameAsync(string userName)
            => _mapper.Map<UserModel>(
                await _unitOfWork.UserRepository.GetByUserNameAsync(userName.Normalize()));

        public async Task<UserModel> UpdateAsync(UserModel model)
        {
            var entity = _mapper.Map<User>(model);
            _unitOfWork.UserRepository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserModel>(entity);
        }
    }
}
