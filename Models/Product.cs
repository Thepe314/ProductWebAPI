using System;

namespace IMS.PRODUCTAPI
{
    public class Product
    {
        //Attributes of a Product
        public int ProductId {get; set;}

        //required to it so that you need to input these values
        public required string Name {get;set;}

        //make nullable property meaning that you can set it as empty
        public string? Description {get;set;}

        public required decimal Price {get;set;}

        public required int Stock {get;set;}
    }
}