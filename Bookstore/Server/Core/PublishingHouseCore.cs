using Bookstore.Server.Data;
using Bookstore.Shared.DbModels;

namespace Bookstore.Server.Core
{
    public class PublishingHouseCore : BaseCore<PublishingHouseModel>
    {
        public PublishingHouseCore(AppDbContext context) : base(context) { }
    }
}
