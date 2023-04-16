using BulkyStore_UI_Razor.Data;
using BulkyStore_UI_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyStore_UI_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public CreateModel(AppDbContext context)
        {
            _context = context;

        }
        public void OnGet()
        {
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
                    FirstOrDefault(x => x.Name.ToLower() == Category.Name.ToLower());
                if (isExists != null)
                {
                    ModelState.AddModelError("Category.Name", "Name must be unique.");
                    return Page();
                }
                _context.Categories.Add(Category);
                _context.SaveChanges();
                TempData["success"] = "Category create successfully";
                return RedirectToPage("/Categories/Index");
            }
            return Page();
        }
    }
}
