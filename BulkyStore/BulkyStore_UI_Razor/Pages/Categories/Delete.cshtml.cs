using BulkyStore_UI_Razor.Data;
using BulkyStore_UI_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyStore_UI_Razor.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;
        [BindProperty]
        public Category Category { get; set; }
        public DeleteModel(AppDbContext context)
        {
            _context = context;

        }
        public void OnGet(int id)
        {
            Category = _context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public IActionResult OnPost()
        {
            _context.Categories.Remove(Category);
            _context.SaveChanges();
            TempData["success"] = "Category delete successfully";
            return RedirectToPage("/Categories/Index");
        }
    }
}
