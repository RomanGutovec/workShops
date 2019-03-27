using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using LoggingSample_BLL.Exceptions;
using LoggingSample_BLL.Helpers;
using LoggingSample_BLL.Models;
using LoggingSample_DAL.Context;

namespace LoggingSample_BLL.Services
{
    public class CustomerService : IDisposable
    {
        private readonly AppDbContext _context = new AppDbContext();

        public async Task<List<CustomerModel>> GetCustomers()
        {
            var customers = await _context.Customers.ToListAsync();
            
            return (await _context.Customers.ToListAsync()).Select(c=>c.Map()).ToList();
        }

        public Task<CustomerModel> GetCustomer(int customerId)
        {
            if (customerId == 56)
            {
                throw new CustomerServiceException("Wrong id has been requested",
                    CustomerServiceException.ErrorType.WrongCustomerId);
            }

            return _context.Customers.SingleOrDefaultAsync(item => item.Id == customerId).ContinueWith(task =>
            {
                var customer = task.Result;

                return customer?.Map();
            });
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