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
        [Inject] private IHttpClientFactory? HttpClientFactory { get; set; }
        [Inject] private AuthenticationStateProvider? AuthenticationStateProvider { get; set; }
        [Inject] private NavigationManager? NavigationManager { get; set; }
        [Inject] private ISnackbar? Snackbar { get; set; }
        private HttpClient? HttpClient { get; set; }

        private List<BookModel>? Books { get; set; }
        private List<BookModel>? SelectedBooks { get; set; }
        private List<CatalogModel>? Catalogs { get; set; }

        private ClaimsPrincipal? _user;

        private List<int> _childCatalogId = new List<int>();

        protected override async Task OnInitializedAsync()
        {
            _user = await GetCurrentUser();

            HttpClient = CreateHttpClientDependingOnUserAuthorization();

            await SetValueToEntitiesFromApi();

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
            Catalogs = await GetCatalogsFromApi();
            Books = await GetBooksFromApi();
            SelectedBooks = Books;
        }

        private async Task<List<CatalogModel>> GetCatalogsFromApi()
        {
            return await HttpClient.GetFromJsonAsync<List<CatalogModel>>("api/Catalog/GetAll");
        }

        private async Task<List<BookModel>> GetBooksFromApi()
        {
            return await HttpClient.GetFromJsonAsync<List<BookModel>>("api/Book/GetAll");
        }


        private string? categoryTitle;
        private void FilterBooksByCategory(TreeItemData treeItemData)
        {
            categoryTitle = treeItemData?.Title ?? null;
            if (treeItemData != null)
            {
                _childCatalogId.Clear();
                AddChildCatalog(treeItemData);
                SelectedBooks = Books.Where(t => t.CatalogId == treeItemData.Id || _childCatalogId.Contains((int)t.CatalogId)).ToList();
            }
            else
            {
                SelectedBooks = Books;
            }
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


        private void AddChildCatalog(TreeItemData treeItemData)
        {
            if (treeItemData.TreeItems == null)
                return;

            foreach (var item in treeItemData.TreeItems)
            {
                _childCatalogId.Add(item.Id);
                AddChildCatalog(item);

            }
        }
    }
}
