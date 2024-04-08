using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Admin.Pages.Category
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Bulky.Models.Category Category { get; set; }

        public IActionResult OnGet(int id)
        {
            Category = _unitOfWork.Category.Get(u => u.Id == id);
            if (Category == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _unitOfWork.Category.Update(Category);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToPage("./Index");
        }
    }
}
