# QuerySharp

QuerySharp is a modern, fluent library for building OData query strings in C#. It provides an intuitive, type-safe API that turns LINQ expression trees into valid OData query parameters, letting you compose filters, sorting, paging, and expansions without string concatenation.

- Target frameworks: `netstandard2.0` (usable from .NET Framework4.6.1+ and modern .NET including .NET9)
- Language level: Compatible with C# expression trees
- Package dependencies: `Microsoft.Extensions.DependencyInjection`, `Xeption`
- Zero setup: `QueryBuilder<T>` wires its own minimal services internally; no DI registration needed.


## Why QuerySharp

- Fluent, strongly-typed API powered by expression trees
- Concise and readable query composition
- Safe OData operator and function translation (no manual strings)
- Composable building blocks for filters, ordering, paging, and expand


## Installation

From NuGet (recommended once the package is published):

```
dotnet add package QuerySharp
```

From source (current repository):

- Add a project reference to `QuerySharp/QuerySharp.csproj`, or
- Build and reference the produced package/assembly.


## Quick start

Build an OData query string in a type-safe way:

```csharp
// Define your model
public class Product
{
 public string Name { get; set; }
 public decimal Price { get; set; }
 public string Category { get; set; }
 public int Stock { get; set; }
 public ImageData Image { get; set; }
 public List<Review> Reviews { get; set; }
}

// Compose a query
string query = QueryBuilder<Product>.Start()
 .Filter(p => p.Price >50 && p.Name.Contains("Laptop") && p.Image.Format == "png")
 .OrderBy(p => p.Name)
 .Top(10)
 .Build();

// query => "$filter=Price gt 50 and contains(Name,'Laptop') and Image/Format eq 'png'&$orderby=Name asc&$top=10"
```

Use the resulting query with any HTTP client against an OData endpoint:

```csharp
var uri = new Uri($"https://api.contoso.com/v1/products?{query}");
var response = await httpClient.GetAsync(uri);
```


## Building expansions

Expand navigation properties, including collection filters using LINQ:

```csharp
// Expand a collection with a filter
string expandQuery = QueryBuilder<Product>.Start()
 .Expand(p => p.Reviews.Where(r => r.Rating >4))
 .Build();

// expandQuery => "$expand=Reviews($filter=Rating gt 4)"
```

You can combine expand with other clauses:

```csharp
string full = QueryBuilder<Product>.Start()
 .Filter(p => p.Category == "Laptops")
 .OrderByDescending(p => p.Price)
 .Expand(p => p.Image)
 .Top(5)
 .Build();

// full => "$filter=Category eq 'Laptops'&$expand=Image&$orderby=Price desc&$top=5"
```


## API reference

All APIs are available via the generic `QueryBuilder<T>`.

- `static QueryBuilder<T> Start()`: Starts a new builder instance.

- `QueryBuilder<T> Filter(Expression<Func<T, bool>> predicate)`: Adds a `$filter` clause. Multiple calls are combined with `and`.

- `QueryBuilder<T> OrderBy(Expression<Func<T, object>> keySelector)`: Adds an ascending `$orderby` clause.

- `QueryBuilder<T> OrderByDescending(Expression<Func<T, object>> keySelector)`: Adds a descending `$orderby` clause.

- `QueryBuilder<T> Top(int count)`: Adds a `$top` (limit) clause.

- `QueryBuilder<T> Skip(int count)`: Adds a `$skip` (offset) clause.

- `QueryBuilder<T> Expand<TProperty>(Expression<Func<T, TProperty>> navigationProperty)`: Adds an `$expand` for the given navigation property. Supports nested member access and collection filters (`Where`).

- `string Build()`: Produces the final query string without a leading `?` and with `&` between segments.


## Supported expressions

- Binary comparisons: `==`, `!=`, `>`, `>=`, `<`, `<=`
- Logical operators: `&&` (`and`), `||` (`or`)
- Nested property access: `p.Image.Format`, `p.Parent.Id`
- String functions: `Contains`, `StartsWith`, `EndsWith` mapped to `contains`, `startswith`, `endswith`
- Collection filtering for expansions: `collection.Where(x => x.Prop op value)` translated to `Collection($filter=...)`

Examples:

```csharp
// Comparisons and logical ops
.Filter(p => p.Price >=100 && p.Stock >0)

// Strings
.Filter(p => p.Name.StartsWith("Pro") || p.Name.EndsWith("Max"))

// Nested properties
.Filter(p => p.Image.Format == "png")

// Expand with collection filter
.Expand(p => p.Reviews.Where(r => r.Rating >4))
```


## Error handling

The library validates input expressions and throws well-typed exceptions:

- `ExpressionValidationException`
 - Thrown for general validation errors when translating expressions.
- `NullExpressionException`
 - Thrown when a provided expression is `null`.

Other unsupported operators/methods will result in `NotSupportedException`.


## Notes and limitations

- OData coverage: This release focuses on core `$filter`, `$orderby`, `$top`, `$skip`, and `$expand` scenarios. Advanced OData functions and operators may not yet be supported.
- Method support: Only selected string methods (`Contains`, `StartsWith`, `EndsWith`) and `Enumerable.Where` inside `Expand` are currently supported. Other methods will throw `NotSupportedException`.
- Null handling: Comparing to `null` and certain constant scenarios may not be supported yet.
- GraphQL: Although the vision includes GraphQL, the current implementation targets OData. GraphQL builders are planned.


## Testing

- Unit tests cover expression translation and query assembly under `QuerySharp.Tests.Unit`.
- A manual console sample is available in `QuerySharp.Tests.Manual` demonstrating typical usage:

```csharp
string query = QueryBuilder<Product>.Start()
 .Filter(p => p.Price >50 && p.Name.Contains("Laptop") && p.Image.Format == "png")
 .Expand(p => p.Reviews.Where(r => r.Rating >4))
 .Build();

Console.WriteLine(query);
```


## Contributing

Contributions are welcome. Recommended steps:

- Open an issue describing the change or feature.
- Add unit tests for new behavior under `QuerySharp.Tests.Unit`.
- Keep the public API fluent and type-safe.
- Follow the existing project structure and coding style.


## License

This project is licensed under the MIT License. See `LICENSE` for details.
