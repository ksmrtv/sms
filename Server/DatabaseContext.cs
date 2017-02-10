using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;

namespace Server
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :
            //base(@"Data Source=TM\SQLSERVER;Initial Catalog=Sms;
            //        Integrated Security=True;MultipleActiveResultSets=True;")
            base(ConfigurationManager.ConnectionStrings["SmsDbContext"].ConnectionString)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Balance> Balances { get; set;}
    }
}
