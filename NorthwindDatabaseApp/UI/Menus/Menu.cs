using System;
using System.Collections.Generic;
using NorthwindDatabaseApp.UI.Menus.Behaviors.Categories;
using NorthwindDatabaseApp.UI.Menus.Behaviors.Products;

namespace NorthwindDatabaseApp.UI.Menus
{
    public abstract class Menu
    {
        protected IDisplay _display;
        protected IInput _input;
        protected Dictionary<string, string> MenuOptions;
        protected Dictionary<string, Func<bool>> MenuActions;
        
        protected Menu(IDisplay display, IInput input)
        {
            _display = display;
            _input = input;
        }

        // Return true to run the menu again, false to stop
        public bool RunMenu()
        {
            bool keepRunning;
            do
            {
                var userChoice = _input.GetMenuOption(MenuOptions);
                keepRunning = MenuActions[userChoice]();
            } while (keepRunning);

            return true;
        }

        public bool ExitMenu()
        {
            return false;
        }
        
        public IBehavior CreateBehavior(BehaviorType type)
        {
            switch (type)
            {
                case BehaviorType.DisplayAllProducts:
                    return new DisplayProductsBehavior(_display, _input, ProductDisplayBehaviorType.AllProducts);
                case BehaviorType.DisplayActiveProducts:
                    return new DisplayProductsBehavior(_display, _input, ProductDisplayBehaviorType.ActiveProducts);
                case BehaviorType.DisplayDiscontinuedProducts:
                    return new DisplayProductsBehavior(_display, _input, ProductDisplayBehaviorType.DiscontinuedProducts);
                case BehaviorType.DisplayProductDetails:
                    return new DisplayProductsBehavior(_display, _input, ProductDisplayBehaviorType.ProductDetails);
                case BehaviorType.AddProduct:
                    return new AddProductBehavior(_display, _input);
                case BehaviorType.EditProduct:
                    return new EditProductBehavior(_display, _input);
                case BehaviorType.DeleteProduct:
                    return new DeleteProductBehavior(_display, _input);
                
                case BehaviorType.DisplayAllCategories:
                    return new DisplayCategoriesBehavior(_display, _input, CategoryDisplayBehaviorType.AllCategories);
                case BehaviorType.DisplayCategoriesAndProducts:
                    return new DisplayCategoriesBehavior(_display, _input, CategoryDisplayBehaviorType.CategoriesAndProducts);
                case BehaviorType.DisplayCategoryDetails:
                    return new DisplayCategoriesBehavior(_display, _input, CategoryDisplayBehaviorType.CategoryDetails);
                case BehaviorType.AddCategory:
                    return new AddCategoryBehavior(_display, _input);
                case BehaviorType.EditCategory:
                    return new EditCategoryBehavior(_display, _input);
                case BehaviorType.DeleteCategory:
                    return new DeleteCategoryBehavior(_display, _input);
                default:
                    return null;
            }
        }
    }
}