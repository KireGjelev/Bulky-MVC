using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
	public class ShopRepository : Repository<Shop>, IShopRepository
    {
		private ApplicationDbContext _db;
		public ShopRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

        public void Update(Shop obj)
        {
			var objFromDb = _db.Shops.FirstOrDefault(u => u.Id == obj.Id);
			if (objFromDb != null)
			{
				objFromDb.Name = obj.Name;
				objFromDb.Location = obj.Location;
			}
        }
    }
}
