using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories
{
    public interface IGenericRepository <T> where T : BaseEntity 
    {
       Task<IReadOnlyList<T>> GetAllAsync ();
        Task<T> GetIdAsync (int id);

        public Task<IReadOnlyList<T>> GetAllBySpec(ISpecification<T> Spec);
        public Task<T> GetByIDSpec(ISpecification<T> Spec);

        public Task<int>GetCountSpecAsync(ISpecification<T> Spec);
    }
}
