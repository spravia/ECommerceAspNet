using Database.DTO;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ECommerce.Context
{

    //https://learn.microsoft.com/es-es/ef/core/modeling/relationships/one-to-one
    //https://learn.microsoft.com/en-us/ef/core/modeling/relationships


    public class ECommerceContext : DbContext
    {

        public ECommerceContext(DbContextOptions options)  : base(options) { }


        //Tablas
        
        public DbSet<Roles> UsuRoles { get; set; }

        public DbSet<UsuarioECommerceTable> UsuECom { get; set; }

        public DbSet<Categories> Categories { get; set; }

        public DbSet<Products> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>()
                .HasOne(e => e.UsuarioECommerceTable)
                .WithOne(e => e.Roles)
                .HasForeignKey<Roles>(e => e.IdUsuarioECommerce)
                .IsRequired();
            
            modelBuilder.Entity<Categories>().HasIndex(u => u.name).IsUnique();
            
        }
    }

}
