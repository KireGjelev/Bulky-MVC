using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IShopRepository : IRepository<Shop>
    {
        void Update(Shop obj);
    }
}
