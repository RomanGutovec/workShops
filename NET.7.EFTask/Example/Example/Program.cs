using System;
namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SomeService();
            service.DoSmth();
            service.PrintCustomersWithOrders();

            Console.WriteLine("Main Completed");
            
            Console.ReadLine();
        }
    }
}
