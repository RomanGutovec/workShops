using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task8
{

    public sealed class ModifyElementEventArgs<T> : EventArgs
    {
        #region properties
        public T Item { get; set; }
        #endregion

        #region ctor
        public ModifyElementEventArgs(T modifyingItem)
        {
            Item = modifyingItem;
        }
        #endregion
    }
}
