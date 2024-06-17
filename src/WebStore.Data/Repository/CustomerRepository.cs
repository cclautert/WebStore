using System.Data;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;

namespace WebStore.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private static string dbSchema = "dbo";

        private IConfiguration _configuration;
        public CustomerRepository(IConfiguration configuration)
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
            public static string AddCustomer = $"{dbSchema}.spAddCustomer";
            public static string DeleteCustomer = $"{dbSchema}.spDeleteCustomer";
            public static string UpdateCustomer = $"{dbSchema}.spUpdateCustomer";
            public static string GetAllCustomer = $"{dbSchema}.spGetAllCustomer";
        }
        
        public virtual async Task CreateAsync(Customer customer)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddCustomer, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", customer.Id);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateCustomer, con);  
                cmd.CommandType = CommandType.StoredProcedure;  
  
                cmd.Parameters.AddWithValue("@Id", customer.Id);  
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);  
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);  
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Address", customer.Address);  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            Customer customer = new Customer();

            await using SqlConnection con = GetConnection();
            string sqlQuery = "SELECT * FROM Customer WHERE Id= " + id;  //Needs to change because security
            SqlCommand cmd = new SqlCommand(sqlQuery, con);  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();  
  
            while (rdr.Read())  
            {  
                customer.Id = Guid.Parse(rdr["Id"].ToString());  
                customer.FirstName = rdr["FirstName"].ToString();  
                customer.LastName = rdr["LastName"].ToString();  
                customer.Email = rdr["Email"].ToString();
                customer.Address = rdr["Address"].ToString();  
            }

            return customer;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            List<Customer> lstSupplier = new List<Customer>();
            await using SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllCustomer, con);  
            cmd.CommandType = CommandType.StoredProcedure;  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();  
  
            while (rdr.Read())  
            {  
                Customer supplier = new Customer();
                supplier.Id = Guid.Parse(rdr["Id"].ToString());
                supplier.FirstName = rdr["FirstName"].ToString();  
                supplier.LastName = rdr["LastName"].ToString();  
                supplier.Email = rdr["Email"].ToString();  
                supplier.Address = rdr["Address"].ToString();  
  
                lstSupplier.Add(supplier);  
            }  
            con.Close();
            return lstSupplier;  
        }

        public async Task RemoveAsync(Guid id)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteCustomer, con);  
                cmd.CommandType = CommandType.StoredProcedure;  
                cmd.Parameters.AddWithValue("@Id", id);  
                con.Open();  
                cmd.ExecuteNonQuery();  
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IEnumerable<Customer>> Search(Expression<Func<Customer, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}