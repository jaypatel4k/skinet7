using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize =50;
        public int PageIndex{get;set;} = 1 ;
        private int _pageSize= 5;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize ) ? MaxPageSize : value;
        }
        
        private List<string> _brands = [];
        public List<string> Brands
        {
            get=> _brands;
            set
            {
                _brands = value.SelectMany(x=>x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }
        private List<string> _types = [];
        public List<string> Types
        {
            get=> _types;
            set
            {
                _types = value.SelectMany(x=>x.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
            }
        }

        #nullable enable
        public string? Sort { get; set; }

        private string? _search;
        public string Search
        {
            get => _search ?? "";
            set => _search = value.ToLower();
        }
        
        #nullable disable
    }
}