using Bookstore.Server.Data.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class PublishingHouseController : BaseEntityController<PublishingHouseModel>
    {
        public PublishingHouseController(BookstoreService<PublishingHouseModel> service) : base(service) { }
    }
}
