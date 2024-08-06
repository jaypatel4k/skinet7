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
        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products
                .Include(p=>p.ProductType)
                .Include(p=>p.ProductBrand)
                .ToListAsync();
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }
        public async Task<IReadOnlyList<ProductType>> GetProductTypeAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

    }
}