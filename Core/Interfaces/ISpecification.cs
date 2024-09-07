using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Core.Interfaces
{
    public interface ISpecification<T>
    {
        #nullable enable
        Expression<Func<T,bool>>? Criteria{get;}
        #nullable disable
        Expression<Func<T,Object>> OrderBy{get;}
        Expression<Func<T,Object>> OrderByDescending{get;}
        bool IsDistinct{get;}
    }
    public interface ISpecification<T,Tresult>: ISpecification<T>
    {
        Expression<Func<T,Tresult>> Select{get;}
    }
}