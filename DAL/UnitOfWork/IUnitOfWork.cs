using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IUrlRepository UrlRepository { get; }

        public void BeginTransaction();
        public void RollbackTransaction();
        public void CommitTransaction();
        public Task SaveChangesAsync();
    }
}
