namespace CafeApp.Models
{
    public class MenuItem
    {
            public int Id { get; set; }
            public string ?Name { get; set; }
            public string ?Category { get; set; } // e.g., Coffee, Food
            public decimal Price { get; set; }
            public string Description { get; set; }
            public decimal DiscountPercent { get; set; } // e.g., 10 for 10%
            public string ImagePath { get; set; }
            public bool IsTrending { get; set; } // New
            public bool IsEcoFriendly { get; set; }
            public decimal DiscountedPrice => Price - (Price * DiscountPercent / 100);

    }


}

