using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbcontext;

        public GenericRepository(StoreContext storeContext)
        {
            _dbcontext = storeContext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbcontext.Set<T>().ToListAsync();
        public async Task<T> GetIdAsync(int id) => await _dbcontext.Set<T>().FindAsync(id);



        public async Task<IReadOnlyList<T>> GetAllBySpec(ISpecification<T> Spec)
        {
           return  await ApplySpec(Spec).ToListAsync();
        }

        public async Task<T> GetByIDSpec(ISpecification<T> Spec)
        {
            return await ApplySpec(Spec).FirstOrDefaultAsync();
        }
        private IQueryable<T>ApplySpec(ISpecification<T> Spec)
        {
            return   SpecificationEvalutor<T>.GetQuery(_dbcontext.Set<T>(), Spec);
        }

        public async Task<int> GetCountSpecAsync(ISpecification<T> Spec)
        {
            return await ApplySpec(Spec).CountAsync();
        }

        public async Task Add(T Item) => await _dbcontext.Set<T>().AddAsync(Item);
    }
}
