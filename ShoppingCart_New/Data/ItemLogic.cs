using ShoppingCart_New.Models;
using ShoppingCart_New.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart_New.Data
{
    public class ItemLogic
    {
        ShoppingCartDBEntities1 dataContext;

        public ItemLogic()
        {
            dataContext = new ShoppingCartDBEntities1();

        }

        public Item AddItem(ItemViewModel itemViewModel)
        {
            string newImage = Guid.NewGuid() + Path.GetExtension(itemViewModel.ItemImage.FileName);
            itemViewModel.ItemImage.SaveAs(HttpContext.Current.Server.MapPath("~/Images/" + newImage));
            Item itemToAdd = new Item();
            itemToAdd.ItemId = Guid.NewGuid();
            itemToAdd.ItemName = itemViewModel.ItemName;
            itemToAdd.ItemPrice = itemViewModel.ItemPrice;
            itemToAdd.ItemCode = itemViewModel.ItemCode;
            itemToAdd.Description = itemViewModel.Description;
            itemToAdd.CategoryId = itemViewModel.CategoryId;
            itemToAdd.ImagePath = "~/Images/" + newImage;
            dataContext.Items.Add(itemToAdd);
            if (dataContext.SaveChanges() > 0)
            {
                return itemToAdd;
            }
            return itemToAdd;

        }

        public ItemViewModel getItemViewModel()
        {
            ItemViewModel itemVM = new ItemViewModel();
            itemVM.CategoryListItems = getCategories();
            return itemVM;
        }

        public IEnumerable<SelectListItem> getCategories()
        {
            
            var categoriesList = new List<Category>();
            var categoriesSelectListItems = new List<SelectListItem>();
            categoriesList = dataContext.Categories.ToList();
            foreach (var category in categoriesList)
            {
                categoriesSelectListItems.Add(new SelectListItem
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString(),
                    Selected = true
                });
            }
            return categoriesSelectListItems;
        }
    }
}