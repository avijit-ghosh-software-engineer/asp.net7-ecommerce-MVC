using BulkyStore_DataAccess.Data;
using BulkyStore_DataAccess.Repository.IRepository;
using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private AppDbContext _db;
        public OrderDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
