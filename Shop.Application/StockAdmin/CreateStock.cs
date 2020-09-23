using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private ApplicationDbContext Context;
        public CreateStock(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task<Response> Do(Request request)
        {
            var stock = new Stock
            {
                Description = request.Description,
                Quantity = request.Quantity,
                ProductId = request.ProductId
            };

            Context.Stock.Add(stock);
            await Context.SaveChangesAsync();

            return new Response
            {
                Id = stock.Id,
                Description = stock.Description,
                Quantity = stock.Quantity
            };
        }

        public class Request
        {
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
          
        }

        public class Response
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
        }

    }
}
