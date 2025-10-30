// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

namespace QuerySharp.Tests.Manual.Models.Products
{
    internal class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Category { get; set; }

        public int Stock { get; set; }

        public ImageData Image { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
