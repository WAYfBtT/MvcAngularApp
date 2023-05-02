using DAL.DataContext;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UrlShortenerDataContext _context;
        private IDbContextTransaction _transaction;

        private IUserRepository _userRepository;
        private IUrlRepository _urlRepository;

        public UnitOfWork(UrlShortenerDataContext context)
            => _context = context;

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
        public IUrlRepository UrlRepository => _urlRepository ??= new UrlRepository(_context);

        public void BeginTransaction() =>
            _transaction = _context.Database.BeginTransaction();

        public void CommitTransaction()
        {
            if (_transaction == null) 
                throw new InvalidOperationException();
            _transaction?.Commit();
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
                throw new InvalidOperationException();
            _transaction?.Rollback();
        }

        public Task SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
