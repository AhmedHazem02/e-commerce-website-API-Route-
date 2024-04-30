﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Include {  get; set; }

        // Sort Product OrderBy
        public Expression<Func<T, object>>OrderBy { get; set; }

        // Sort Product OrderByDesc
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip {  get; set; }

        public int Take { get; set; }
        public bool IsPangEnable {  get; set; }
    }
}
