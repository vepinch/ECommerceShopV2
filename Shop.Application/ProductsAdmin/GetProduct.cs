using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class GetProduct
    {
        private readonly ApplicationDbContext Context;
        public GetProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public ProductViewModel Do(int id)
        {
            return Context.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Value = x.Value
            })
                .FirstOrDefault();
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
