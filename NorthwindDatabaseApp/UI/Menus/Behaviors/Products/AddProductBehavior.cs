using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Products
{
    public class AddProductBehavior : ProductBehavior
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public AddProductBehavior(IDisplay display, IInput input) : base(display, input)
        {
        }

        public override void Run()
        {
            using (var db = new NorthwindContext())
            {
                Product product = new Product
                {
                    ProductName = GetProductName(),
                    Category = GetCategory(db),
                    Discontinued = GetDiscontinued(),
                    Supplier = GetSupplier(db),
                    ReorderLevel = GetReorderLevel(),
                    UnitPrice = GetUnitPrice(),
                    QuantityPerUnit = GetQuantityPerUnit(),
                    UnitsInStock = GetUnitsInStock(),
                    UnitsOnOrder = GetUnitsOnOrder()
                };
                
                ValidationContext context = new ValidationContext(product, null, null);
                List<ValidationResult> results = new List<ValidationResult>();

                var isValid = Validator.TryValidateObject(product, context, results, true);
                if (isValid)
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    foreach (var result in results)
                    {
                        _display.ShowMessage($"Data entered in field {result.MemberNames.First()} was invalid");
                        logger.Error($"{result.MemberNames.First()} : {result.ErrorMessage}");
                    }
                }

                logger.Info("Added product {0}", product.ProductName);

            }
        }
    }
}