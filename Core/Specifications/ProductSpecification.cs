using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        #nullable enable
        // public ProductSpecification(string? brand, string? type, string? sort): base(x => 
        //     (string.IsNullOrWhiteSpace(brand) || x.ProductBrand.Name == brand) && 
        //     (string.IsNullOrWhiteSpace(type) || x.ProductType.Name == type)
        // )
        // public ProductSpecification(ProductSpecParams specParams): base(x => 
        //     (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.ProductBrand.Name)) && 
        //     (specParams.Types.Count == 0 || specParams.Brands.Contains(x.ProductBrand.Name))
        // )
        public ProductSpecification(ProductSpecParams specParams): base(x => 
            (!string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (specParams.Brands.Count == 0 || specParams.Brands.Contains(x.ProductBrand.Name)) && 
            (specParams.Types.Count == 0 || specParams.Brands.Contains(x.ProductBrand.Name))
        )
        {
            ApplyPaging(specParams.PageSize * specParams.PageIndex-1, specParams.PageSize);
            switch(specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x=>x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x=>x.Price);
                    break;
                default:
                    AddOrderBy(x=>x.Name);
                    break;
            }
        }
        #nullable disable
    }
}