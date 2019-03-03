using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
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
    }
}
