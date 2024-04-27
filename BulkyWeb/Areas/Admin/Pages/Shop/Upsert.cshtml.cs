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

namespace BulkyWeb.Areas.Admin.Pages.Shop
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty]
        public ShopVM ShopVM { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int? id)
        {
            ShopVM = new ShopVM
            {
                Shop = new Bulky.Models.Shop()
            };

            if (id != null && id != 0)
            {
                ShopVM.Shop = _unitOfWork.Shop.Get(u => u.Id == id);
                if (ShopVM.Shop == null)
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
                return Page();
            }

            if (ShopVM.Shop.Id == 0)
            {
                _unitOfWork.Shop.Add(ShopVM.Shop);
            }
            else
            {
                _unitOfWork.Shop.Update(ShopVM.Shop);
            }

            _unitOfWork.Save();

            TempData["Success"] = "Shop created/updated successfully";
            return RedirectToPage("./Index");
        }
    }
}
