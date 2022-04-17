using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookstore.Client.Pages.Personal
{
    public partial class Cart
    {
        [Inject] private HttpClient? Http { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private IDialogService? DialogService { get; set; }
        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }

        public List<CartModel>? UserCarts { get; set; }
        public List<CartModel> SelectedCart { get; set; } = new List<CartModel>();
        protected override async Task OnInitializedAsync()
        {
            UserCarts = await Http.GetFromJsonAsync<List<CartModel>>("api/Cart/GetAll");
        }



        private void NavigateToCatalog()
        {
            NavigationManager.NavigateTo("/");
        }

        private async Task DeleteBookFromCart(CartModel cart)
        {
            await Http.PostAsJsonAsync("api/Cart/Delete", cart);
            UserCarts.Remove(cart);
        }

        private async Task DeleteAllBooksFromCart()
        {

            List<CartModel> currentCart;
            List<CartModel> containInCart = null;
            if (SelectedCart.Count == default)
            {
                currentCart = UserCarts;
            }
            else
            {
                currentCart = SelectedCart;
                containInCart = UserCarts.Where(t => !SelectedCart.Contains(t)).ToList();
            }

            foreach (var cart in currentCart)
            {
                await Http.PostAsJsonAsync("api/Cart/Delete", cart);
            }
            UserCarts.Clear();
            SelectedCart.Clear();
            StateHasChanged();
            if (containInCart != null)
            {
                UserCarts = containInCart;
            }
            containInCart = null;
        }

        private async Task<bool?> ShowMessageDeleteBooks()
        {
            bool? result = await DialogService.ShowMessageBox(
                "Внимание",
                "Вы действительно хотите удалить " + (SelectedCart.Count == default ? "все" : "выбранные") + " книги!",
                yesText: "Удалить!", cancelText: "Отмена");
            return result;

        }

        private async Task CreateOrder()
        {
            var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
            var selectedBooks = SelectedCart.Select(t => t.Book).ToList();

            OrderModel order = new OrderModel();
            order.UserId = user.Claims.First(t => t.Type == "sub").Value;
            order.OrderDate = DateTime.Now;

            HttpResponseMessage httpResponseMessage = await Http.PostAsJsonAsync("api/Order/Save", order);
            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                order = await httpResponseMessage.Content.ReadFromJsonAsync<OrderModel>();

                foreach (var book in selectedBooks)
                {
                    await Http.PostAsJsonAsync("api/BookOrder/Save", new BookModelOrderModel() { OrdersId = order.Id, BooksId = book.Id });
                }

                await DeleteAllBooksFromCart();
            }


        }

        private void SelectCart(bool check, CartModel cart)
        {
            if (check)
                SelectedCart.Add(cart);
            else
                SelectedCart.Remove(cart);
        }

    }
}
