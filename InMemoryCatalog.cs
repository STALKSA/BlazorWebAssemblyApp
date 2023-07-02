using BlazorBookShop.Models;
using System.Collections.Concurrent;

namespace BlazorBookShop
{
    public class InMemoryCatalog : ICatalog
    {
        private ConcurrentDictionary<Guid, Product> _products = GenerateProducts(10);

        public async Task<List<Product>> GetProductsAsync()
        {
            return _products.Values.ToList();
        }


        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            if (!_products.TryGetValue(productId, out var product))
            {
                throw new KeyNotFoundException($"Id {productId} не найден.");
            }
            else
            {
                return product;
            }
        }


        public Task AddProduct(Product product)
        {
            if (!_products.TryAdd(product.Id, product))
            {
                throw new ArgumentException($"Товар {product.Id} уже существует.");
            };
            return Task.CompletedTask;

        }


        public Task DeleteProductById(Guid productId)
        {
            if (!_products.TryRemove(productId, out _))
            {
                throw new InvalidOperationException($"Товар {productId} не найден");
            }
            return Task.CompletedTask;

        }


        public Task ClearProducts()
        {
            _products.Clear();
            return Task.CompletedTask;

        }


        public Task UpdateProductById(Guid productId, Product newProduct)
        {
            if (!_products.TryGetValue(productId, out var oldProductValue))
            {
                throw new KeyNotFoundException($" ID {newProduct.Id} не найден.");
            }
            if (!_products.TryUpdate(productId, newProduct, oldProductValue))
            {
                throw new InvalidCastException($"Операция прервана...");
            }

            return Task.CompletedTask;
        }


        static ConcurrentDictionary<Guid, Product> GenerateProducts(int count)
        {
            var random = new Random();
            var products = new ConcurrentDictionary<Guid, Product>();

            var productNames = new string[]
            {
            "Cracking the Coding Interview",
            "Code Complete",
            "Clean Code",
            "Refactoring",
            "Head First Design Patterns",
            "Patterns of Enterprise Application Architecture",
            "Working Effectively with Legacy Code",
            "The Clean Coder",
            "Introduction to Algorithms",
            "The Pragmatic Programmer"
            };



            for (int i = 0; i < count; i++)
            {
                var name = productNames[i];
                var price = random.Next(50, 2000);
                var producedAt = DateTime.Now.AddDays(-random.Next(1, 30));
                var expiredAt = producedAt.AddDays(random.Next(1, 365));
                var stock = random.NextDouble() * 100;

                var product = new Product(name, price);
                product.Id = i == 0 ? Guid.Empty : Guid.NewGuid();
                product.Description = "Описание" + name;
                product.ProducedAt = producedAt;
                product.ExpiredAt = expiredAt;
                product.Stock = stock;
                products.TryAdd(product.Id, product);
            }

            return products;
        }

    }
}

