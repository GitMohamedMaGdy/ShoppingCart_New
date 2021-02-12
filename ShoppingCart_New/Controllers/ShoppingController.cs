using ShoppingCart_New.Data;
using ShoppingCart_New.Models;
using ShoppingCart_New.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_New.Controllers
{
    public class ShoppingController : Controller
    {
        // GET: Shopping
        ShoppingCartDBEntities1 context;
        List<ShoppingCartModel> shoppingCartModels;
        ShoppingLogic _shoppingLogic;
        OrderLogic _OrderLogic;
        public ShoppingController()
        {
            context = new ShoppingCartDBEntities1();
            shoppingCartModels = new List<ShoppingCartModel>();
            _shoppingLogic = new ShoppingLogic();
            _OrderLogic = new OrderLogic();
        }
        [HttpGet]
        public ActionResult Index()
        {

            var shoppingItems = (from item in context.Items
                                 join category in context.Categories
                                 on item.CategoryId equals category.CategoryId
                                 select new ShoppingViewModel()
                                 {
                                     ItemId = item.ItemId,
                                     ItemName = item.ItemName,
                                     ItemPrice = item.ItemPrice,
                                     ImagePath = item.ImagePath,
                                     Description = item.Description,
                                     ItemCode = item.ItemCode,
                                     Category = category.CategoryName
                                 }).ToList();


            return View(shoppingItems);
        }

        [HttpPost]
        public ActionResult AddToCart(string ItemId)
        {
           
            ViewBag.ShoppingCartItems = _shoppingLogic.AddCart(ItemId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ShoppingCart()
        {
            shoppingCartModels = HttpContext.Session["CartItems"] as List<ShoppingCartModel>;
            return View(shoppingCartModels);
        }

        [HttpPost]
        public ActionResult AddOrder()
        {
            bool result =_OrderLogic.AddOrder();
            if (result)
            {
                Session["CartItems"] = null;
                Session["CartCount"] = null;
                return RedirectToAction("Index");



            }
            return View("ShoppingCart");
        }

        
    }
}