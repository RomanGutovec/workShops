using LoggingSample_BLL.Models;
using LoggingSample_DAL.Context;
using LoggingSample_BLL.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingSample_BLL.Exceptions;

namespace LoggingSample_BLL.Services
{
    public class OrderService : IDisposable
    {
        private readonly AppDbContext _context = new AppDbContext();

        public async Task<List<OrderModel>> GetOrdersAsync(int customerId)
        {
            return (await _context.Orders.Where(item => item.CustomerId == customerId).ToListAsync()).Select(item => item.Map()).ToList();
        }

        public async Task<OrderModel> GetOrderAsync(int customerId, int orderId)
        {
            if (orderId == 30)
            {
                throw new OrderServiceException("Wrong id has been requested",
                    OrderServiceException.ErrorType.WrongOrderId);
            }

            var order = (await _context.Orders.SingleOrDefaultAsync(item => item.Id == orderId && item.CustomerId == customerId));
            return order?.Map();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _context.Dispose();
        //    }

        //    base.Dispose(disposing);
        //}
    }
}

