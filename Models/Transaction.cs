using System;

namespace IMS.PRODUCTAPI
{
    public class Transaction
    {
        //Attributes of a Product
        public int TransactionId {get; set;}
        public int ProductId { get; set; }

        public string Type { get; set; } = "";

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; }
}
}