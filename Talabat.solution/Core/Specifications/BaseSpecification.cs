using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Include { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPangEnable { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>>CriteriaExpertion)
        {
            Criteria = CriteriaExpertion;
        }
        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy= orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc=orderByDesc;
        }

        public void ApplyPangination(int skip,int take)
        {
            IsPangEnable=true;
            Skip=skip;
            Take=take;
        }

    }
}
