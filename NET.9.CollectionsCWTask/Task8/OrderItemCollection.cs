using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{
    public class OrderItemCollection<T> : Collection<T>
    {
        public event EventHandler<ModifyElementEventArgs<T>> OrderItemAdded = delegate { };

        public event EventHandler<ModifyElementEventArgs<T>> OrderItemRemoved = delegate { };

        public new void Add(T item)
        {
            base.Add(item);
            this.OnOrderItemAdded(this, new ModifyElementEventArgs<T>(item));
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            OnOrderItemRemoved(this, new ModifyElementEventArgs<T>(item));
        }

        protected virtual void OnOrderItemAdded(object sender, ModifyElementEventArgs<T> e)
        {
            OrderItemAdded?.Invoke(this, e);
        }

        protected virtual void OnOrderItemRemoved(object sender, ModifyElementEventArgs<T> e)
        {
            OrderItemRemoved?.Invoke(this, e);
        }
    }
}
