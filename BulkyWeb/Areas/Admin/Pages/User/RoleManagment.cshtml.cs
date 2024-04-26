using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Bulky.Models.ViewModels;
using Bulky.Models;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Pages.User
{
    public class RoleManagementModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public RoleManagementModel(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public RoleManagmentVM RoleVM { get; set; }

        public IActionResult OnGet(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }

            RoleVM = new RoleManagmentVM
            {
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId, includeProperties: "Company"),
                RoleList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            RoleVM.ApplicationUser.Role = _userManager.GetRolesAsync(RoleVM.ApplicationUser).Result.FirstOrDefault();

            return Page();
        }

        public IActionResult OnPost()
        {
            var applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == RoleVM.ApplicationUser.Id);

            if (applicationUser == null)
            {
                return NotFound();
            }

            var oldRole = _userManager.GetRolesAsync(applicationUser).Result.FirstOrDefault();

            if (RoleVM.ApplicationUser.Role != oldRole)
            {
                if (RoleVM.ApplicationUser.Role == SD.Role_Company)
                {
                    applicationUser.CompanyId = RoleVM.ApplicationUser.CompanyId;
                }
                else if (oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }

                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).Wait();
                _userManager.AddToRoleAsync(applicationUser, RoleVM.ApplicationUser.Role).Wait();
            }
            else
            {
                if (oldRole == SD.Role_Company && applicationUser.CompanyId != RoleVM.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = RoleVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
