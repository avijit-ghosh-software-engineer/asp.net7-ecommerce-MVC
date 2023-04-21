using BulkyStore_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyStore_Utility.GetLists
{
    public static class CategoryList
    {
        public static IEnumerable<SelectListItem> GetCategoryList(IUnitOfWork unitOfWork, int? categoryId = 0)
        {
            IEnumerable<SelectListItem> data = null;
            if (categoryId == 0)
            {
                data = unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            }
            else
            {
                data = unitOfWork.Category.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = (x.Id == categoryId ? true : false)
                });
            }

            return data;
        }
    }
}
