using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Admin.Pages.Shop
{
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult OnGet(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            Bulky.Models.Shop shop = _unitOfWork.Shop.Get(shop => shop.Id == id);

            if (shop == null)
            {
                return NotFound();
            }

            _unitOfWork.Shop.Remove(shop);

            _unitOfWork.Save();

            TempData["Success"] = "Shop deleted successfully";
            return RedirectToPage("./Index");
        }
    }
}
