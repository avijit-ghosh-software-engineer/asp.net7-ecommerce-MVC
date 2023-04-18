using BulkyStore_DataAccess.Data;
using BulkyStore_Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyStore_UI.Controllers
{
	public class CategoryController : Controller
	{
		private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
			_context = context;

		}
        public IActionResult Index()
		{
			List<Category> data =  _context.Categories.ToList();
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
				if (category.Name.ToLower()==category.DisplayOrder.ToString().ToLower())
				{
					ModelState.AddModelError("Name", "Name and Display order not be equal.");
					return View(category);
				}
				var isExists = _context.Categories.
					FirstOrDefault(x=>x.Name.ToLower()==category.Name.ToLower());
				if (isExists!=null)
				{
                    ModelState.AddModelError("Name", "Name must be unique.");
                    return View(category);
                }
				_context.Categories.Add(category);
				_context.SaveChanges();
				TempData["success"] = "Category create successfully";
				return RedirectToAction("Index");
			}
			return View(category);
		}

		public IActionResult Edit(int? id)
		{
			if (id==null || id==0)
			{
				return NotFound();
			}
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
			if (category==null)
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
                var isExists = _context.Categories.
                    FirstOrDefault(x => x.Name.ToLower() == category.Name.ToLower() && x.Id != category.Id);
                if (isExists != null)
                {
                    ModelState.AddModelError("Name", "Name must be unique.");
                    return View(category);
                }
                _context.Categories.Update(category);
				_context.SaveChanges();
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
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
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
			var obj = _context.Categories.FirstOrDefault(c => c.Id == id);
			_context.Categories.Remove(obj);
			_context.SaveChanges();
			TempData["success"] = "Category delete successfully";
			return RedirectToAction("Index");
		}
	}
}
