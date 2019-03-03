using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    class Program
    {

        static void Main(string[] args)
        {
            OrderItemCollection<OrderItem> collection = new OrderItemCollection<OrderItem>()
            {
            new OrderItem(110072674, "Widget", 400, 45.17),
            new OrderItem(110072675, "Sprocket", 27, 5.3),
            new OrderItem(101030411, "Motor", 10, 237.5),
            new OrderItem(110072684, "Gear", 175, 5.17)
            };

            collection.OrderItemAdded += PrintWhenAdded;
            collection.OrderItemRemoved += PrintWhenRmoved;

            collection.Add(new OrderItem(110072674, "Widget", 400, 45.17));
            collection.Remove(new OrderItem(110072674, "Widget", 400, 45.17));

            Console.ReadLine();

        }

        private static void PrintWhenAdded<OrderItem>(object sender, ModifyElementEventArgs<OrderItem> e)
        {
            Console.WriteLine($"Element {e.Item} was added to collection");
        }

        private static void PrintWhenRmoved<OrderItem>(object sender,ModifyElementEventArgs<OrderItem> e)
        {
            Console.WriteLine($"Element {e.Item} was deleted");
        }
    }
}
