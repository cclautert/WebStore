using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;

namespace WebStore.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private static string dbSchema = "dbo";
        
        private IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            var conn = new SqlConnection(GetConnectionString());
            return conn;
        }
        private string? GetConnectionString()
        {
            return this._configuration.GetConnectionString("DefaultConnection");
        }

        public static class StoredProcedures
        {
            public static string AddProduct = $"{dbSchema}.spAddProduct";
            public static string DeleteProduct = $"{dbSchema}.spDeleteProduct";
            public static string UpdateProduct = $"{dbSchema}.spUpdateProduct";
            public static string GetAllProducts = $"{dbSchema}.spGetAllProducts";
        }

        public async Task CreateAsync(Product product)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand("spAddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Value", product.Value);
                cmd.Parameters.AddWithValue("@DateRegister", product.DateRegister);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            Product product = new Product();

            await using SqlConnection con = GetConnection();
            string sqlQuery = "SELECT * FROM Product WHERE Id= " + id;  
            SqlCommand cmd = new SqlCommand(sqlQuery, con);  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();  
  
            while (rdr.Read())  
            {  
                product.Id = Guid.Parse(rdr["Id"].ToString());  
                product.Name = rdr["Name"].ToString();  
                product.Description = rdr["Description"].ToString();  
                product.Value = Decimal.Parse(rdr["Value"].ToString());  
                product.DateRegister = DateTime.Parse(rdr["DateRegister"].ToString()); 
            }

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            List<Product> lstProduct = new List<Product>();
            await using SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand("spGetAllProducts", con);  
            cmd.CommandType = CommandType.StoredProcedure;  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();  
  
            while (rdr.Read())  
            {  
                Product product = new Product();
                product.Id = Guid.Parse(rdr["Id"].ToString());
                product.Name = rdr["Name"].ToString();  
                product.Description = rdr["Description"].ToString();  
                product.Value = Decimal.Parse(rdr["Value"].ToString());  
                product.DateRegister = DateTime.Parse(rdr["DateRegister"].ToString());  
  
                lstProduct.Add(product);  
            }  
            con.Close();
            return lstProduct;
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand("spUpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Value", product.Value);
                cmd.Parameters.AddWithValue("@DateRegister", product.DateRegister);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task RemoveAsync(Guid id)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand("spDeleteProduct", con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@Id", id);  
                con.Open();  
                await cmd.ExecuteNonQueryAsync();  
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<Product>> Search(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
