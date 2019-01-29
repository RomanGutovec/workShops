using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidWorkShop_Refactored.Interfaces
{
    /// <summary>
    /// Represents unit of work with a database
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Save data to the database
        /// </summary>
        void Save();
    }
}
