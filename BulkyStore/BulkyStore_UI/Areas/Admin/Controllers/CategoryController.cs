using BulkyStore_DataAccess.Repository.IRepository;
using BulkyStore_Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyStore_UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            List<Category> data = _unitOfWork.Category.GetAll().ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Name.ToLower() == category.DisplayOrder.ToString().ToLower())
                {
                    ModelState.AddModelError("Name", "Name and Display order not be equal.");
                    return View(category);
                }
                var isExists = _unitOfWork.Category.Get(x => x.Name.ToLower() == category.Name.ToLower());
                if (isExists != null)
                {
                    ModelState.AddModelError("Name", "Name must be unique.");
                    return View(category);
                }
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category create successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.Name.ToLower() == category.DisplayOrder.ToString().ToLower())
                {
                    ModelState.AddModelError("Name", "Name and Display order not be equal.");
                    return View(category);
                }
                var isExists = _unitOfWork.Category.Get(x => x.Name.ToLower() == category.Name.ToLower() && x.Id != category.Id);
                if (isExists != null)
                {
                    ModelState.AddModelError("Name", "Name must be unique.");
                    return View(category);
                }
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                TempData["success"] = "Category update successfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.Get(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var obj = _unitOfWork.Category.Get(c => c.Id == id);
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category delete successfully";
            return RedirectToAction("Index");
        }
    }
}
