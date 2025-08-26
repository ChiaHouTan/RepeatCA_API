namespace GamesAPI.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string DevelopersCollection { get; set; } = null!;
        public string GamesCollection { get; set; } = null!;
    }
}
