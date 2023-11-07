namespace MGM.MS.Management.Product.Infrastructure.Config
{
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = string.Empty;

        public string CategoryCollectionName { get; set; } = string.Empty;

        public string ProductCollectionName { get; set; } = string.Empty;
    }
}
