using Microsoft.AspNetCore.Mvc.RazorPages;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.ViewModels;
using System.Collections.Generic;
using Bulky.Models;

namespace BulkyWeb.Areas.Admin.Pages.User
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ApplicationUser> Users { get; set; }

        public void OnGet()
        {
            Users = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company");
        }
    }
}
