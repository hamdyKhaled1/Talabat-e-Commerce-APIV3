using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private Hashtable _repositories;
        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
            _repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
           var type=typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository=new GenericRepository<TEntity>(_dbContext);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as GenericRepository<TEntity>;
        }
        public async Task<int> Complete()
        =>await _dbContext.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        =>await _dbContext.DisposeAsync();

        
    }
}
