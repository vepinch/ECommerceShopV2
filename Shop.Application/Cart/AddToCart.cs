

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession Session;


        public AddToCart(ISession session)
        {
            Session = session;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Quantity { get; set; }
        }
        public void Do(Request request)
        {
            var cartList = new List<CartProduct>();

            var stringObject = Session.GetString("cart");
            if (!string.IsNullOrEmpty(stringObject))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(stringObject); 
            }

            if (cartList.Any(x => x.StockId == request.StockId))
            {
                cartList.Find(x => x.StockId == request.StockId).Quantity += request.Quantity;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Quantity = request.Quantity
                });
            }

            stringObject = JsonConvert.SerializeObject(cartList);



            Session.SetString("cart", stringObject);
        }
    }
}
