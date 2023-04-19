using BulkyStore_DataAccess.Data;
using BulkyStore_Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyStore_UI_Razor.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(AppDbContext context)
        {
            _context = context;

        }
        public void OnGet(int id)
        {
            Category = _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Category.Name.ToLower() == Category.DisplayOrder.ToString().ToLower())
                {
                    ModelState.AddModelError("Category.Name", "Name and Display order not be equal.");
                    return Page();
                }
                var isExists = _context.Categories.
                    FirstOrDefault(x => x.Name.ToLower() == Category.Name.ToLower() && x.Id != Category.Id);
                if (isExists != null)
                {
                    ModelState.AddModelError("Category.Name", "Name must be unique.");
                    return Page();
                }
                _context.Categories.Update(Category);
                _context.SaveChanges();
                TempData["success"] = "Category update successfully";
                return RedirectToPage("/Categories/Index");
            }
            return Page();
        }
    }
}
