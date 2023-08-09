using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Product> products = _unitOfWork.Product.GetAll(incluedeProperties:"Category").ToList();
            return View(products);
        }
        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.Product.Get(u => u.Id == productId, incluedeProperties: "Category");
            ShoppingCart cart = new ShoppingCart();
            cart.Product = product;
            cart.Count = 1;
            cart.ProductId = productId;
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart cart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            cart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.ApplicationUserId == cart.ApplicationUserId &&
            u.ProductId == cart.ProductId);
            if(cartFromDb!= null)
            {
                //Update cart exist
                cartFromDb.Count += cart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
                _unitOfWork.Save();

            }
            else
            {
                //Add new cart to Db
                _unitOfWork.ShoppingCart.Add(cart);
                _unitOfWork.Save();

            }

            return RedirectToAction("Index");

        }

    }
}
