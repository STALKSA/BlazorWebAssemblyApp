using BlazorBookShop.Models;

namespace BlazorBookShop
{
    public interface IBookShopClient
    {
        Task AddProduct(Product product);
        Task<Product> GetProduct(Guid id);
    }
}