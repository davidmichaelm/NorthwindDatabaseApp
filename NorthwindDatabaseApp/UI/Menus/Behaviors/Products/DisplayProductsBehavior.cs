using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        
        public override void Run()
        {
            switch (_displayType)
            {
                case ProductDisplayBehaviorType.AllProducts:
                    DisplayProductsAsync(p => true);
                    break;
                case ProductDisplayBehaviorType.ActiveProducts:
                    DisplayProductsAsync(p => !p.Discontinued);
                    break;
                case ProductDisplayBehaviorType.DiscontinuedProducts:
                    DisplayProductsAsync(p => p.Discontinued);
                    break;
                case ProductDisplayBehaviorType.ProductDetails:
                    DisplayProductDetails();
                    break;
                default:
                    logger.Error("Unkown ProductDisplayBehaviorType");
                    break;
            }
            _input.GetStringInput();
        }

        private async void DisplayProductsAsync(Func<Product, bool> searchCondition)
        {
            using (var db = new NorthwindContext())
            {
                var loading = Loading.Create(_display);
                var productList = await GetProductsAsync(db);

                loading.Cancel();

                DisplayProductList(productList.Where(searchCondition).ToList());
                _display.ShowMessage("Press any key to continue");
                
                logger.Info("Fetched {0} products from the database", productList.Count());
            }
        }
        
        private void DisplayProductDetails()
        {
            var userProductIdChoice = GetUserProductIdChoice();
            DisplayChosenProductDetails(userProductIdChoice);
        }

        /*
         * Although this method is async, it won't really matter since the connection
         * is already made when getting the userProductIdChoice...
        */
        private async void DisplayChosenProductDetails(int userProductIdChoice)
        {
            using (var db = new NorthwindContext())
            {
                var productList = await GetProductsAsync(db);
                var product = productList.First(p => p.ProductID == userProductIdChoice);
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
            _display.ShowMessage("Press any key to continue");
        }
        
        private async Task<List<Product>> GetProductsAsync(NorthwindContext db)
        {
            return await db.Products.ToListAsync();
        }

        private void DisplayProductList(List<Product> productList)
        {
            _display.ShowMessage("\n\nProduct List\n" +
                                     "------------\n");
            foreach (var product in productList)
            {
                _display.ShowMessage(product.ProductID + " - " + product.ProductName + "\n");
            }
        }
    }
}