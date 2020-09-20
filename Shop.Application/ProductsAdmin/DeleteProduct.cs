using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Database;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDbContext Context;
        public DeleteProduct(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<bool> Do(int id)
        {
            var Product = Context.Products.FirstOrDefault(x => x.Id == id);
            Context.Products.Remove(Product); 
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
