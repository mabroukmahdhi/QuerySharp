// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2025. All rights reserved.
// ---------------------------------------------------------------

using QuerySharp.Tests.Manual.Models.Products;

namespace QuerySharp.Tests.Manual
{
    class Program
    {
        static void Main(string[] args)
        {
            string query = QueryBuilder<Product>.Start()
                .Filter(p => p.Price > 50 && p.Name.Contains("Laptop") && p.Image.Format == "png")
                .Expand(p => p.Reviews.Where(r => r.Rating > 4))
                .Build();

            Console.WriteLine(query);
        }
    }
}