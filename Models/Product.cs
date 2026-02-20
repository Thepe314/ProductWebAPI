using System;

namespace IMS.PRODUCTAPI
{

      // Represents a product in the inventory
    public class Product
    {
        //Attributes of a Product

        //Primary key of product
        public int ProductId {get; set;}    

        // the name of the product
        // 'required' ensures a value must be provided when creating a Product
        public required string Name {get;set;} 

        // Description of the product
        // Nullable property (can be left empty)
        public string? Description {get;set;} 

        // Price of a single unit of the product
        // Required value
        public required decimal Price {get;set;} 

        // Current stock of the product
        // Required value
        public required int Stock {get;set;} 

        // list of transactions associated with this product
        // Initialized with an empty list to avoid null references
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>(); 

    }
}