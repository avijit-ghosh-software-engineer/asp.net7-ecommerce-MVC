using BulkyStore_DataAccess.Data;
using BulkyStore_DataAccess.Repository.IRepository;
using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private AppDbContext _db;
        public ProductImageRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(ProductImage obj)
        {
            //_db.ProductImages.Update(obj);
        }
    }
}
