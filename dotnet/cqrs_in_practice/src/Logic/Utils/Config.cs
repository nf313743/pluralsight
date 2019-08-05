namespace Logic.Utils
{
    public sealed class Config
    {
        public Config(int numberOfDatabaseRetries)
        {
            NumberOfDatabaseRetries = numberOfDatabaseRetries;
        }

        public int NumberOfDatabaseRetries { get; }
    }
}