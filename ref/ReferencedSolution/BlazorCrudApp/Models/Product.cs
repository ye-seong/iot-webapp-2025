﻿namespace BlazorCrudApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;    
        public double Price { get; set; }
        public int Qty { get; set; }
    }
}
