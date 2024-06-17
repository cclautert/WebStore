using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebStore.Business.Interfaces;
using WebStore.Business.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebStore.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private static string dbSchema = "dbo";

        private IConfiguration _configuration;
        public UserRepository(IConfiguration configuration)
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
            public static string AddUser = $"{dbSchema}.spAddUser";
            public static string DeleteUser = $"{dbSchema}.spDeleteUser";
            public static string UpdateUser = $"{dbSchema}.spUpdateUser";
            public static string GetAllUser = $"{dbSchema}.spGetAllUser";
        }
        
        public virtual async Task CreateAsync(User user)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand(StoredProcedures.AddUser, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@Name", user.Name);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@PasswordSalt", user.PasswordHSalt);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateAsync(User user)
        {
            try
            {
                await using SqlConnection con = GetConnection();
                SqlCommand cmd = new SqlCommand(StoredProcedures.UpdateUser, con);  
                cmd.CommandType = CommandType.StoredProcedure;  
  
                cmd.Parameters.AddWithValue("@Id", user.Id);  
                cmd.Parameters.AddWithValue("@Name", user.Name);  
                cmd.Parameters.AddWithValue("@Email", user.Email); 
                cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                cmd.Parameters.AddWithValue("@PasswordSalt", user.PasswordHSalt);
                con.Open();  
                cmd.ExecuteNonQuery();  
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
                SqlCommand cmd = new SqlCommand(StoredProcedures.DeleteUser, con);  
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

        public async Task<User> FindByEmailAsync(string email)
        {
            User user = new User();

            await using SqlConnection con = GetConnection();
            string sqlQuery = $"SELECT * FROM User_Authentication WHERE Email='{email}' ";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();  
  
            while (rdr.Read())  
            {  
                user.Id = Guid.Parse(rdr["Id"].ToString());  
                user.Name = rdr["Name"].ToString();
                user.Email = rdr["Email"].ToString();
                user.PasswordHash = (byte[])rdr["PasswordHash"];
                user.PasswordHSalt = (byte[])rdr["PasswordSalt"];
            }

            return user;
        }

        public async Task<bool> PasswordSignInAsync(string email, string password)
        {
            Customer supplier = new Customer();

            await using SqlConnection con = GetConnection();
            string sqlQuery = $"SELECT * FROM User_Authentication WHERE Email='{email}' AND Password='{password}'";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();

            return rdr.HasRows;
        }

        public async Task<bool> AuthenticationAsync(string email, string password)
        {
            Customer supplier = new Customer();

            await using SqlConnection con = GetConnection();
            string sqlQuery = $"SELECT * FROM User_Authentication WHERE Email='{email}' AND Password='{password}'";
            SqlCommand cmd = new SqlCommand(sqlQuery, con);  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();

            return rdr.HasRows;
        }
        
        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> lstUser = new List<User>();
            await using SqlConnection con = GetConnection();
            SqlCommand cmd = new SqlCommand(StoredProcedures.GetAllUser, con);  
            cmd.CommandType = CommandType.StoredProcedure;  
            con.Open();  
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();  
  
            while (rdr.Read())  
            {  
                User user = new User();
                user.Id = Guid.Parse(rdr["Id"].ToString());
                user.Name = rdr["Name"].ToString();
                user.Email = rdr["Email"].ToString();  
                user.PasswordHash = Encoding.ASCII.GetBytes(rdr["PasswordHash"].ToString());
                user.PasswordHSalt = Encoding.ASCII.GetBytes(rdr["PasswordHSalt"].ToString());
  
                lstUser.Add(user);  
            }  
            con.Close();
            return lstUser;
        }

        public string GenerateToken(string id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            var provateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:secretkey"]));
            var credentials = new SigningCredentials(provateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddHours(24);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public Task<IEnumerable<User>> Search(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}