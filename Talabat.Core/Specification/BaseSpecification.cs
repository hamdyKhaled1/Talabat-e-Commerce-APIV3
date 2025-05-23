﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Includes { get ; set; }
        public Expression<Func<T, object>> OrderBy { get; set ; }
        public Expression<Func<T, object>> OrderByDesc { get ; set; }
        public int Skip { get; set ; }
        public int Take { get ; set; }
        public bool IsPaginationEnable { get ; set ; }

        public BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>();
        }
        public BaseSpecification(Expression<Func<T,bool>> criteriaexpression)
        {
            Criteria = criteriaexpression;
            Includes=new List<Expression<Func<T, object>>>();
        }

        public void ApplyPagination(int skip,int take)
        {
            IsPaginationEnable = true;
            Skip = skip;
            Take= take;
        }
    }
}
