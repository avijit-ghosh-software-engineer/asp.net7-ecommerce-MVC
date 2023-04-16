using BulkyStore_UI_Razor.Data;
using BulkyStore_UI_Razor.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyStore_UI_Razor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<Category> Categories { get; set; }
        public IndexModel(AppDbContext context)
        {
            _context = context;

        }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
        }

    }
}
