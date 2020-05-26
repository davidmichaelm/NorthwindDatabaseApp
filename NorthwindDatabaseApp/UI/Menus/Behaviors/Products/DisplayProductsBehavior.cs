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
                    DisplayAllProductsAsync();
                    _input.GetStringInput();
                    break;
                case ProductDisplayBehaviorType.ActiveProducts:
                    DisplaySomeProducts(p => !p.Discontinued);
                    break;
                case ProductDisplayBehaviorType.DiscontinuedProducts:
                    DisplaySomeProducts(p => p.Discontinued);
                    break;
                case ProductDisplayBehaviorType.ProductDetails:
                    DisplayProductDetails();
                    break;
                default:
                    logger.Error("Unkown ProductDisplayBehaviorType");
                    break;            }
        }

        private async void DisplayAllProductsAsync()
        {
            using (var db = new NorthwindContext())
            {
                var cts = new CancellationTokenSource();
                var progress = Progress(cts.Token);
                var productTask = GetProductsAsync(db);
                

                var productList = await productTask;
                cts.Cancel();

                foreach (var product in productList)
                {
                    _display.ShowMessage(product.ProductID + " - " + product.ProductName + "\n");
                }
                _display.ShowMessage("Press any key to continue");
                
                logger.Info("Fetched {0} products from the database", productList.Count());
            }
        }

        private async Task Progress(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                _display.ShowMessage("\rFetching... /");
                await Task.Delay(100, token);
                _display.ShowMessage("\rFetching... —");
                await Task.Delay(100, token);
                _display.ShowMessage("\rFetching... \\");
                await Task.Delay(100, token);
                _display.ShowMessage("\rFetching... |");
                await Task.Delay(100, token);
            }
        }

        private async Task<List<Product>> GetProductsAsync(NorthwindContext db)
        {
            return await db.Products.ToListAsync();
        }

        private void DisplaySomeProducts(Func<Product, bool> searchCondition)
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
        }

        private void DisplayProductDetails()
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
        }
    }
}