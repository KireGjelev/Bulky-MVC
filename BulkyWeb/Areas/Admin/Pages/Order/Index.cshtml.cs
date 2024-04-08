using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Identity; 


namespace BulkyWeb.Areas.Admin.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;


        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public IndexModel(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            return Page();
        }

        //public IActionResult OnGetAllOrders(string status)
        //{

        //    IEnumerable<OrderHeader> objOrderHeaders;
        //    var user = _userManager.GetUserAsync(User);

        //    if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
        //    {
        //        objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
        //    }
        //    else
        //    {
        //        objOrderHeaders = _unitOfWork.OrderHeader
        //            .GetAll(u => u.ApplicationUserId == user.Id, includeProperties: "ApplicationUser");
        //    }

        //    switch (status)
        //    {
        //        case "pending":
        //            objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus == SD.PaymentStatusDelayedPayment);
        //            break;
        //        case "inprocess":
        //            objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
        //            break;
        //        case "completed":
        //            objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusShipped);
        //            break;
        //        case "approved":
        //            objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusApproved);
        //            break;
        //        default:
        //            break;
        //    }

        //    return new JsonResult(new { data = objOrderHeaders });
        //}
    }
}
