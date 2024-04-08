using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Persistence.Contexts
{
    public  class BaseDbContext :DbContext
    {
        public IConfiguration Configuration { get; set; }
        public DbSet<Brand> Brands { get; set; }

        public BaseDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        { 
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString("RentACar"));
        }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(a => { 
                    a.ToTable("Brands").HasKey(k=>k.Id);
                a.Property(p=>p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
            });

            Brand[] brandEntitySeed = { new(1,"bmw"),new(2,"mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeed);
        }
    }
}
