using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<OrderItem> orderItemsToInitialize = new List<OrderItem>()
            {
            new OrderItem(110072674, "Widget", 400, 45.17),
            new OrderItem(110072675, "Sprocket", 27, 5.3),
            new OrderItem(101030411, "Motor", 10, 237.5),
            new OrderItem(110072684, "Gear", 175, 5.17)
            };

            OrderItemReadOnlyCollection<OrderItem> collection = new OrderItemReadOnlyCollection<OrderItem>(orderItemsToInitialize);

            collection.OrderItemAdded += PrintWhenAdded;
            collection.OrderItemRemoved += PrintWhenRmoved;

            foreach (var item in collection)
            {
                Console.WriteLine($"Item: part number:{item.PartNumber}, description:{item.Description}, price: {item.UnitPrice}");
            }

            Console.ReadLine();

        }

        private static void PrintWhenAdded<OrderItem>(object sender, ModifyElementEventArgs<OrderItem> e)
        {
            Console.WriteLine($"Element {e.Item} was added to collection");
        }

        private static void PrintWhenRmoved<OrderItem>(object sender, ModifyElementEventArgs<OrderItem> e)
        {
            Console.WriteLine($"Element {e.Item} was deleted");
        }
    }
}
