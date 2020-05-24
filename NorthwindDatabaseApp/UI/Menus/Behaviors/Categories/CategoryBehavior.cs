using System.Collections.Generic;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Categories
{
    public abstract class CategoryBehavior : IBehavior
    {
        protected IInput _input;
        protected IDisplay _display;

        protected CategoryBehavior(IDisplay display, IInput input)
        {
            _input = input;
            _display = display;
        }
        
        public abstract bool Run();

        protected int GetUserCategoryIdChoice()
        {
            using (var db = new NorthwindContext())
            {
                var menuOptions = new Dictionary<string, string>();
                foreach (var category in db.Categories)
                {
                    menuOptions.Add(category.CategoryId.ToString(), category.CategoryName);
                }

                return int.Parse(_input.GetMenuOption(menuOptions));
            }
        }
        
        protected string GetCategoryName()
        {
            return _input.GetStringInput("Enter the category name:");
        }

        protected string GetCategoryDescription()
        {
            return _input.GetStringInput("Enter the category description:");
        }
    }
}