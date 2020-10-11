using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FbStats.Data.Repositories;

namespace FbStats.Client.Factories
{
    public static class RepositoryFactory
    {

        public static IRepository NewRepository()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["statsdb"].ConnectionString;
            return new SqlRepository(connectionString);
        }

    }
}
