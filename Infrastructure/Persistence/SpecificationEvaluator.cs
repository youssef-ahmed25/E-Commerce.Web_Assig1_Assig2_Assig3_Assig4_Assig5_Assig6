using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,Tkey>(IQueryable<TEntity>InputQuary,ISpecification<TEntity,Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            var query = InputQuary;
            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }

            if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
            if (specification.IsPaginated)
            {
                query = query.Skip(specification.Skip);
                query = query.Take(specification.Take);
            }

            if (specification.IncludeExperssions is not null&& specification.IncludeExperssions.Any())
            {
                #region ده طريقه
                //foreach (var expression in specification.IncludeExperssions)
                //{
                //    query = query.Include(expression);
                //} 
                #endregion
                query = specification.IncludeExperssions.Aggregate(query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }
            return query;
        }
    }
}
