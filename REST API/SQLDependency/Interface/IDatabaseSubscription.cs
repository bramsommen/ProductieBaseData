using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace REST_API.SQLDependency.Interface
{
    public interface IDatabaseSubscription
    {
        void Configure(string connectionString);
    }
}
