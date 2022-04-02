using Bookstore.Server.Core;
using Bookstore.Shared.DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Server.Controllers
{
    public class PublishingHouseController : BaseEntityController<PublishingHouseModel>
    {
        public PublishingHouseController(PublishingHouseCore core) : base(core) { }
    }
}
