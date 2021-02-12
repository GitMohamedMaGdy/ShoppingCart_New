using ShoppingCart_New.Models;
using ShoppingCart_New.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_New.Data
{
    public class OrderLogic
    {
        ShoppingCartDBEntities1 dataContext;
        List<ShoppingCartModel> shoppingCartModels;

        public OrderLogic()
        {
            dataContext = new ShoppingCartDBEntities1();
            shoppingCartModels = HttpContext.Current.Session["CartItems"] as List<ShoppingCartModel>;

        }

        public bool AddOrder()
        {
            int order_Id = 0;

            Order order = new Order()
            {
                OrderDate = DateTime.Now,
                OrderNumber = string.Format("{0:ddmmyyyyHHmmsss}", DateTime.Now)
            };
            dataContext.Orders.Add(order);
            dataContext.SaveChanges();
            order_Id = order.OrderId;

            foreach (var item in shoppingCartModels)
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order_Id,
                    ItemId = item.ItemId,
                    Quantity = item.Quantity,
                    Total = item.Total,
                    UnitPrice = item.UnitPrice,
                   
                };
                dataContext.OrderDetails.Add(orderDetail);
            }
            if (dataContext.SaveChanges()>0)
            {
                return true;
            }
            return false;
          
        }
    }
}