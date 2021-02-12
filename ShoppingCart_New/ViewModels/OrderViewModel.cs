using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingCart_New.ViewModels
{
    public class OrderViewModel
    {
        public int orderId { get; set; }
        public string orderNumber{ get; set; }
        public DateTime orderDateTime { get; set; }
    }
}