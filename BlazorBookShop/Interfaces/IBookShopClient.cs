using BlazorBookShop.Models;

namespace BlazorBookShop.Interfaces
{
    public interface IBookShopClient
    {
		Task<Product[]> GetProductsAsync();
        Task<Product> GetProduct(Guid id);
		Task AddProduct(Product product);
		Task UpdateProduct(Guid id, Product product);
		Task DeleteProduct(Guid id);

	}
}