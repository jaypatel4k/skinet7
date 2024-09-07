using Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRespository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRespository(StoreContext context)
        {
            _context = context;
        }

        public  async Task<Product> GetPoductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .FirstOrDefaultAsync(p=> p.Id == id);
        }

        

        // public async Task<IReadOnlyList<Product>> GetProductsAsync()
        // {
        //     return await _context.Products.ToListAsync();
        // }
        // Task<IReadOnlyList<Product>> not working so taken Task<List<Product>>
       // public async Task<List<Product>> GetProductsAsync()
        // public async Task<IReadOnlyList<Product>> GetProductsAsync()
        // {
        //     return await _context.Products
        //         .Include(p=>p.ProductType)
        //         .Include(p=>p.ProductBrand)
        //         .ToListAsync();
        // }
        #nullable enable
        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            
            // return await _context.Products
            //     .Include(p=>p.ProductType)
            //     .Include(p=>p.ProductBrand)
            //     .ToListAsync();
            var query = _context.Products.AsQueryable();
            if(!string.IsNullOrWhiteSpace(brand))
                query = query.Where(x=>x.ProductBrand.Name == brand);
            if(!string.IsNullOrWhiteSpace(type))
                query = query.Where(x=>x.ProductType.Name == type);
            
            query = sort switch
            {
                "priceAsc" => query.OrderBy(x=> x.Price),
                "priceDesc" => query.OrderByDescending(x=>x.Price),
                _ => query.OrderBy(x=> x.Name)
            };
            return await query.ToListAsync();
        }
        #nullable disable
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return (IReadOnlyList<string>)await _context.Products.Select(x=>x.ProductBrand)
                    .Distinct().ToListAsync();
        }
        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return (IReadOnlyList<string>)await _context.Products.Select(x=>x.ProductType)
                .Distinct()
                .ToListAsync();
        }

    }
}