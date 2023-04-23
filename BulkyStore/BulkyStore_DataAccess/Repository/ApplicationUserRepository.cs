using BulkyStore_DataAccess.Data;
using BulkyStore_DataAccess.Repository.IRepository;
using BulkyStore_Models.Models;

namespace BulkyStore_DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private AppDbContext _db;
        public ApplicationUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(ApplicationUser applicationUser)
        {
            _db.ApplicationUsers.Update(applicationUser);
        }
    }
}
