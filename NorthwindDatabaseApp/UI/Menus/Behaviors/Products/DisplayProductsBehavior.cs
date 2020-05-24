using System;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Products
{
    public class DisplayProductsBehavior : ProductBehavior
    {
        private ProductDisplayBehaviorType _displayType;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DisplayProductsBehavior(IDisplay display, IInput input, ProductDisplayBehaviorType displayType) : base(display, input)
        {
            _displayType = displayType;
        }
        
        public override bool Run()
        {
            switch (_displayType)
            {
                case ProductDisplayBehaviorType.AllProducts:
                    return DisplayAllProducts();
                case ProductDisplayBehaviorType.ActiveProducts:
                    return DisplaySomeProducts(p => !p.Discontinued);
                case ProductDisplayBehaviorType.DiscontinuedProducts:
                    return DisplaySomeProducts(p => p.Discontinued);
                case ProductDisplayBehaviorType.ProductDetails:
                    return DisplayProductDetails();
                default:
                    logger.Error("Unkown ProductDisplayBehaviorType");
                    return true;
            }
        }

        private bool DisplayAllProducts()
        {
            using (var db = new NorthwindContext())
            {
                var productList = db.Products;
                foreach (var product in productList)
                {
                    _display.ShowMessage(product.ProductID + " - " + product.ProductName + "\n");
                }
                
                logger.Info("Fetched {0} products from the database", productList.Count());
            }
            
            return true;
        }

        private bool DisplaySomeProducts(Func<Product, bool> searchCondition)
        {
            using (var db = new NorthwindContext())
            {
                var productList = db.Products.Where(searchCondition).ToList();
                foreach (var product in productList)
                {
                    _display.ShowMessage(product.ProductID + " - " + product.ProductName + "\n");
                }
                
                logger.Info("Fetched {0} products from the database", productList.Count);
            }
            return true;
        }

        private bool DisplayProductDetails()
        {
            var userProductIdChoice = GetUserProductIdChoice();

            using (var db = new NorthwindContext())
            {
                var product = db.Products.Find(userProductIdChoice);
                if (product != null)
                {
                    _display.ShowMessage("ProductID - " + product.ProductID + "\n" +
                                         "ProductName - " + product.ProductName + "\n" +
                                         "QuantityPerUnit - " + product.QuantityPerUnit + "\n" +
                                         "UnitPrice - " + product.UnitPrice + "\n" +
                                         "UnitsInStock - " + product.UnitsInStock + "\n" +
                                         "UnitsOnOrder - " + product.UnitsOnOrder + "\n" +
                                         "ReorderLevel - " + product.ReorderLevel + "\n" +
                                         "Discontinued - " + product.Discontinued + "\n");

                    logger.Info("Fetched 1 product from the database");
                }
                else
                {
                    _display.ShowMessage("Product with that ID was not found");
                    logger.Error("Failed to find product with ProductId {0}", userProductIdChoice);
                }
            }
            
            return true;
        }

        
    }
}