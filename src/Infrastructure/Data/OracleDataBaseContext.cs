using Infrastructure.Data.Models.Alerts;
using Involys.QueryBuilder.Api.Infrastructure.Data.Models.QueryBuilder;
using Involys.Repository.Api.Infrastructure.Data.Models.Alerts;
using Microsoft.EntityFrameworkCore;

namespace Involys.Poc.Api.Data
{

    public class OracleDataBaseContext : DatabaseContext
    {
        public OracleDataBaseContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // max length for Oracle 11g.
            modelBuilder.Model.SetMaxIdentifierLength(30);
        }
    }


}



