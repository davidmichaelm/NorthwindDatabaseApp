using System;

namespace NorthwindDatabaseApp.UI
{
    public class CommandLineDisplay : IDisplay
    {
        public void ShowMessage(string message)
        {
            Console.Write(message);
        }
    }
}