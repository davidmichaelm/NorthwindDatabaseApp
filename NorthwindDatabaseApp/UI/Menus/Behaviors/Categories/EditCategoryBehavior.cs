using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Categories
{
    public class EditCategoryBehavior : CategoryBehavior
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public EditCategoryBehavior(IDisplay display, IInput input) : base(display, input)
        {
        }

        public override bool Run()
        {
            using (var db = new NorthwindContext())
            {
                var categoryId = GetUserCategoryIdChoice();
                var category = db.Categories.Find(categoryId);

                if (category != null)
                {
                    if (_input.GetConfirmation("Do you want to edit the category name? (y/n)"))
                    {
                        category.CategoryName = GetCategoryName();
                    }

                    if (_input.GetConfirmation("Do you want to edit the category description? (y/n)"))
                    {
                        category.Description = GetCategoryDescription();
                    }
                    
                    ValidationContext context = new ValidationContext(category, null, null);
                    List<ValidationResult> results = new List<ValidationResult>();

                    var isValid = Validator.TryValidateObject(category, context, results, true);
                    if (isValid)
                    {
                        db.SaveChanges();
                        logger.Info("Edited category name {0}", category.CategoryName);
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            _display.ShowMessage($"Data entered in field {result.MemberNames.First()} was invalid");
                            logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                        }
                    }
                }
                else
                {
                    _display.ShowMessage("Category could not be found");
                    logger.Error("Failed to edit category with CategoryId {0}", categoryId);
                }
            }

            return true;
        }
    }
}