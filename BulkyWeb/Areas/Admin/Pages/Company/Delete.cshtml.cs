using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Admin.Pages.Company
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Bulky.Models.Company CompanyObj { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompanyObj = _unitOfWork.Company.Get(u => u.Id == id);

            if (CompanyObj == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CompanyObj = _unitOfWork.Company.Get(u => u.Id == id);

            if (CompanyObj != null)
            {
                _unitOfWork.Company.Remove(CompanyObj);
                _unitOfWork.Save();
                TempData["success"] = "Company deleted successfully";
                return RedirectToPage("Index");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
