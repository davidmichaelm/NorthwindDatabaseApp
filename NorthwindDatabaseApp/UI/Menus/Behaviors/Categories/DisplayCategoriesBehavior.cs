using System.Collections.Generic;
using System.Data.Entity;
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
                    DisplayAllCategoriesAsync();
                    break;
                case CategoryDisplayBehaviorType.CategoriesAndProducts:
                    DisplayAllCategoriesAndProductsAsync();
                    break;
                case CategoryDisplayBehaviorType.CategoryDetails:
                    DisplayCategoryDetails();
                    break;
                default:
                    logger.Error("Unkown CategoryDisplayBehaviorType");
                    break;
            }
            _input.GetStringInput();
        }

        private async void DisplayAllCategoriesAsync()
        {
            using (var db = new NorthwindContext())
            {
                var loading = Loading.Create(_display);
                var categoryList = await db.Categories.ToListAsync();
                loading.Cancel();
                
                _display.ShowMessage("\n\nCategory List\n" +
                                     "------------\n");

                foreach (var category in categoryList)
                {
                    _display.ShowMessage(category.CategoryId + " " + category.CategoryName + " (" +
                                         category.Description + ")\n");
                }

                _display.ShowMessage("Press any key to continue");

                logger.Info("Fetched {0} categories from the database", categoryList.Count());
            }
        }

        private async void DisplayAllCategoriesAndProductsAsync()
        {
            using (var db = new NorthwindContext())
            {
                var productCount = 0;
                
                var loading = Loading.Create(_display);
                var categoryList = await db.Categories.Include("Products").ToListAsync();
                loading.Cancel();
                
                _display.ShowMessage("\n\nCategory List\n" +
                                     "------------\n");
                
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
                
                _display.ShowMessage("Press any key to continue");

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
            
            _display.ShowMessage("Press any key to continue");
        }
    }
}