using BlazorBookShop.Models;



namespace BlazorBookShop.Pages
{
	public partial class CatalogPage
    {
        private Product[] products;

		protected override async Task OnInitializedAsync()
		{
			products = await BookShopClient.GetProductsAsync();
			StateHasChanged();
		}
		void NavigateToProductPage(Guid id)
	    {
            NavigationManager.NavigateTo($"/products/{id.ToString()}");
	    }

    }
}
