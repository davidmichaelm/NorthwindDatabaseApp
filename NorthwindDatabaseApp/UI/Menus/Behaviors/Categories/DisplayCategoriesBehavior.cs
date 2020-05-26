using System.Collections.Generic;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Categories
{
    public class DisplayCategoriesBehavior : CategoryBehavior
    {
        private CategoryDisplayBehaviorType _displayType;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        public DisplayCategoriesBehavior(IDisplay display, IInput input, CategoryDisplayBehaviorType displayType) : base(display, input)
        {
            _displayType = displayType;
        }

        public override void Run()
        {
            switch (_displayType)
            {
                case CategoryDisplayBehaviorType.AllCategories:
                    DisplayAllCategories();
                    break;
                case CategoryDisplayBehaviorType.CategoriesAndProducts:
                    DisplayAllCategoriesAndProducts();
                    break;
                case CategoryDisplayBehaviorType.CategoryDetails:
                    DisplayCategoryDetails();
                    break;
                default:
                    logger.Error("Unkown CategoryDisplayBehaviorType");
                    break;
            }
        }
        
        private void DisplayAllCategories()
        {
            using (var db = new NorthwindContext())
            {
                var categoryList = db.Categories;
                foreach (var category in categoryList)
                {
                    _display.ShowMessage(category.CategoryId + " " + category.CategoryName + " (" + category.Description + ")\n");
                }
                
                logger.Info("Fetched {0} categories from the database", categoryList.Count());
            }
        }
        
        private void DisplayAllCategoriesAndProducts()
        {
            using (var db = new NorthwindContext())
            {
                var productCount = 0;
                var categoryList = db.Categories.Include("Products");
                foreach (var category in categoryList)
                {
                    _display.ShowMessage(category.CategoryId + " - " + category.CategoryName + "\n");

                    var categoryProducts = category.Products.Where(p => !p.Discontinued).ToList();
                    foreach (var product in categoryProducts)
                    {
                        _display.ShowMessage("    " + product.ProductName + "\n");
                    }
                    
                    productCount += categoryProducts.Count();
                }
                
                logger.Info("Fetched {0} categories and {1} products from database", categoryList.Count(), productCount);
            }
        }
        
        private void DisplayCategoryDetails()
        {
            var userCategoryIdChoice = GetUserCategoryIdChoice();

            using (var db = new NorthwindContext())
            {
                var category = db.Categories.Find(userCategoryIdChoice);
                if (category != null)
                {
                    _display.ShowMessage(category.CategoryId + " - " + category.CategoryName + " " + category.Description + "\n");
                    
                    foreach (var product in category.Products.Where(p => !p.Discontinued))
                    {
                        _display.ShowMessage("    " + product.ProductName + "\n");
                    }
                    
                    logger.Info("Fetched 1 category from the database");
                }
                else
                {
                    _display.ShowMessage("Category could not be found\n");
                    logger.Error("Failed to find category with id {0}", userCategoryIdChoice);
                }
            }
        }
    }
}