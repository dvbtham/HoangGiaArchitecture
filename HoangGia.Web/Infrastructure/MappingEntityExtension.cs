using System;
using HoangGia.Entities;
using HoangGia.Model.Entities;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Infrastructure
{
    public static class MappingEntityExtension
    {
        public static void MenuMapper(this Menu menu, MenuViewModel viewModel)
        {
            menu.Name = viewModel.Name;
            menu.ActionName = viewModel.ActionName;
            menu.ControllerName = viewModel.ControllerName;
            menu.IsActived = viewModel.IsActived;
            menu.ParentId = viewModel.ParentId;
            menu.Order = viewModel.Order;
        }
        public static void ServiceMapper(this Model.Entities.Service service, ServiceViewModel viewModel)
        {
            service.Id = viewModel.Id;
            service.Name = viewModel.Name;
            service.Overview = viewModel.Overview;
            service.Images = viewModel.Images;
            service.ChooseUsContent = viewModel.ChooseUsContent;
            service.ChooseUsValues = viewModel.ChooseUsValues;
            service.IsActived = viewModel.IsActived;
            service.IsDeleted = viewModel.IsDeleted;

            service.Description = viewModel.Description;
            service.MetaDescription = viewModel.MetaDescription;
            service.MetaKeyword = viewModel.MetaKeyword;
            service.CreatedBy = viewModel.CreatedBy;
            service.CreatedDate = DateTime.Now;
            service.UpdatedBy = viewModel.UpdatedBy;
            service.UpdatedDate = viewModel.UpdatedDate;
            service.ShowOnHome = viewModel.ShowOnHome;
        }
        public static void PostMapper(this Post post, PostViewModel viewModel)
        {
            post.Id = viewModel.Id;
            post.Name = viewModel.Name;
            post.Alias = viewModel.Alias;
            post.Content = viewModel.Content;
            post.Image = viewModel.Image;
            post.CategoryId = viewModel.CategoryId;
            post.Description = viewModel.Description;
            post.HotFlag = viewModel.HotFlag;
            post.HomeFlag = viewModel.HomeFlag;
            post.IsDeleted = viewModel.IsDeleted;
            post.ViewCount = viewModel.ViewCount;
            post.Tags = viewModel.Tags;

            post.Description = viewModel.Description;
            post.MetaDescription = viewModel.MetaDescription;
            post.MetaKeyword = viewModel.MetaKeyword;
            post.CreatedBy = viewModel.CreatedBy;
            post.CreatedDate = DateTime.Now;
            post.UpdatedBy = viewModel.UpdatedBy;
            post.UpdatedDate = viewModel.UpdatedDate;
        }
        public static void PostCategoryMapper(this PostCategory postCategory, PostCategoryViewModel viewModel)
        {
            postCategory.Id = viewModel.Id;
            postCategory.Name = viewModel.Name;
            postCategory.Image = viewModel.Image;
            postCategory.ParentId = viewModel.ParentId;
            postCategory.Description = viewModel.Description;
            postCategory.HomeFlag = viewModel.HomeFlag;
            postCategory.IsDeleted = viewModel.IsDeleted;
            postCategory.Description = viewModel.Description;
            postCategory.DisplayOrder = viewModel.DisplayOrder;
            postCategory.Alias = viewModel.Alias;
        }
        public static void ProjectCategoryMapper(this ProjectCategory projectCategory, ProjectCategoryViewModel viewModel)
        {
            projectCategory.Id = viewModel.Id;
            projectCategory.Name = viewModel.Name;
            projectCategory.IsDeleted = viewModel.IsDeleted;
        }
        public static void ProjectImageMapper(this ProjectImage projectImage, ProjectImageViewModel viewModel)
        {
            projectImage.Id = viewModel.Id;
            projectImage.ProjectId = viewModel.ProjectId;
            projectImage.ImageUrl = viewModel.ImageUrl;
        }
        public static void ProjectMapper(this Project project, ProjectViewModel viewModel)
        {
            project.Id = viewModel.Id;
            project.Name = viewModel.Name;
            project.Client = viewModel.Client;
            project.Status = viewModel.Status;
            project.Location = viewModel.Location;
            project.IsDeleted = viewModel.IsDeleted;
            project.ProjectCategoryId = viewModel.ProjectCategoryId;
            project.ProjectDescription = viewModel.ProjectDescription;

            project.MetaDescription = viewModel.MetaDescription;
            project.MetaKeyword = viewModel.MetaKeyword;
            project.CreatedBy = viewModel.CreatedBy;
            project.CreatedDate = DateTime.Now;
            project.UpdatedBy = viewModel.UpdatedBy;
            project.UpdatedDate = viewModel.UpdatedDate;
        }
    }
}