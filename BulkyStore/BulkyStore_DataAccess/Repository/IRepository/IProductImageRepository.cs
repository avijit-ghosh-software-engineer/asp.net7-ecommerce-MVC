using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository.IRepository
{
    public interface IProductImageRepository : IRepository<ProductImage>
    {
        void Update(ProductImage obj);
    }
}
