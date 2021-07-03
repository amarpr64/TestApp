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
        public DatabaseContext()
        {
            //needed for migration
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("data source=AMAR\\DELL\\SqlExpress; initial catalog=EFCore8PM;persist security info=True;user id=sa;password=$Amarpr09$;");
            }
        }

        public Product usp_getproduct(int Id)
        {
            Product product = new Product();
            using (var command = this.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_getproduct";
                command.CommandType = CommandType.StoredProcedure;
                var parameter = new SqlParameter("Id", Id);

                //for output parameter
                //var parm = new SqlParameter()
                //{
                //    ParameterName = "@ProductId",
                //    SqlDbType = SqlDbType.Int,
                //    Direction = ParameterDirection.Output
                //};

                command.Parameters.Add(parameter);
                this.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product.Id = reader.GetInt32("Id");
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

        public Product fn_getproduct(int Id)
        {
            return this.Products.FromSqlRaw("select * from fn_getproduct(@id)", new SqlParameter("id", Id)).FirstOrDefault();
        }
    }
}
