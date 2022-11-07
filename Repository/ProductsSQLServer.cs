using ApiRest.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace ApiRest.Repository
{
    public class ProductsSQLServer : IProductsInMemory
    {
        private string ConnectionString;

        public ProductsSQLServer(DataAccess connectionString)
        {
            ConnectionString = connectionString.SQLConnectionString;
        }

        private SqlConnection connection()
        {
            return new SqlConnection(ConnectionString);
        }
        public void CreateProduct(Product product)
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
                Comm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception("Se produjo nu error al dar de alta " + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public void DeleteProduct(string SKU)
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
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo nu error al eliminar el producto " + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public Product GiveProduct(string SKU)
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
                SqlDataReader reader = Comm.ExecuteReader();

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
                throw new Exception("Se produjo nu error al dar de alta " + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return product;
        }

        public IEnumerable<Product> GiveProducts()
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
                SqlDataReader reader = Comm.ExecuteReader();

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
                throw new Exception("Se produjo nu error al dar de alta " + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
            return products;
        }

        public void ModifyProduct(Product product)
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
                Comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo nu error al modificar el producto " + ex.ToString());
            }
            finally
            {
                Comm.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }
    }
}
