using ApiRest.Model;
using ApiRest.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace ApiRest.Repository
{
    public class UsersSQLServer : IUsersSQLServer
    {
        private string ConnectionString;
        private readonly ILogger<UsersSQLServer> log;

        public UsersSQLServer(DataAccess connectionString, ILogger<UsersSQLServer> log)
        {
            ConnectionString = connectionString.SQLConnectionString;
            this.log = log;
        }

        private SqlConnection connection()
        {
            return new SqlConnection(ConnectionString);
        }

        public async Task<UserAPI> GiveUser(LoginAPI login)
        {
            SqlConnection sqlConnection = connection();
            SqlCommand Comm = null;
            UserAPI user = null;

            try
            {
                sqlConnection.Open();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.UserAPI_Obtain";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@UserAPI", SqlDbType.VarChar, 50).Value = login.userAPI;
                Comm.Parameters.Add("@PassAPI", SqlDbType.VarChar, 50).Value = login.passAPI;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (reader.Read())
                {
                    user = new UserAPI
                    {
                        User = reader["UserApi"].ToString(),
                        Email = reader["EmailUser"].ToString()
                    };

                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo un error al obtener los datos del usuario del login" + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return user;
        }
    }
}
