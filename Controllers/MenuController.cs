using Microsoft.AspNetCore.Mvc;
using CafeApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace CafeApp.Controllers
{
    public class MenuController : Controller
    {
        // Static menu items
        private static List<MenuItem> Menu = new List<MenuItem>
        {
            new MenuItem { Id = 1, Name = "Cappuccino", Category = "Coffee", Price = 5.00M, DiscountPercent = 10, ImagePath = "/images/menu/Cappuccino.jpeg" },
            new MenuItem { Id = 2, Name = "Espresso", Category = "Coffee", Price = 4.00M, DiscountPercent = 0, ImagePath = "/images/menu/ESPRESSO.jpg" },
            new MenuItem { Id = 3, Name = "Latte", Category = "Coffee", Price = 5.50M, DiscountPercent = 5, ImagePath = "/images/menu/LATTE.jpg" },
            new MenuItem { Id = 4, Name = "Chocolate Croissant", Category = "Food", Price = 3.50M, DiscountPercent = 0, ImagePath = "/images/menu/croissant.jpg" },
            new MenuItem { Id = 5, Name = "Muffin", Category = "Food", Price = 3.00M, DiscountPercent = 20, ImagePath = "/images/menu/muffin.jpg" },
            new MenuItem { Id = 6, Name = "Iced Oat Milk Latte", Category = "Coffee", Price = 6.00M, DiscountPercent = 5, ImagePath = "/images/menu/Iced Oat milk Latte.jpg", IsTrending = true, IsEcoFriendly = true },
            new MenuItem { Id = 7, Name = "Iced Matcha Latte", Category = "Plant-Based", Price = 6.50M, DiscountPercent = 0, ImagePath = "/images/menu/Iced Matcha Latte.jpg", IsTrending = true, IsEcoFriendly = true },
            new MenuItem { Id = 8, Name = "Iced Caramel Latte", Category = "Coffee", Price = 6.00M, DiscountPercent = 5, ImagePath = "/images/menu/iced caramel latte.jpg" },
            new MenuItem { Id = 9, Name = "Hot Chocolate", Category = "Coffee", Price = 5.00M, DiscountPercent = 0, ImagePath = "/images/menu/hot chocolate.jpg" },
            new MenuItem { Id = 10, Name = "Vegan Avocado Toast", Category = "Food", Price = 7.50M, DiscountPercent = 10, ImagePath = "/images/menu/avocado toast.jpg", IsEcoFriendly = true },
            new MenuItem { Id = 11, Name = "Chia Seed Pudding", Category = "Plant-Based", Price = 6.20M, DiscountPercent = 5, ImagePath = "/images/menu/chia-pudding.jpeg", IsTrending = true, IsEcoFriendly = true },

            // New Arrivals
            new MenuItem { Id = 12, Name = "Iced Americano", Category = "New Arrivals", Description = "Refreshing cold coffee with bold espresso and ice.", ImagePath = "/images/menu/Iced americano.jpg", Price = 5.00M },
            new MenuItem { Id = 13, Name = "White Chocolate Balls", Category = "New Arrivals", Description = "Sweet and creamy white chocolate energy balls.", ImagePath = "/images/menu/white chocolate balls.jpg", Price = 3.50M },
            new MenuItem { Id = 14, Name = "Velvet Cake", Category = "New Arrivals", Description = "Classic red velvet slice with soft frosting.", ImagePath = "/images/menu/Velvet cake.jpg", Price = 6.00M },
            new MenuItem { Id = 15, Name = "Birthday Cake", Category = "New Arrivals", Description = "Colorful layered birthday cake with sprinkles.", ImagePath = "/images/menu/birthday cake.jpg", Price = 6.50M },
            new MenuItem { Id = 16, Name = "Chocolate Brownie", Category = "New Arrivals", Description = "Rich and fudgy brownie with dark chocolate chunks.", ImagePath = "/images/menu/Chocolate-brownie.jpg", Price = 4.50M }
        };

        // Static cart items
        private static List<CartItem> CartItems = new List<CartItem>();

        // Display menu with optional category filtering
        public IActionResult Index(string category)
        {
            if (string.IsNullOrEmpty(category) || category == "All")
                return View(Menu);

            var filtered = Menu.Where(m => m.Category == category).ToList();
            return View(filtered);
        }

        // Add item to cart
        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            var item = Menu.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                var existing = CartItems.FirstOrDefault(c => c.Item.Id == id);
                if (existing != null)
                {
                    existing.Quantity += quantity;
                }
                else
                {
                    CartItems.Add(new CartItem
                    {
                        Item = item,
                        Quantity = quantity
                    });
                }
            }
            return RedirectToAction("CartView");
        }

        // Show current cart items
        public IActionResult CartView()
        {
            return View("Cart", CartItems);
        }

        // Checkout with payment and eco-discount logic
        [HttpPost]
        public IActionResult Checkout(string paymentMethod, bool reusableCup)
        {
            decimal total = CartItems.Sum(c => c.TotalPrice);

            if (reusableCup)
            {
                total -= 0.50M;
                if (total < 0) total = 0;
            }

            // Pass values to CheckoutConfirmation via TempData
            TempData["ReusableCup"] = reusableCup;
            TempData["PaymentMethod"] = paymentMethod;

            // Clear the cart
            CartItems.Clear();

            return RedirectToAction("CheckoutConfirmation", new { total = total });
        }

        // Show confirmation page
        public IActionResult CheckoutConfirmation(decimal total)
        {
            ViewBag.ReusableCup = TempData["ReusableCup"];
            ViewBag.PaymentMethod = TempData["PaymentMethod"];
            return View(total);
        }
    }
}


