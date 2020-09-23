using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class GetStocks
    {
        private ApplicationDbContext Context;
        public GetStocks(ApplicationDbContext context)
        {
            Context = context;
        }

        public IEnumerable<StockViewModel> Do(int productId)
        {
            var stock = Context.Stock
                .Where(x => x.Id == productId)
                .Select(x => new StockViewModel 
                {
                    Id = x.Id,
                    Description = x.Description,
                    Quantity = x.Quantity
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
    }
}
