using ApiRest.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace ApiRest.Repository
{
    public class ProductsSQLServer : IProductsInMemory
    {
        private string ConnectionString;
        private readonly ILogger<ProductsSQLServer> log;

        public ProductsSQLServer(DataAccess connectionString, ILogger<ProductsSQLServer> log)
        {
            ConnectionString = connectionString.SQLConnectionString;
            this.log = log;
        }

        private SqlConnection connection()
        {
            return new SqlConnection(ConnectionString);
        }
        public async Task CreateProductAsync(Product product)
        {
            SqlConnection sqlConnection = connection();
            SqlCommand Comm = null;

            try
            {
                sqlConnection.Open();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.RegisterProducts";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = product.Name;
                Comm.Parameters.Add("@Description", SqlDbType.VarChar, 5000).Value = product.Description;
                Comm.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = product.SKU;
                await Comm.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo nu error al dar de alta " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            await Task.CompletedTask;
        }

        public async Task DeleteProductAsync(string SKU)
        {
            SqlConnection sqlConnection = connection();
            SqlCommand Comm = null;

            try
            {
                sqlConnection.Open();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.Delete_Product";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                await Comm.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo nu error al eliminar el producto " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            await Task.CompletedTask;
        }

        public async Task<Product> GiveProductAsync(string SKU)
        {
            SqlConnection sqlConnection = connection();
            SqlCommand Comm = null;
            Product product = null;

            try
            {
                sqlConnection.Open();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.Obtain_Products";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = SKU;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                if (reader.Read())
                {
                    product = new Product
                    {
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDouble(reader["Price"].ToString()),
                        SKU = reader["SKU"].ToString()
                };

                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo nu error al dar de alta " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GiveProductsAsync()
        {
            SqlConnection sqlConnection = connection();
            SqlCommand Comm = null;
            List<Product> products = new List<Product>();
            Product product = null;

            try
            {
                sqlConnection.Open();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.Obtain_Products";
                Comm.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await Comm.ExecuteReaderAsync();

                while (reader.Read())
                {
                    product = new Product
                    {
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"].ToString(),
                        Price = Convert.ToDouble(reader["Price"].ToString()),
                        SKU = reader["SKU"].ToString()
                    };

                    products.Add(product);

                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo nu error al dar de alta " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return products;
        }

        public async Task ModifyProductAsync(Product product)
        {
            SqlConnection sqlConnection = connection();
            SqlCommand Comm = null;

            try
            {
                sqlConnection.Open();
                Comm = sqlConnection.CreateCommand();
                Comm.CommandText = "dbo.Modify_Product";
                Comm.CommandType = CommandType.StoredProcedure;
                Comm.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = product.Name;
                Comm.Parameters.Add("@Description", SqlDbType.VarChar, 5000).Value = product.Description;
                Comm.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                Comm.Parameters.Add("@SKU", SqlDbType.VarChar, 100).Value = product.SKU;
                await Comm.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                log.LogError(ex.ToString());
                throw new Exception("Se produjo nu error al modificar el producto " + ex.Message);
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            await Task.CompletedTask;
        }
    }
}
