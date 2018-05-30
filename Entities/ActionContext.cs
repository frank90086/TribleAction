using System;
using Microsoft.EntityFrameworkCore;

namespace TribleAction.Entities
{
    public class ActionContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Lottery> Lotterys { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Team> Teams { get; set; }
    }
}