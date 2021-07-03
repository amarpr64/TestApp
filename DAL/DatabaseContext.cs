using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() { }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Category>().Property(c => c.Name)
                                            .IsRequired()
                                            .IsUnicode(false)
                                            .HasMaxLength(50);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=AMAR\\SQLEXPRESS; initial catalog=EFCore8PM;persist security info=True;user id=sa;password=64amarpr!");
            }
        }

        public Product usp_getproduct(int ProductId)
        {
            Product product = new Product();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_getproduct";
                command.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter("ProductId", ProductId);

                command.Parameters.Add(parameter);
                this.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.ProductId = reader.GetInt32("ProductId");
                        product.Name = reader.GetString("Name");
                        product.Description = reader.GetString("Description");
                        product.UnitPrice = reader.GetDecimal("UnitPrice");
                        product.CategoryId = reader.GetInt32("CategoryId");
                    }
                }
                this.Database.CloseConnection();
            }
            return product;
        }

        public Product udf_getproduct(int ProductId)
        {
            return Products.FromSqlRaw<Product>("Select * from udf_getproduct(@ProductId)", new SqlParameter("ProductId", ProductId)).FirstOrDefault();
        }
    }
}
