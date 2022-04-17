using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bookstore.Client.Pages.Personal
{
    public partial class Order
    {
        [Inject] private HttpClient? Http { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }

        private List<OrderModel>? _allOrders;
        protected override async Task OnInitializedAsync()
        {
            _allOrders = await Http.GetFromJsonAsync<List<OrderModel>>("api/Order/GetAll");
        }
        private void NavigateToCatalog()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
