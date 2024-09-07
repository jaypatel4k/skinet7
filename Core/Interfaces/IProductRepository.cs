using Core.Entities;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetPoductByIdAsync(int id);
        //Task<IReadOnlyList<Product>> GetProductsAsync();
        //Task<List<Product>> GetProductsAsync();
        #nullable enable
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
        #nullable disable
        Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypeAsync();
    }
}