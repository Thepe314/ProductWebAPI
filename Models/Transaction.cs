using System;


namespace IMS.PRODUCTAPI
{
    public class Transaction
    {

        //Attributes of a Transactions
        public int Transactionid {get; set;} //Primary key of transaction
        public int ProductId { get; set; }  // Foreign key referencing the Product

        public string Type { get; set; }  // Type of transaction, e.g., "Restocking" or "Selling"

        public int Quantity { get; set; }  // Number of items involved in the transaction

        public decimal TotalAmount { get; set; } // Total amount = Quantity * Product Price

        public DateTime CreatedAt { get; set; } = DateTime.Now; // Timestamp when the transaction occurred

        // Nullable (?) means the Product may not be loaded yet or might be optional
        // Use 'required' instead if this must always have a value
        public Product? Product { get; set; }  // Reference to the related Product object

      
}
}