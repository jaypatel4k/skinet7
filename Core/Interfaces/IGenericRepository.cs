using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();

        //Below Two methods added for SpecificationEvaluaton
        #nullable enable
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        #nullable disable
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        //Now add below two methods to adding projection for Spec part 2
        //For return type TResult
        #nullable enable
        Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T,TResult> spec);
        #nullable disable
        Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T,TResult> spec);
        void Add(T Entity);
        void Remove(T Entity);
        void Update(T Entity);
        Task<bool> SaveAllAsync();
        bool Exists(int id);
        Task<int> CountAsync(ISpecification<T> spec);
        
    }
}