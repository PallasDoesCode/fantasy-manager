using System;
using System.Collections.Generic;
using System.Text;

namespace FantasyManager.Infrastructure.Context
{
    public interface IDbContextSettings
    {
        /// <summary>
        /// connection string used to connect to database instance 
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// schema name associated with any queries or operations performed for connection 
        /// </summary>
        string SchemaName { get; }
    }
}
