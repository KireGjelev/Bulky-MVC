using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace BulkyWeb.Pages.Customer.Home
{
    //[Authorize] // Ensure that only authenticated users can access this page
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty] // Allows binding from query string
        public int ProductId { get; set; } // Bind product id from query string

        [BindProperty]
        public ShoppingCart Cart { get; set; }

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int id)
        {
            Cart = new ShoppingCart
            {
                Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "Category,ProductImages"),
                Count = 1,
                ProductId = id
            };


            if (Cart.Product == null)
            {
                // Handle case when product is not found
                return RedirectToPage("/NotFound"); // Redirect to a custom not found page
            }

            return Page();
        }
        public IActionResult OnPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Cart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == Cart.ProductId);

            if (cartFromDb != null)
            {
                cartFromDb.Count += Cart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(Cart);
            }

            _unitOfWork.Save();

            TempData["success"] = "Cart updated successfully";

            return RedirectToPage("/Cart/Index");
        }
    }
}
