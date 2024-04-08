using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace BulkyWeb.Areas.Admin.Pages.Product
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ProductVM ProductVM { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            ProductVM = new ProductVM
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Bulky.Models.Product()
            };

            if (id != null && id != 0)
            {
                ProductVM.Product = _unitOfWork.Product.Get(u => u.Id == id, includeProperties: "ProductImages");
                if (ProductVM.Product == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost(List<IFormFile> files)
        {
            if (!ModelState.IsValid)
            {
                ProductVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return Page();
            }

            if (ProductVM.Product.Id == 0)
            {
                _unitOfWork.Product.Add(ProductVM.Product);
            }
            else
            {
                _unitOfWork.Product.Update(ProductVM.Product);
            }

            _unitOfWork.Save();

           

            TempData["Success"] = "Product created/updated successfully";
            return RedirectToPage("./Index");
        }
    }
}
