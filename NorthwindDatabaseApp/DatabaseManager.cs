using NorthwindDatabaseApp.UI;
using NorthwindDatabaseApp.UI.Menus;

namespace NorthwindDatabaseApp
{
    public class DatabaseManager
    {
        private IInput _input;
        private IDisplay _display;

        public DatabaseManager(ManagerConfig config)
        {
            _input = config.Input;
            _display = config.Display;
        }

        public void Run()
        {
            var mainMenu = new MainMenu(_display, _input);
            var keepRunning = true;

            while (keepRunning)
            {
                keepRunning = mainMenu.RunMenu();
            }
        }
    }
}