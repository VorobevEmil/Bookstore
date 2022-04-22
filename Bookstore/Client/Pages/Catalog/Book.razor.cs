using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Bookstore.Client.Pages.Catalog
{
    public partial class Book
    {
        [Inject] private IHttpClientFactory? HttpClientFactory { get; set; }
        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }

        [Parameter]
        public int Id { get; set; }

        private List<FeedbackModel>? Feedbacks { get; set; }
        public BookModel? BookModel { get; set; }
        private HttpClient? _http;
        private ClaimsPrincipal? _user;

        protected override async Task OnInitializedAsync()
        {
            _user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;

            if (_user.Identity.IsAuthenticated)
            {
                _http = HttpClientFactory.CreateClient("Bookstore.ServerAPI");

            }
            else
            {
                _http = HttpClientFactory.CreateClient("Bookstore.AnonymousAPI");
            }

            Feedbacks = await _http.GetFromJsonAsync<List<FeedbackModel>>($"api/Feedback/GetFeedbacksByBookId/{Id}");
            BookModel = await _http.GetFromJsonAsync<BookModel>($"api/Book/GetById/{Id}");
        }
        private async Task AddBookToCartAsync(int bookId, string? bookTitle)
        {
            if (_user.Identity.IsAuthenticated)
            {
                var userId = _user.Claims.First(t => t.Type == "sub").Value;
                CartModel cartModel = new CartModel() { BookId = bookId, UserId = userId };

                await _http.PostAsJsonAsync("api/Cart/Save", cartModel);

                Snackbar.Configuration.PreventDuplicates = false;
                Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopEnd;
                Snackbar.Add($"Книга {bookTitle} добавлена в корзину", Severity.Info, configure: (configure =>
                {
                    configure.Icon = Icons.Filled.Bookmark;
                    configure.Onclick = (snackbar) =>
                    {
                        NavigationManager.NavigateTo("/personal/cart");
                        return Task.CompletedTask;
                    };
                }));
            }
        }

        public static string DeclinationEnding(int count)
        {
            if ((count % 100 > 10 && count % 100 < 20) || (count % 10 >= 5 && count % 10 <= 9) || count % 10 == 0) return "Отзывов";
            else if (count % 10 == 1) return "Отзыв";
            return "Отзыва";
        }
    }
}
