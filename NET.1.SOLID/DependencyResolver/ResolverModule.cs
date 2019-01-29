using System.Data.Common;
using System.Data.SqlClient;
using Ninject.Modules;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbConnection>().To<SqlConnection>();
        }
    }
}
