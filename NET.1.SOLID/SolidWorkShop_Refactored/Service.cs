using System;
using System.Collections.Generic;
using System.Data.Common;
using SolidWorkShop_Refactored.Interfaces;

namespace SolidWorkShop_Refactored
{
    public class Service
    {
        private readonly DbConnection _connection;
        private readonly IDataContext _dataContext;

        /// <summary>
        /// Create instance of the <see cref="Service"/> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when database connection has null value</exception>
        /// <exception cref="ArgumentNullException">Thrown when unit of work with database has null value</exception>
        /// <param name="connection">Provides connection to a database.</param>
        /// <param name="dataContext">Represents working with a databse.</param>
        public Service(DbConnection connection, IDataContext dataContext)
        {
            _connection = connection ?? throw new ArgumentNullException($"Connection {nameof(connection)} has incorrect value");
            _dataContext = dataContext ?? throw new ArgumentNullException($"Data context {nameof(dataContext)} has incorrect value");
        }

        /// <summary>
        /// Saves data into the particular databse
        /// </summary>
        /// <param name="entity">Entity to save to the database.</param>
        /// <returns>Entity added to the database.</returns>
        public IEntity Save(IEntity entity)
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        _connection.Open();

                        _dataContext.Save();

                        _connection.Close();
                        return entity;
                    }
                    catch
                    {
                        if (i == 2)
                        {
                            throw;
                        }
                    }
                }

                throw new Exception("Ex");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Represents an opportunity to read all elements from the database.
        /// </summary>
        /// <returns>Iterated collection.</returns>
        public IEnumerable<IEntity> ReadAll()
        {
            _connection.Open();

            _dataContext.Save();

            _connection.Close();
            return new List<IEntity>();
        }
    }
}
