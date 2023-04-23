using BulkyStore_DataAccess.Data;
using BulkyStore_DataAccess.Repository.IRepository;
using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private AppDbContext _db;
        public ShoppingCartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCarts.Update(obj);
        }
    }
}
