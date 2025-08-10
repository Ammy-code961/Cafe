namespace CafeApp.Models
{
    public class CartItem
    {
        public MenuItem Item { get; set; }
        public int Quantity { get; set; }

        public decimal TotalPrice =>
            (Item.Price - Item.Price * Item.DiscountPercent / 100) * Quantity;
    }
}
  

