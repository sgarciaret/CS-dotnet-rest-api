namespace ApiRest.Repository
{
    public class DataAccess
    {
        private string sqlConnectionString;

        public string SQLConnectionString { get => sqlConnectionString; }

        public DataAccess(string ConnectionSql)
        {
            sqlConnectionString = ConnectionSql;
        }
    }
}
