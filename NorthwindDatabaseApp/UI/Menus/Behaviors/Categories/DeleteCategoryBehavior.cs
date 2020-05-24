using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Categories
{
    public class DeleteCategoryBehavior : CategoryBehavior
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DeleteCategoryBehavior(IDisplay display, IInput input) : base(display, input)
        {
        }

        public override bool Run()
        {
            var userCategoryIdChoice = GetUserCategoryIdChoice();

            if (_input.GetConfirmation("Are you sure you want to delete this category? (y/n)"))
            {
                using (var db = new NorthwindContext())
                {
                    var category = db.Categories.Find(userCategoryIdChoice);
                    var product = db.Products.FirstOrDefault(p => p.CategoryId == userCategoryIdChoice);
                    if (product != null && category != null)
                    {
                        _display.ShowMessage("Category found in Products table, unable to delete\n");
                        logger.Info("Unable to delete category {0}, category referenced in Products table", category.CategoryName);
                    }
                    else if (category != null)
                    {
                        db.Categories.Remove(category);
                        db.SaveChanges();
                        
                        logger.Info("Deleted category {0}", category.CategoryName);
                    }
                    else
                    {
                        _display.ShowMessage("Unable to find category to delete");
                        logger.Error("Failed to delete category with CategoryId {0}", userCategoryIdChoice);
                    }
                }
            }

            return true;
        }
    }
}