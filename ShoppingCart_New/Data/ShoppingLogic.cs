using ShoppingCart_New.Models;
using ShoppingCart_New.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_New.Data
{
    public class ShoppingLogic
    {
        ShoppingCartDBEntities1 dataContext;
        List<ShoppingCartModel> shoppingCartListItems;

        public ShoppingLogic()
        {
            dataContext = new ShoppingCartDBEntities1();
            shoppingCartListItems = new List<ShoppingCartModel>();

        }

        public IEnumerable<ShoppingCartModel> AddCart(string ItemId)
        {
            ShoppingCartModel shoppingCartItem = new ShoppingCartModel();
            Item item = dataContext.Items.SingleOrDefault(model => model.ItemId.ToString() == ItemId);
            if(HttpContext.Current.Session["CartCount"] != null)
            {
                shoppingCartListItems = HttpContext.Current.Session["CartItems"] as List<ShoppingCartModel>;
            }
            if (shoppingCartListItems.Any(model => model.ItemId == ItemId))
            {
                shoppingCartItem = shoppingCartListItems.Single(model => model.ItemId == ItemId);
                shoppingCartItem.Quantity += 1;
                shoppingCartItem.Total = shoppingCartItem.Quantity * shoppingCartItem.UnitPrice;
            }

            else
            {
                shoppingCartItem.ItemId = ItemId;
                shoppingCartItem.ItemName = item.ItemName;
                shoppingCartItem.UnitPrice = item.ItemPrice;
                shoppingCartItem.Quantity = 1;
                shoppingCartItem.ImagePath = item.ImagePath;
                shoppingCartItem.Total = item.ItemPrice;
                shoppingCartListItems.Add(shoppingCartItem);

            }
            HttpContext.Current.Session["CartItems"] = shoppingCartListItems;
            HttpContext.Current.Session["CartCount"] = shoppingCartListItems.Count;
            return shoppingCartListItems;
        }


    }
}