using System;
using System.Collections.Generic;

namespace NorthwindDatabaseApp.UI.Menus.Products
{
    public class DisplayProductsMenu : Menu
    {
        public DisplayProductsMenu(IDisplay display, IInput input) : base(display, input)
        {
            var displayAllProducts = CreateBehavior(BehaviorType.DisplayAllProducts);
            var displayActiveProducts = CreateBehavior(BehaviorType.DisplayActiveProducts);
            var displayDiscontinuedProducts = CreateBehavior(BehaviorType.DisplayDiscontinuedProducts);
            var displayProductDetails = CreateBehavior(BehaviorType.DisplayProductDetails);
            
            MenuOptions = new Dictionary<string, string>
            {
                {"1", "Display all Products"},
                {"2", "Display active Products"},
                {"3", "Display discontinued Products"},
                {"4", "Display Product details"},
                {"5", "Back"}
            };
            MenuActions = new Dictionary<string, Func<bool>>
            {
                {"1", displayAllProducts.Run},
                {"2", displayActiveProducts.Run},
                {"3", displayDiscontinuedProducts.Run},
                {"4", displayProductDetails.Run},
                {"5", ExitMenu}
            };
        }
    }
}