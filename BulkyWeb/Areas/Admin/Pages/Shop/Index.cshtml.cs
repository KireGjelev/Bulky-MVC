using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWeb.Areas.Admin.Pages.Shop
{
    public class IndexModel : PageModel
    {
       
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<Bulky.Models.Shop> Shops { get; set; }


        public IActionResult OnGet()
        {
            // tuka da gi zemime od bazata
            // da gi predadime na pejdzot
            Shops = (_unitOfWork.Shop.GetAll()).ToList();
            return Page();
        }
    }
}
