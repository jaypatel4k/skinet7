using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        // private readonly StoreContext _context;
        
        // public ProductsController(StoreContext context)
        // {
        //     _context = context;
        // }
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }
        // Below method code is synchronious 
        // [HttpGet]
        // public ActionResult<List<Product>> GetProducts()
        // {
        //     var products = _context.Products.ToList();
        //     return products;
        // }

       // Below method code is Asynchronious as above method code changed 
        
       // public async Task<ActionResult<List<Product>>> GetProducts()
       [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            //var products = await _context.Products.ToListAsync();
            var products = await _repo.GetProductsAsync();
            //return products; // work with Task<ActionResult<List<Product>>>
            return Ok(products); // work with Task<ActionResult<IReadOnlyList<Product>>>
            // Replace return products with return OK(products) for convert error

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //return await _context.Products.FindAsync(id);
            return await _repo.GetPoductByIdAsync(id);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _repo.GetProductBrandAsync();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _repo.GetProductTypeAsync();
            return Ok(types);
        }
        
    }
}