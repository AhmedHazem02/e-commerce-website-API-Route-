using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _db_Context;
        private Hashtable hashtable;
        public UnitOfWork(StoreContext db_context)
        {
            _db_Context = db_context;
            hashtable = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _db_Context.SaveChangesAsync();

        public async ValueTask DisposeAsync()=>await _db_Context.DisposeAsync();

        public  IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type= typeof(TEntity).Name;
           
            if (!hashtable.ContainsKey(type))
            {
                var Repo = new GenericRepository<TEntity>(_db_Context);
                 hashtable.Add(type, Repo);
            }
            return (IGenericRepository<TEntity>)hashtable[type];
        }
    }
}
