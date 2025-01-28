// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi 2024. All rights reserved.
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
            // Output: "$filter=Price gt 50 and contains(Name, 'Laptop') and Image/Format eq 'png'&$expand=Reviews($filter=Rating gt 4)"
            //$filter=(((Price gt 50) and contains(Name,'Laptop')) and (Image/Format eq 'png'))&$expand=(Reviews($filter=(Rating gt 4))
        }
    }
}