using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        public void Update(ApplicationUser applicationUser);
    }
}
