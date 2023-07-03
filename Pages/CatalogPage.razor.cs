using BlazorBookShop.Models;
using System;

namespace BlazorBookShop.Pages
{
    public partial class CatalogPage
    {
        private List<Product>? products;

        protected override async Task OnInitializedAsync()
        {
            products = await Catalog.GetProductsAsync();
        }
        private void NavigateToProductPage(Guid productId)
		{
            NavigationManager.NavigateTo($"/products/{productId.ToString()}");
		}
    }
}
