using Bookstore.Client.UIHelper;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Bookstore.Client.Pages
{
    public partial class Index
    {
#nullable disable
        [Inject] private IHttpClientFactory HttpClientFactory { get; set; }
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISnackbar Snackbar { get; set; }
        private HttpClient HttpClient { get; set; }

        private List<BookModel> Books { get; set; } = new List<BookModel>();
        private ClaimsPrincipal _user;
#nullable enable
        private int _pageNumber;
        private bool _disableLoadMoreBooks;
        private Func<Task<List<BookModel>>> GetBooksFromApi;

        protected override async Task OnInitializedAsync()
        {
            _user = await GetCurrentUser();

            HttpClient = CreateHttpClientDependingOnUserAuthorization();

            await FilterBooksByCategory(null);

        }

        private async Task<ClaimsPrincipal> GetCurrentUser()
        {
            return (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User;
        }

        private HttpClient CreateHttpClientDependingOnUserAuthorization()
        {
            return _user.Identity.IsAuthenticated ?
                HttpClientFactory.CreateClient("Bookstore.ServerAPI") :
                HttpClientFactory.CreateClient("Bookstore.AnonymousAPI");
        }

        private async Task SetValueToEntitiesFromApi()
        {
            var books = await GetBooksFromApi();
            if (books.Count < 12)
                _disableLoadMoreBooks = true;

            Books.AddRange(books);
            _pageNumber++;
        }


        private string? categoryTitle;
        private async Task FilterBooksByCategory(TreeItemData treeItemData)
        {
            _pageNumber = 0;
            _disableLoadMoreBooks = false;
            Books = new List<BookModel>();

            categoryTitle = treeItemData?.Title ?? null;
            if (treeItemData != null)
            {
                GetBooksFromApi = (async () => await HttpClient.GetFromJsonAsync<List<BookModel>>($"api/Book/GetBooksByCatalogId/{treeItemData.Id}?page={_pageNumber}&sizepage=12"));
            }
            else
            {
                GetBooksFromApi = (async () => await HttpClient.GetFromJsonAsync<List<BookModel>>($"api/Book/GetBooksOnOnePage?page={_pageNumber}&sizepage=12"));
            }
            await SetValueToEntitiesFromApi();
        }

        private async Task AddBookToCartAsync(int bookId, string? bookTitle)
        {
            if (!_user.Identity.IsAuthenticated)
                return;

            var userId = _user.Claims.First(t => t.Type == "sub").Value;
            CartModel cartModel = new CartModel() { BookId = bookId, UserId = userId };

            await HttpClient.PostAsJsonAsync("api/Cart/Save", cartModel);

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
}
