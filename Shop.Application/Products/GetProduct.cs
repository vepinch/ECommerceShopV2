﻿using Shop.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private readonly ApplicationDbContext Context;
        public GetProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public ProductViewModel Do(string name) =>
            Context.Products
            .Include(x => x.Stock)
            .Where(x => x.Name == name)
            .Select(x => new ProductViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Value = $"{x.Value.ToString("N2")} $",
                
                Stock = x.Stock.Select(y => new StockViewModel
                {
                    Id = y.Id,
                    Description = y.Description,
                    InStock = y.Quantity > 0
                })
            })
            .FirstOrDefault();

        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }

            public IEnumerable<StockViewModel> Stock { get; set; }

        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool InStock { get; set; }
        }
    }
}
