using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WPF_SQL_Injection
{
    //Connection to the database
    class UserContext : DbContext
    {

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
                    => options.UseSqlite(@"Data Source=C:\SQLite\test.db");
    }


    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string lastname { get; set; }
        public string accesstype { get; set; }
    }
}
