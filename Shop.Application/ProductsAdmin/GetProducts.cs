﻿using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private readonly ApplicationDbContext Context;
        public GetProducts(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            return Context.Products.ToList().Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            }) ;
        }
        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
        }
    }
    
}
