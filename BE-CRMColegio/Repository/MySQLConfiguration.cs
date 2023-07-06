namespace BE_CRMColegio.Repository
{
    public class MySQLConfiguration
    {
        public MySQLConfiguration(String connectionString)
        {
            ConnectionString = connectionString;
        }
        public String ConnectionString { get; set; }
    }
}
