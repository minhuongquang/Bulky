using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;


        public UserController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ApplicationUser> users = _unitOfWork.ApplicationUser.GetAll().ToList();
            IEnumerable<string> roles;
            foreach(var user in users)
            {
                if(user.Company == null)
                {
                    user.Company = new() { Name = ""};
                }
                roles = await _userManager.GetRolesAsync(user);
                user.Role = roles.FirstOrDefault();
            }
            return Json(new { data = users });
        }
        [HttpPost]
        public IActionResult LockAndUnlockUser(string? id)
        {
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            string m = "";
            if (user == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });

            }
            if(user!=null && user.LockoutEnd > DateTime.Now)
            {
                //user is currently lock and we need unlock
                user.LockoutEnd = DateTime.Now;
                m = "Unlocking";
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
                m = "Locking";
            }
            _unitOfWork.Save();
            return Json(new { success = true, message =  m + " successfully" });
        }
        #endregion
    }
}
