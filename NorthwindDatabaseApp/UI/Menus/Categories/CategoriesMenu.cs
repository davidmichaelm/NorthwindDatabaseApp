using System;
using System.Collections.Generic;

namespace NorthwindDatabaseApp.UI.Menus.Categories
{
    public class CategoriesMenu : Menu
    {
        public CategoriesMenu(IDisplay display, IInput input) : base(display, input)
        {
            Menu displayMenu = new DisplayCategoriesMenu(display, input);
            var addBehavior = CreateBehavior(BehaviorType.AddCategory);
            var editBehavior = CreateBehavior(BehaviorType.EditCategory);
            var deleteBehavior = CreateBehavior(BehaviorType.DeleteCategory);
                
            MenuOptions = new Dictionary<string, string>
            {
                {"1", "Display Categories"},
                {"2", "Add Category"},
                {"3", "Edit Category"},
                {"4", "Delete Category"},
                {"5", "Back"}
            };
            MenuActions = new Dictionary<string, Action>
            {
                {"1", displayMenu.RunMenu},
                {"2", () => addBehavior.Run()},
                {"3", () => editBehavior.Run()},
                {"4", () => deleteBehavior.Run()},
                {"5", ExitMenu}
            };
        }
    }
}