using Microsoft.EntityFrameworkCore;
using RFD.Entities.DTO;

namespace RFD.DataAccess.EntityFramework
{
    public class RFDContext : DbContext
    {
        public RFDContext(DbContextOptions<RFDContext> options) : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Insider>().HasMany(x => x.Transections).WithOne(x=>x.Insider).OnDelete(DeleteBehavior.Cascade);
          //  modelBuilder.Entity<Phishing>().HasMany(x => x.Transections).WithOne(x => x.Phishing).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
        //public virtual DbSet<Component> Components { get; set; }
       // public virtual DbSet<Phishing> Phishing { get; set; }
        public virtual DbSet<Insider> Insider { get; set; }
        public virtual DbSet<Transection> Transections { get; set; }
        public virtual DbSet<Summary> Summaries { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
