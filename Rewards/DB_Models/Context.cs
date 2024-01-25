using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Data.Entity;

namespace Rewards.DB_Models
{
    public class Context : DbContext
    {
        public Context() : base ("context")
        { 

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Forms> Forms { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Reward_Stock> Rewards_Stock { get; set; }
        public DbSet<File> Files { get; set; }

    }
}