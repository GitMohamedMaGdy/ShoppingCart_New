using ShoppingCart_New.Data;
using ShoppingCart_New.Models;
using ShoppingCart_New.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_New.Controllers
{
    public class ItemsController : Controller
    {
        ItemLogic _Logic;
        // GET: Items
        ShoppingCartDBEntities1 context;
        
        public ItemsController()
        {
            context = new ShoppingCartDBEntities1();
            _Logic = new ItemLogic();

        }

        [HttpGet]
        public ActionResult Index()
        {
            var itemVM = new ItemViewModel();
            ItemLogic _Logic = new ItemLogic();
            itemVM.CategoryListItems = _Logic.getCategories();
            return View(itemVM);
        }

        [HttpPost]
        public ActionResult Index(ItemViewModel itemViewModel)
        {
            Item item = _Logic.AddItem(itemViewModel);
            if (item != null)
            {
                ViewBag.Message ="Item is Added Successfully";
                return View(_Logic.getItemViewModel()) ;
            }
            
            ViewBag.Message = "There is an error adding Item";
            return View(_Logic.getItemViewModel());
            //return Json(new { Success = true, Message = "Item is Added Successfully" },JsonRequestBehavior.AllowGet);
            

        }
    }
}