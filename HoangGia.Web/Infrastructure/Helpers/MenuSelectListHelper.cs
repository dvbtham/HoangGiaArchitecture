using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HoangGia.Entities;
using HoangGia.Service.Services;

namespace HoangGia.Web.Infrastructure.Helpers
{
    public static class MenuSelectListHelper
    {
        public static List<SelectListItem> GetCategoryList(IMenuService menuService)
        {
            var categories = GetAllCategories(menuService.GetAll().OrderBy(x => x.Order));
            var result = new List<SelectListItem>();
            //clone the list to ensure that "selected" property is not set
            foreach (var item in categories)
            {
                result.Add(new SelectListItem
                {
                    Text = item.Text,
                    Value = item.Value
                });
            }

            return result;
        }

        private static IEnumerable<SelectListItem> GetAllCategories(IEnumerable<Menu> menus)
        {
            return menus.Select(c => new SelectListItem
            {
                Text = c.GetFormattedBreadCrumb(menus.ToList()),
                Value = c.Id.ToString()
            });
        }

        private static string GetFormattedBreadCrumb(this Menu menu,
            IList<Menu> menus,
            string separator = " >> ")
        {
            string result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(menu, menus, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Name;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : $"{result} {separator} {categoryName}";
            }

            return result;
        }

        private static IList<Menu> GetCategoryBreadCrumb(this Menu menu,
            IList<Menu> allCategories, bool showHidden = false)
        {
            if (menu == null)
                throw new ArgumentNullException(nameof(Menu));

            var result = new List<Menu>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (menu != null && //not null
                   showHidden &&
                   !alreadyProcessedCategoryIds.Contains(menu.Id)) //prevent circular references
            {
                result.Add(menu);

                alreadyProcessedCategoryIds.Add(menu.Id);

                menu = (from c in allCategories
                    where c.Id == menu.ParentId
                    select c).FirstOrDefault();
            }
            result.Reverse();
            return result;
        }
    }
}