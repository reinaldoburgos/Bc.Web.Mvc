using System.Collections.Generic;
using System.Data.Entity;

namespace Bc.Web.Mvc.Data
{
    public class DeceContext : DbContext
    {

        public DeceContext() : base("name=DefaultConnection")
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer<DeceContext>(null);
        }


        public int ExecuteSqlCommand(string sql, List<Helper.Parameter> parameters = null)
        {
            sql = Helper.Parameter.ApplyParameters(sql, parameters);
            return base.Database.ExecuteSqlCommand(sql);
        }

        List<string> pendingCommands = null;
        public List<string> PendingCommands
        {
            get
            {
                if (pendingCommands == null)
                    pendingCommands = new List<string>();
                return pendingCommands;
            }
        }

        public override int SaveChanges()
        {
            int result = 0;
            foreach (string sql in PendingCommands)
                result = ExecuteSqlCommand(sql);

            int resultDb = base.SaveChanges();
            return result + resultDb;

            // return base.SaveChanges();
        }
    }
}