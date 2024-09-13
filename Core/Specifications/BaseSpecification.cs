using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Core.Interfaces;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        protected BaseSpecification():this(null) {}
        #nullable enable
        private readonly Expression<Func<T, bool>>? _criteria;
        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria => _criteria;

        public Expression<Func<T, object>>? OrderBy {get;private set;}

        public Expression<Func<T, object>>? OrderByDescending {get;private set;}

        public bool IsDistinct {get;private set;}

        public int Take {get;private set;}

        public int Skip {get;private set;}

        public bool IsPagingEnabled {get;private set;}

        protected void AddOrderBy(Expression<Func<T,object>> orderByExpression)
        {
            OrderBy= orderByExpression;
        }
        protected void AddOrderByDescending(Expression<Func<T,object>> orderByDescExpression)
        {
            OrderByDescending= orderByDescExpression;
        }
        protected void ApplyDistinct()
        {
            IsDistinct = true;
        }
        protected void ApplyPaging(int skip, int take)
        {
            Skip= skip;
            Take = take;
            IsPagingEnabled = true;
        }

        public IQueryable<T> ApplyCriteria(IQueryable<T> query)
        {
            if(Criteria != null)
            {
                query = query.Where(Criteria);
            }

            return query;
        }
#nullable disable
    }

    //added below class for projection to the Spec Part 1
    public class BaseSpecification<T, TResult>(Expression<Func<T,bool>> criteria)
       : BaseSpecification<T>(criteria), ISpecification<T, TResult>
    {
        //Below secondary constructor commented and added Primary constructor in class header
        // (Expression<Func<T,bool>> criteria) 
        // Pimary constructor only worked in C# langulage versin 12(.net framework 8 and above)
        // public BaseSpecification(Expression<Func<T,bool>> criteria)
        // {
        // }
        //Add without parameter contructor for projection to Spec Part 3
        protected BaseSpecification():this(null!) {}

        public Expression<Func<T, TResult>>? Select{get;private set;} 
        public void AddSelect(Expression<Func<T,TResult>> selectExpression)
        {
            Select = selectExpression;
        }
        
    }
}