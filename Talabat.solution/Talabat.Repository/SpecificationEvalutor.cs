using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<T>where T : BaseEntity
    {
        public static IQueryable<T>GetQuery(IQueryable<T> query,ISpecification<T> Spec)
        {
            var Query=query;
            if(Spec.Criteria is not null) Query=Query.Where(Spec.Criteria);
            if(Spec.OrderBy is not null) Query=Query.OrderBy(Spec.OrderBy);
            if(Spec.OrderByDesc is not null) Query=Query.OrderByDescending(Spec.OrderByDesc);
            if (Spec.IsPangEnable) Query=Query.Skip(Spec.Skip).Take(Spec.Take);
            

            Query = Spec.Include.Aggregate(Query, (Current, Expreation) => Current.Include(Expreation));
            return Query;
        }
    }
}
