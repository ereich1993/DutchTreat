using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private DutchContext _ctx;

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
        public Order GetOrderById(int id)
        {
            return _ctx.Orders.Include(o => o.Items).
                ThenInclude(p => p.Product).
                Where(i=>i.Id==id).FirstOrDefault();
        }

        /// <summary>
        /// very important when you want to return a complex entity
        /// you have to use the Inclue pluss the lambda what to include
        /// unless that the items of the orders would be null
        /// then you have to turn of self referencing loop se at 
        /// add mvc part extension:
        /// services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).
        /// AddJsonOptions(opt=>opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders.Include(o => o.Items).
                ThenInclude(p => p.Product).ToList();
            }
            else
            {
              return  _ctx.Orders.ToList();
            }
                
            
            
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products.OrderBy(p=>p.Title).ToList();
        }

        

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
          return  _ctx.Products.
                Where(p => p.Category == category).
                ToList();
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        

        public DutchRepository(DutchContext ctx)
        {
            _ctx = ctx;
        }
    }
}
