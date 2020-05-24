namespace NorthwindDatabaseApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ManagerConfig config = new ManagerConfig();
            DatabaseManager databaseManager = new DatabaseManager(config);
            databaseManager.Run();
            
            // Flush NLog when the app closes
            NLog.LogManager.Shutdown();
        }
    }
}