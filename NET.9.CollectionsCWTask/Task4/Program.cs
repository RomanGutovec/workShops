﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

public class OrderItem
{
    public readonly int PartNumber;
    public readonly string Description;
    public readonly double UnitPrice;
    private int _quantity = 0;

    public OrderItem(int partNumber, string description, int quantity, double unitPrice)
    {
        this.PartNumber = partNumber;
        this.Description = description;
        this.Quantity = quantity;
        this.UnitPrice = unitPrice;
    }

    public int Quantity
    {
        get
        {
            return _quantity;
        }

        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Quantity cannot be negative.");
            }

            _quantity = value;
        }
    }

    public override string ToString()
    {
        return String.Format("{0,9} {1,6} {2,-12} at {3,8:#,###.00} = {4,10:###,###.00}",
            PartNumber, _quantity, Description, UnitPrice, UnitPrice * _quantity);
    }
}

public class Order : KeyedCollection<int, OrderItem>
{
    private readonly List<OrderItem> _orderItems = new List<OrderItem>();

    public Order(IEnumerable<OrderItem> items)
    {
        if (items == null)
        {
            throw new ArgumentNullException("Consequence has null value");
        }

        this._orderItems = items.ToList();
    }

    public IList<OrderItem> OrderItems
    {
        get
        {
            return new ReadOnlyCollection<OrderItem>(_orderItems);
        }
    }

    protected override int GetKeyForItem(OrderItem item)
    {
        return item.PartNumber;
    }

    public OrderItem Search(int partNumber)
    {
        for (int i = 0; i < OrderItems.Count; i++)
        {
            if (OrderItems[i].PartNumber == partNumber)
            {
                return OrderItems[i];
            }
        }

        return null;
    }
}

public class Program
{
    public static void Main()
    {
        var ordersToInitializeFirstOrder = new List<OrderItem>()
        {
            new OrderItem(110072674, "Widget", 400, 45.17),
            new OrderItem(110072675, "Sprocket", 27, 5.3),
            new OrderItem(101030411, "Motor", 10, 237.5),
            new OrderItem(110072684, "Gear", 175, 5.17),
        };

        var firstOrder = new Order(ordersToInitializeFirstOrder);

        Display("Order #1", firstOrder);

        FindByPartNumber("Order #1", firstOrder, 111033401);


        var ordersToInitializeSecondOrder = new List<OrderItem>()
        {
            new OrderItem(110072674, "Widget", 400, 45.17),
            new OrderItem(110072675, "Sprocket", 27, 5.3),
            new OrderItem(101030411, "Motor", 10, 237.5),
            new OrderItem(110072684, "Gear", 175, 5.17),
            new OrderItem(111033401, "Nut", 10, .5),
            new OrderItem(127700026, "Crank", 27, 5.98)
        };

        var secondOrder = new Order(ordersToInitializeSecondOrder);

        Display("Order #2", secondOrder);

        FindByPartNumber("Order #2", secondOrder, 127700026);
    }

    private static void Display(string title, Order order)
    {
        Console.WriteLine(title);
        foreach (OrderItem item in order.OrderItems)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    private static void FindByPartNumber(string title, Order order, int partNumber)
    {
        var result = order.Search(partNumber);
        if (result == null)
        {
            Console.WriteLine(String.Format("{0} doesn't have #{1} item.", title, partNumber));
        }
        else
        {
            Console.WriteLine(String.Format("{0} has #{1} item.", title, partNumber));
            Console.WriteLine("Order #2 has #127700026 item - price is {0}$.", result.Quantity * result.UnitPrice);
        }
    }
}