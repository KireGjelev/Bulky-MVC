using System.Collections.Generic;
using System.Linq;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Admin.Pages.Category
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Bulky.Models.Category> Categories { get; set; }

        public IActionResult OnGet()
        {
            Categories = _unitOfWork.Category.GetAll().ToList();
            return Page();
        }
    }
}
