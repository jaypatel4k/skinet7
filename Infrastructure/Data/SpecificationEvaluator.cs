using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
        {
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); 
                //x=>x.Brand.Name == brand
                // here spec.Criteria is specified Criteria like used in ProductRespository class
                // inside where cluase like below
                // x=>x.Brand.Name == brand

                // add Below changes for orderby changes
                if(spec.OrderBy !=null)
                {
                    query = query.OrderBy(spec.OrderBy);
                }
                if(spec.OrderByDescending !=null)
                {
                    query = query.OrderByDescending(spec.OrderByDescending);
                }
                if(spec.IsDistinct)
                {
                    query = query.Distinct();
                }
            }
            return query;
        }
        // add below method for projection to add Spec Part 1
        public static IQueryable<TResult> GetQuery<TSpec,TResult>(IQueryable<T> query, 
            ISpecification<T, TResult> spec)
        {
            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria); 
                //x=>x.Brand.Name == brand
                // here spec.Criteria is specified Criteria like used in ProductRespository class
                // inside where cluase like below
                // x=>x.Brand.Name == brand
            }
                // add Below changes for orderby changes
            if(spec.OrderBy !=null)
            {
                query = query.OrderBy(spec.OrderBy);
            }
            if(spec.OrderByDescending !=null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }
            var selectQuery = query as IQueryable<TResult>;
            if(spec.Select !=null)
            {
                selectQuery = query.Select(spec.Select);
            }
            if(spec.IsDistinct)
            {
                selectQuery = selectQuery?.Distinct();
            }
            return selectQuery ?? query.Cast<TResult>();
            }
    }
}