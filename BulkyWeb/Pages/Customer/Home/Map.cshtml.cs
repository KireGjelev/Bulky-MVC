using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace BulkyWeb.Pages.Customer.Home
{
    public class MapModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public MapModel(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

        public IList<Shop> Shops { get; set; }


        public IActionResult OnGet()
        {
            // tuka da gi zemime od bazata
            // da gi predadime na pejdzot
            Shops = (_unitOfWork.Shop.GetAll()).ToList();
            return Page();
        }
    }
}