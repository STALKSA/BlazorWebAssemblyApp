using BlazorBookShop.Models;
using System.Net.Http.Json;

namespace BlazorBookShop
{
    public class BookShopClient : IDisposable, IBookShopClient
    {
        private readonly string _host;
        private readonly HttpClient _httpClient;

        public BookShopClient(string host = "https://bookshop.com/", HttpClient? httpClient = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(host);
            if (!Uri.TryCreate(host, UriKind.Absolute, out var hostUri))
            {
                throw new ArgumentException("Адрес хоста должен быть валидного значения", nameof(host));
            }

            _host = host;
            _httpClient = httpClient ?? new HttpClient();
            if (httpClient.BaseAddress is null)
            {
                _httpClient.BaseAddress = hostUri;
            }
        }

        public void Dispose()
        {
            ((IDisposable)_httpClient).Dispose();
        }

        public async Task<Product> GetProduct(Guid id)
        {
            var productTask = await _httpClient.GetFromJsonAsync<Product>($"get_product?id={id}");
            if (productTask is null)
            {
                throw new InvalidOperationException("Сервер вернул продукт со значением null");
            }
            return productTask!;

        }

        public async Task AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            using var response = await _httpClient.PostAsJsonAsync("add_product", product);
            response.EnsureSuccessStatusCode();
        }
    }
}
