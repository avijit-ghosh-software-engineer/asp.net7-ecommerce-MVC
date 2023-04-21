using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
