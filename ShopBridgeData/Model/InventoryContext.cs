using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ShopBridgeData.Model
{
    /// <summary>
    ///  DbContext class for Entity framework code first approach
    /// </summary>
    public class InventoryContext : DbContext
    {
        /// <summary>
        ///  Pass connection string
        /// </summary>
        public InventoryContext() : base("data source=localDb;initial catalog=InventoryDB;persist security info=True;user id=devuser;password=*****")
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<InventoryContext, Migrations.Configuration>());
        }

        /// <summary>
        /// Property to store Invertories
        /// </summary>
        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<InventoryType> InventoryTypes { get; set; }
        
    }
}
