using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Products
{
    public class DeleteProductBehavior : ProductBehavior
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DeleteProductBehavior(IDisplay display, IInput input) : base(display, input)
        {
        }

        public override bool Run()
        {
            var userProductIdChoice = GetUserProductIdChoice();

            if (_input.GetConfirmation("Are you sure you want to delete this product? (y/n)"))
            {
                using (var db = new NorthwindContext())
                {
                    var product = db.Products.Find(userProductIdChoice);
                    var orderDetail = db.OrderDetails.FirstOrDefault(od => od.ProductID == userProductIdChoice);
                    if (orderDetail != null && product != null)
                    {
                        _display.ShowMessage("Product found in OrderDetails table, unable to delete\n");
                        logger.Info("Unable to delete product {0}, product referenced in OrderDetails table", product.ProductName);

                    }
                    else if (product != null)
                    {
                        db.Products.Remove(product);
                        db.SaveChanges();
                        
                        logger.Info("Deleted product {0}", product.ProductName);

                    }
                    else
                    {
                        _display.ShowMessage("Unable to find product to delete");
                        logger.Error("Failed to edit product with ProductId {0}", userProductIdChoice);

                    }
                }
            }

            return true;
        }
    }
}