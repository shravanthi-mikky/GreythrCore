using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        SqlConnection sqlConnection;

        SqlDataReader reader;



        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        //Not using
        public ProfileModel UserLogin(LoginModel loginModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("greythrDb"));
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("SP_Login", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@EmployeeID", loginModel.EmployeeID);
                    command.Parameters.AddWithValue("@Password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT * FROM Profile WHERE EmployeeID = '" + loginModel.EmployeeID + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        reader = cmd.ExecuteReader();
                        ProfileModel employeeemodel = new ProfileModel();
                        while (reader.Read())
                        {
                            employeeemodel.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                            employeeemodel.Name = reader["Name"].ToString();
                            employeeemodel.Location = reader["Location"].ToString();
                            employeeemodel.PrimaryContact = Convert.ToInt64(reader["PrimaryContact"]);
                            employeeemodel.CompanyEmail = reader["CompanyEmail"].ToString();
                            employeeemodel.Password = reader["Password"].ToString();


                        }
                        return employeeemodel;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }

            }
        }


        public string GenerateJWTToken2(string email, int userid)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", email),
                new Claim("Id", userid.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(11),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string Login(LoginModel loginModel)
        {

            using (sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("greythrDb")))
                try
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_Login", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();

                    sqlCommand.Parameters.AddWithValue("@EmployeeID", loginModel.EmployeeID);
                    sqlCommand.Parameters.AddWithValue("@Password", loginModel.Password);

                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    //if (rd.HasRows)
                    reader.Close();
                    if (reader != null)
                    {
                        reader = sqlCommand.ExecuteReader();
                        ProfileModel employeeemodel = new ProfileModel();
                        while (reader.Read())
                        {
                            employeeemodel.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
                            employeeemodel.Name = reader["Name"].ToString();
                            employeeemodel.Location = reader["Location"].ToString();
                            employeeemodel.PrimaryContact = Convert.ToInt64(reader["PrimaryContact"]);
                            employeeemodel.CompanyEmail = reader["CompanyEmail"].ToString();
                            employeeemodel.Password = reader["Password"].ToString();
                        }

                        var token = this.GenerateJWTToken2(employeeemodel.CompanyEmail, employeeemodel.EmployeeID);
                        return token;
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);

                }
                finally { sqlConnection.Close(); }
        }

        //Register

        public ProfileModel2 Register(ProfileModel2 registrationModel)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("greythrDb"));

            using (sqlConnection)
                try
                {
                    var password = registrationModel.Password;
                    SqlCommand sqlCommand = new SqlCommand("dbo.SP_Register", sqlConnection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlConnection.Open();
                    sqlCommand.Parameters.AddWithValue("@Name", registrationModel.Name);
                    sqlCommand.Parameters.AddWithValue("@Location", registrationModel.Location);
                    sqlCommand.Parameters.AddWithValue("@PrimaryContact", registrationModel.PrimaryContact);
                    sqlCommand.Parameters.AddWithValue("@CompanyEmail", registrationModel.CompanyEmail);
                    sqlCommand.Parameters.AddWithValue("@Password", registrationModel.Password);

                    int result = sqlCommand.ExecuteNonQuery();
                    if (result > 0)
                        return registrationModel;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }
        }


    }
}
