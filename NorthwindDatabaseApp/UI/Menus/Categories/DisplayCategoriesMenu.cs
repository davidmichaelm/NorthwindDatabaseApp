using System;
using System.Collections.Generic;

namespace NorthwindDatabaseApp.UI.Menus.Categories
{
    public class DisplayCategoriesMenu : Menu
    {
        public DisplayCategoriesMenu(IDisplay display, IInput input) : base(display, input)
        {
            var displayAllCategories = CreateBehavior(BehaviorType.DisplayAllCategories);
            var displayCategoriesAndProducts = CreateBehavior(BehaviorType.DisplayCategoriesAndProducts);
            var displayCategoryDetails = CreateBehavior(BehaviorType.DisplayCategoryDetails);
            
            MenuOptions = new Dictionary<string, string>
            {
                {"1", "Display all Categories"},
                {"2", "Display all Categories and related active Products"},
                {"3", "Display single Category details and related active Products"},
                {"4", "Back"}
            };
            MenuActions = new Dictionary<string, Action>
            {
                {"1", () => displayAllCategories.Run()},
                {"2", () => displayCategoriesAndProducts.Run()},
                {"3", () => displayCategoryDetails.Run()},
                {"4", ExitMenu}
            };
        }
    }
}