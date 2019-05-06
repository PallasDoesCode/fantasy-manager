using System;
using System.Collections.Generic;
using System.Text;
using FantasyManager.Infrastructure.Context;

namespace FantasyManager.Infrastructure.Repository
{
    public interface IDbRepository : IRepository { }

    public class DbRepository : Repository, IDbRepository
    {
        #region Constructor

        public DbRepository(FantasyManagerContext dbContext)
            : base(dbContext) { }

        #endregion
    }
}
