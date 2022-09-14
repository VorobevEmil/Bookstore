using Bookstore.Server.Service;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class PublishingHouseController : BaseEntityController<PublishingHouseModel>
    {
        public PublishingHouseController(PublishingHouseService service) : base(service) { }
    }
}
