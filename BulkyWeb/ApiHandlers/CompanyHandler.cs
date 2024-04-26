using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BulkyWeb.ApiHandlers
{
    [Route("api/company")]
    [ApiController]
    public class CompanyHandler : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return new JsonResult(new { data = objCompanyList });
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return new JsonResult(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return new JsonResult(new { success = true, message = "Delete Successful" });
        }
    }

}
