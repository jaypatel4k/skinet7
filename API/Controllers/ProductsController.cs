using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        
        #region ProductsController()
        /*
        private readonly StoreContext _context;
        
        public ProductsController(StoreContext context)
        {
            _context = context;
        }
        
        private readonly IProductRepository _repo;
        public ProductsController(IProductRepository repo)
        {
            _repo = repo;
        }
        */
        #endregion
        //private readonly IProductRepository _repo;
        private readonly IGenericRepository<Product> _repo;
        public ProductsController(IGenericRepository<Product> repo)
        {
            _repo = repo;
        }
        
        #region  GetProducts()
        // Below method code is synchronious 
        // [HttpGet]
        // public ActionResult<List<Product>> GetProducts()
        // {
        //     var products = _context.Products.ToList();
        //     return products;
        // }

       // Below method code is Asynchronious as above method code changed 
        
       // public async Task<ActionResult<List<Product>>> GetProducts()
       #endregion
       #nullable enable
       [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand,
                                                    string? type, string? sort)
        {
            //var products = await _context.Products.ToListAsync();
            //return products; // work with Task<ActionResult<List<Product>>>
            // Below two lines work with Task<ActionResult<IReadOnlyList<Product>>>
            // Replace return products with return OK(products) for convert error
            // var products = await _repo.GetProductsAsync();
            // return Ok(products); 

            // var products = await _repo.ListAllAsync();
            // return Ok(products); 

            //Below code added for specification Implement
            var spec = new ProductSpecification(brand,type,sort);
            var products = await _repo.ListAsync(spec);
            return Ok(products);
        }
        #nullable disable

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            //return await _context.Products.FindAsync(id);
            //return await _repo.GetPoductByIdAsync(id);
            var product =  _repo.GetByIdAsync(id);
            if(product == null) return NotFound();

            return await product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            _repo.Add(product);
            if(await _repo.SaveAllAsync())
            {
                return CreatedAtAction("GetProduct",new {id=product.Id},product);
            }

            return BadRequest("Problem creating product");
        }
        // [HttpGet("brands")]
        // public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        // {
        //      var brands = await _repo.GetProductBrandAsync();
        //      return Ok(brands);
        // }
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<String>>> GetBrands()
        {
             //TODO : Implement method
             //Implemented last 40. Add Projection to Spec Part 3
             var spec = new BrandListSpecification();
             return  Ok(await _repo.ListAsync(spec));
        }
        // [HttpGet("types")]
        // public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        // {
        //     var types = await _repo.GetProductTypeAsync();
        //     return Ok(types);
        // }
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<String>>> GetTypes()
        {
            //TODO : iplement Method
            //Implemented last 40. Add Projection to Spec Part 3
            var spec = new TypeListSpecification();
            return Ok(await _repo.ListAsync(spec));
        }
        
    }
}