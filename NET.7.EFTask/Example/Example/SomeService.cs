using System;
using System.Linq;
using DAL.Context;
using System.Data.Entity;

namespace Example
{
    public class SomeService
    {
        public void DoSmth()
        {
            using (var context = new AppDbContext())
            {
                var items = context.Items.ToList();

                Console.WriteLine($"Items number: {items.Count}");

                foreach (var item in items)
                {
                    Console.WriteLine($"{item.Id} - {item.Description} - {item.Price}");
                }
            }
        }

        public void PrintCustomersWithOrders()
        {
            using (var context = new AppDbContext())
            {
                var customers = context.Customers.Include(x=>x.Orders).Include(x=>x.Orders.Select(y=>y.OrderItems)).ToList();
                var items = context.Items.ToList();
                foreach (var item in customers)
                {
                    Console.WriteLine("Customer:");
                    Console.WriteLine($"{item.Id} " +
                        $"- {item.Name} - " +
                        $"{item.Address} - " +
                        $"{item.City} - " +
                        $"{item.State}");
                    Console.WriteLine("Orders:");
                    foreach (var order in item.Orders)
                    {
                        Console.WriteLine($"{order.Id} - {order.Date}");
                        foreach (var orderItem in order.OrderItems)
                        {
                            Console.WriteLine($"{orderItem.Id} - " +
                                $"{orderItem.Quantity} - " +
                                $"{items.FirstOrDefault(i=>i.Id==orderItem.ItemId).Price} " +
                                $"- {items.FirstOrDefault(i => i.Id == orderItem.ItemId).Description} ");
                        }
                    }
                }
            }
        }
    }
}