using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System.Collections.Generic;

namespace BulkyWeb.Areas.Admin.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Bulky.Models.Product> Products { get; set; }

        public void OnGet()
        {
            Products = _unitOfWork.Product.GetAll(includeProperties: "Category");
        }
    }
}
