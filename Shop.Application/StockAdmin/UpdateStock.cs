using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class UpdateStock
    {
        private ApplicationDbContext Context;
        public UpdateStock(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<Response> Do(Request request)
        {
            var stocks = new List<Stock>();
            foreach (var stock in request.Stock)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    Quantity = stock.Quantity,
                    ProductId = stock.ProductId
                }); 
            }


            Context.Stock.UpdateRange(stocks); 
            await Context.SaveChangesAsync();

            return new Response
            {
                Stock = request.Stock
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }
        public class Request
        {
            public IEnumerable<Stock> Stock { get; set; }
        }

        public class Response
        {
            public IEnumerable<Stock> Stock { get; set; }
        }

    }
}
