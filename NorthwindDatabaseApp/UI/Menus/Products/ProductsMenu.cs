using System;
using System.Collections.Generic;

namespace NorthwindDatabaseApp.UI.Menus.Products
{
    public class ProductsMenu : Menu
    {
        private Menu _displayMenu;
        private IBehavior _addBehavior;
        private IBehavior _deleteBehavior;
        private IBehavior _editBehavior;

        public ProductsMenu(IDisplay display, IInput input) : base(display, input)
        {
            _displayMenu = new DisplayProductsMenu(display, input);
            _addBehavior = CreateBehavior(BehaviorType.AddProduct);
            _editBehavior = CreateBehavior(BehaviorType.EditProduct);
            _deleteBehavior = CreateBehavior(BehaviorType.DeleteProduct);
                
            MenuOptions = new Dictionary<string, string>
            {
                {"1", "Display Products"},
                {"2", "Add Product"},
                {"3", "Edit Product"},
                {"4", "Delete Product"},
                {"5", "Back"}
            };
            MenuActions = new Dictionary<string, Func<bool>>
            {
                {"1", _displayMenu.RunMenu},
                {"2", _addBehavior.Run},
                {"3", _editBehavior.Run},
                {"4", _deleteBehavior.Run},
                {"5", ExitMenu}
            };
        }
    }
}