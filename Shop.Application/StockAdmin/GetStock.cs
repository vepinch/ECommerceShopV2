﻿using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class GetStock
    {
        private ApplicationDbContext Context;
        public GetStock(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<ProductViewModel> Do()
        {
            var stock = Context.Products
                .Include(x => x.Stock) 
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Stock = x.Stock.Select(y => new StockViewModel 
                    {
                        Id = y.Id,
                        Description = y.Description,
                        Quantity = y.Quantity
                    })
                })
                .ToList();

            return stock;
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }

        public class ProductViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public IEnumerable<StockViewModel> Stock { get; set; }
        }
    }
}
