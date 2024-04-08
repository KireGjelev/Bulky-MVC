using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Admin.Pages.Company
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Bulky.Models.Company CompanyObj { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || id == 0)
            {
                CompanyObj = new Bulky.Models.Company(); // Create a new company
            }
            else
            {
                // Fetch company details for editing
                CompanyObj =  _unitOfWork.Company.Get(u => u.Id == id);
                if (CompanyObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CompanyObj.Id == 0)
            {
                // Create new company
                _unitOfWork.Company.Add(CompanyObj);
            }
            else
            {
                // Update existing company
                _unitOfWork.Company.Update(CompanyObj);
            }

            _unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToPage("Index");
        }
    }
}
