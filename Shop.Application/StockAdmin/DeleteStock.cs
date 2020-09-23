using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private ApplicationDbContext Context;
        public DeleteStock(ApplicationDbContext context)
        {
            Context = context;
        }
        public async Task<bool> Do(int id)
        {
            var stock = Context.Stock.FirstOrDefault(x => x.Id == id);

            Context.Stock.Remove(stock);
            
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
