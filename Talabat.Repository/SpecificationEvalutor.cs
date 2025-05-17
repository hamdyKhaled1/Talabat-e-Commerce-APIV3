using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    public static class SpecificationEvalutor<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputquery,ISpecification<T> spec)
        {
            var query = inputquery;
            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDesc is not null)
            {
                query=query.OrderByDescending(spec.OrderByDesc);
            }
            if (spec.IsPaginationEnable)
            {
                query=query.Skip(spec.Skip).Take(spec.Take);
            }
            query = spec.Includes.Aggregate(query, (currentquery, IncludeExpression) => currentquery.Include(IncludeExpression));
            return query;
        }
    }
}
