namespace Exemplo.Infrastructure.MongoDB.Configurations
{
    public sealed class MongoDBConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public int MaxConnectionIdleTime { get; set; }
    }
}