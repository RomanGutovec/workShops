using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8_2
{
    class OrderItemReadOnlyCollection<T> : ReadOnlyCollection<T>
    {
        private OrderItemCollection<T> innerCollection;
        public OrderItemReadOnlyCollection(IList<T> list) : base(list)
        {
            innerCollection = new OrderItemCollection<T>();
            foreach (var item in list)
            {
                innerCollection.Add(item);
            }
        }        

        public ReadOnlyCollection<T> OrderItems
        {
            get
            {
                return new ReadOnlyCollection<T> (innerCollection);
            }
        }

        public event EventHandler<ModifyElementEventArgs<T>> OrderItemAdded = delegate { };

        protected virtual void OnOrderItemAdded(object sender, ModifyElementEventArgs<T> e)
        {
            OrderItemAdded?.Invoke(this, e);
        }

        public event EventHandler<ModifyElementEventArgs<T>> OrderItemRemoved = delegate { };

        protected virtual void OnOrderItemRemoved(object sender, ModifyElementEventArgs<T> e)
        {
            OrderItemRemoved?.Invoke(this, e);
        }
    }
}
