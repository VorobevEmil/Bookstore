using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Bookstore.Client.Pages.Catalog
{
    public partial class Author
    {
        [Inject] private IHttpClientFactory? HttpClientFactory { get; set; }

        [Parameter]
        public int Id { get; set; }

        public AuthorModel? AuthorModel { get; set; }

        private HttpClient? _http;

        protected override async Task OnInitializedAsync()
        {
            _http = HttpClientFactory.CreateClient("Bookstore.AnonymousAPI");
            AuthorModel = await _http.GetFromJsonAsync<AuthorModel>($"api/Author/GetById/{Id}");
        }

    }
}
