using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        private ISession Session;
        private ApplicationDbContext Context;

        public GetCart(ISession session, ApplicationDbContext context)
        {
            Session = session;
            Context = context;
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }

        public IEnumerable<Response> Do()
        {
            var stringObject = Session.GetString("cart");
            if (string.IsNullOrEmpty(stringObject))
            {
                return new List<Response>();
            }
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject);

            var response = Context.Stock
                .Include(x => x.Product).AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId == x.Id))
                .Select(x => new Response
                {
                    Name = x.Product.Name,
                    Value = $"{x.Product.Value.ToString("N2")} $",
                    StockId = x.Id,
                    Quantity = cartList.FirstOrDefault(y => y.StockId == x.Id).Quantity
                })
                .ToList();


            
            return response;
        }
    }
}
