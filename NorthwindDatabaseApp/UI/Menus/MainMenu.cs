using System;
using System.Collections.Generic;
using NorthwindDatabaseApp.UI.Menus.Categories;
using NorthwindDatabaseApp.UI.Menus.Products;

namespace NorthwindDatabaseApp.UI.Menus
{
    public class MainMenu : Menu
    {
        public MainMenu(IDisplay display, IInput input) : base(display, input)
        {
            var productsMenu = new ProductsMenu(_display, _input);
            var categoriesMenu = new CategoriesMenu(_display, _input);
            
            MenuOptions = new Dictionary<string, string>
            {
                {"1", "Manage Products"},
                {"2", "Manage Categories"},
                {"3", "Exit"}
            };
            MenuActions = new Dictionary<string, Func<bool>>
            {
                {"1", productsMenu.RunMenu},
                {"2", categoriesMenu.RunMenu},
                {"3", ExitMenu}
            };
        }

        public new bool RunMenu()
        {
            bool keepRunning;
            do
            {
                var userChoice = _input.GetMenuOption(MenuOptions);
                keepRunning = MenuActions[userChoice]();
            } while (keepRunning);

            return false;
        }
    }
}