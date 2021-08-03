using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Paybook.DatabaseLayer
{
    public class DbContextFactory
    {
        private DbContextFactory() { }
        private static readonly Lazy<IDbContext> _Instance = new Lazy<IDbContext>(() => new SQLDataAccess());
        public static IDbContext Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

    }
}
