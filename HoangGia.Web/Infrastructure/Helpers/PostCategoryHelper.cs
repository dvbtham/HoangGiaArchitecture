using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;

namespace HoangGia.Web.Infrastructure.Helpers
{
    public static class PostCategoryHelper
    {
        public static IEnumerable<SelectListItem> GetCategoryList(IPostCategoryService postCategoryService)
        {
            var categories = GetAllCategories(postCategoryService.GetAll().Where(x => x.IsDeleted == false).OrderBy(x => x.DisplayOrder));

            return categories.Select(item => new SelectListItem
            {
                Text = item.Text,
                Value = item.Value
            }).ToList();
        }

        private static IEnumerable<SelectListItem> GetAllCategories(IEnumerable<PostCategory> postCategories)
        {
            return postCategories.Select(c => new SelectListItem
            {
                Text = c.GetFormattedBreadCrumb(postCategories.ToList()),
                Value = c.Id.ToString()
            });
        }

        private static string GetFormattedBreadCrumb(this PostCategory postCategory,
            IList<PostCategory> postCategories,
            string separator = " >> ")
        {
            string result = string.Empty;

            var breadcrumb = GetCategoryBreadCrumb(postCategory, postCategories, true);
            for (int i = 0; i <= breadcrumb.Count - 1; i++)
            {
                var categoryName = breadcrumb[i].Name;
                result = String.IsNullOrEmpty(result)
                    ? categoryName
                    : $"{result} {separator} {categoryName}";
            }

            return result;
        }

        private static IList<PostCategory> GetCategoryBreadCrumb(this PostCategory postCategory,
            IList<PostCategory> allCategories, bool showHidden = false)
        {
            if (postCategory == null)
                throw new ArgumentNullException(nameof(PostCategory));

            var result = new List<PostCategory>();

            //used to prevent circular references
            var alreadyProcessedCategoryIds = new List<int>();

            while (postCategory != null && //not null
                   showHidden &&
                   !alreadyProcessedCategoryIds.Contains(postCategory.Id)) //prevent circular references
            {
                result.Add(postCategory);

                alreadyProcessedCategoryIds.Add(postCategory.Id);

                postCategory = (from c in allCategories
                                where c.Id == postCategory.ParentId
                                select c).FirstOrDefault();
            }
            result.Reverse();
            return result;
        }
    }
}