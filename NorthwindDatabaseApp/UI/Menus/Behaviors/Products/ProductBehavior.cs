using System.Collections.Generic;
using System.Linq;
using NorthwindConsole.Models;

namespace NorthwindDatabaseApp.UI.Menus.Behaviors.Products
{
    public abstract class ProductBehavior : IBehavior
    {
        protected IInput _input;
        protected IDisplay _display;

        protected ProductBehavior(IDisplay display, IInput input)
        {
            _display = display;
            _input = input;
        }

        public abstract void Run();

        protected string GetProductName()
        {
            return _input.GetStringInput("Enter the product name:");
        }

        protected Category GetCategory(NorthwindContext db)
        {
            var categories = db.Categories;
            var categoryOptions = new Dictionary<string, string>();
            foreach (var category in categories)
            {
                categoryOptions.Add(category.CategoryId.ToString(), category.CategoryName);
            }

            var categoryChoice = _input.GetMenuOption(categoryOptions);
            return categories.First(c => c.CategoryId.ToString().Equals(categoryChoice));
        }

        protected bool GetDiscontinued()
        {
            return _input.GetConfirmation("Is the product discontinued? (y/n)");
        }

        protected Supplier GetSupplier(NorthwindContext db)
        {
            var suppliers = db.Suppliers;
            var supplierOptions = new Dictionary<string, string>();
            foreach (var supplier in suppliers)
            {
                supplierOptions.Add(supplier.SupplierId.ToString(), supplier.CompanyName);
            }

            var supplierChoice = _input.GetMenuOption(supplierOptions);
            return suppliers.First(c => c.SupplierId.ToString().Equals(supplierChoice));
        }

        protected short GetReorderLevel()
        {
            return _input.GetShortInput("Enter the reorder level (int):");
        }

        protected decimal GetUnitPrice()
        {
            return _input.GetDecimalInput("Enter the unit price (decimal):");
        }

        protected string GetQuantityPerUnit()
        {
            return _input.GetStringInput("Enter the quantity per unit:");
        }

        protected short GetUnitsInStock()
        {
            return _input.GetShortInput("Enter how many units are in stock (int):");
        }

        protected short GetUnitsOnOrder()
        {
            return _input.GetShortInput("Enter how many units are on order (int):");
        }

        protected int GetUserProductIdChoice()
        {
            using (var db = new NorthwindContext())
            {
                var menuOptions = new Dictionary<string, string>();
                foreach (var product in db.Products)
                {
                    menuOptions.Add(product.ProductID.ToString(), product.ProductName);
                }

                return int.Parse(_input.GetMenuOption(menuOptions));
            }
        }
    }
}