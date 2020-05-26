using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Categories
{
    public class AddCategoryBehavior : CategoryBehavior
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public AddCategoryBehavior(IDisplay display, IInput input) : base(display, input)
        {
        }

        public override void Run()
        {
            var category = new Category
            {
                CategoryName = GetCategoryName(), 
                Description = GetCategoryDescription()
            };
            
            ValidationContext context = new ValidationContext(category, null, null);
            List<ValidationResult> results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(category, context, results, true);
            if (isValid)
            {
                using (var db = new NorthwindContext())
                {
                    db.Categories.Add(category);
                    db.SaveChanges();

                    logger.Info("Added category {0}", category.CategoryName);
                }
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
    }
}