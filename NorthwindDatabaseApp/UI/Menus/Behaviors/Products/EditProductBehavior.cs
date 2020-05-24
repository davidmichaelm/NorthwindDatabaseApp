using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Products
{
    public class EditProductBehavior : ProductBehavior
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public EditProductBehavior(IDisplay display, IInput input) : base(display, input)
        {
        }
        public override bool Run()
        {
            using (var db = new NorthwindContext())
            {
                var productId = GetUserProductIdChoice();
                var product = db.Products.Find(productId);

                if (product != null)
                {
                    if (_input.GetConfirmation("Do you want to edit the product name? (y/n)"))
                    {
                        product.ProductName = GetProductName();
                    }

                    if (_input.GetConfirmation("Do you want to edit the product category? (y/n)"))
                    {
                        product.Category = GetCategory(db);
                    }

                    if (_input.GetConfirmation("Do you want to edit the product's discontinued status? (y/n)"))
                    {
                        product.Discontinued = GetDiscontinued();
                    }
                
                    if (_input.GetConfirmation("Do you want to edit the product's supplier? (y/n)"))
                    {
                        product.Supplier = GetSupplier(db);
                    }
                
                    if (_input.GetConfirmation("Do you want to edit the product's reorder level? (y/n)"))
                    {
                        product.ReorderLevel = GetReorderLevel();
                    }
                
                    if (_input.GetConfirmation("Do you want to edit the product's unit price? (y/n)"))
                    {
                        product.UnitPrice = GetUnitPrice();
                    }
                
                    if (_input.GetConfirmation("Do you want to edit the product's quantity per unit? (y/n)"))
                    {
                        product.QuantityPerUnit = GetQuantityPerUnit();
                    }
                
                    if (_input.GetConfirmation("Do you want to edit the product's units in stock? (y/n)"))
                    {
                        product.UnitsInStock = GetUnitsInStock();
                    }
                
                    if (_input.GetConfirmation("Do you want to edit the product's units on order? (y/n)"))
                    {
                        product.UnitsOnOrder = GetUnitsOnOrder();
                    }
                    
                    ValidationContext context = new ValidationContext(product, null, null);
                    List<ValidationResult> results = new List<ValidationResult>();

                    var isValid = Validator.TryValidateObject(product, context, results, true);
                    if (isValid)
                    {
                        db.SaveChanges();
                        logger.Info("Edited product name {0}", product.ProductName);
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
                    _display.ShowMessage("Product could not be found");
                    logger.Error("Failed to edit category with ProductId {0}", productId);
                }
            }
            
            return true;
        }
    }
}