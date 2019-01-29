using System;
using System.Collections.Generic;

namespace SolidWorkShop_Refactored.Interfaces
{
    /// <summary>
    /// Represents type to add data
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Accessor to identifier of the database.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Accessor to identifier of the database.
        /// </summary>
        DateTime CreatedDate { get; set; }
    }
}
