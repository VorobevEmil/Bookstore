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

        public List<BookModel>? Books { get; set; }
        public List<BookModel>? SelectedBooks { get; set; }
        public List<CatalogModel>? Catalogs { get; set; }
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

            Catalogs = await _http.GetFromJsonAsync<List<CatalogModel>>("api/Catalog/GetAll");
            Books = await _http.GetFromJsonAsync<List<BookModel>>("api/Book/GetAll");
            SelectedBooks = Books;

        }


        private string? categoryTitle;
        private void FilterBooksByCategory(TreeItemData treeItemData)
        {
            categoryTitle = treeItemData?.Title ?? null;
            if (treeItemData != null)
            {
                childCatalogId.Clear();
                AddChildCatalog(treeItemData);
                SelectedBooks = Books.Where(t => t.CatalogId == treeItemData.Id || childCatalogId.Contains((int)t.CatalogId)).ToList();
            }
            else
            {
                SelectedBooks = Books;
            }
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

        private List<int> childCatalogId = new List<int>();
        private void AddChildCatalog(TreeItemData treeItemData)
        {
            if (treeItemData.TreeItems == null)
                return;

            foreach (var item in treeItemData.TreeItems)
            {
                childCatalogId.Add(item.Id);
                AddChildCatalog(item);

            }
        }
    }
}
