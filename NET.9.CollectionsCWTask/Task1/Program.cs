﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

public class Order
{
    private OrderItem[] _orderItems;

    public OrderItem[] OrderItems
    {
        get
        {
            return _orderItems;
        }

        set
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this._orderItems = value;
        }
    }

    public Order(OrderItem[] orderItems)
    {
        this.OrderItems = orderItems;
    }

    public OrderItem Search(int partNumber)
    {
        for (int i = 0; i < _orderItems.Length; i++)
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
        var orderItems = new OrderItem[]
        {
            new OrderItem(110072674, "Widget", 400, 45.17),
            new OrderItem(110072675, "Sprocket", 27, 5.3),
            new OrderItem(101030411, "Motor", 10, 237.5),
            new OrderItem(110072684, "Gear", 175, 5.17)
        };

        var order = new Order(orderItems);
        Display("Order #1", order);

        FindByPartNumber("Order #1", order, 111033401);

        var newOrderItems = new OrderItem[orderItems.Length + 2];
        Array.Copy(orderItems, 0, newOrderItems, 0, orderItems.Length);

        newOrderItems[orderItems.Length] = new OrderItem(111033401, "Nut", 10, .5);
        newOrderItems[orderItems.Length + 1] = new OrderItem(127700026, "Crank", 27, 5.98);

        order.OrderItems = newOrderItems;
        Display("Order #2", order);

        FindByPartNumber("Order #2", order, 127700026);
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
        //var result = Array.Find(order.OrderItems, (i) => i.PartNumber == partNumber);
        var result = order.Search(partNumber);
        if (result == null)
        {
            Console.WriteLine(string.Format("{0} doesn't have #{1} item.", title, partNumber));
        }
        else
        {
            Console.WriteLine(string.Format("{0} has #{1} item.", title, partNumber));
        }
    }
}