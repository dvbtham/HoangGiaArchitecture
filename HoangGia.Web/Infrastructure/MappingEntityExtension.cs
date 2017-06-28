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