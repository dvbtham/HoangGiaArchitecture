using AutoMapper;
using HoangGia.Entities;
using HoangGia.Model.Entities;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Menu, MenuViewModel>();
                x.CreateMap<Model.Entities.Service, ServiceViewModel>();
                x.CreateMap<Project, ProjectViewModel>();
                x.CreateMap<Post, PostViewModel>();
                x.CreateMap<Setting, SettingViewModel>();
                x.CreateMap<PostCategory, PostCategoryViewModel>();
                x.CreateMap<ProjectImage, ProjectImageViewModel>();
                x.CreateMap<ProjectCategory, ProjectCategoryViewModel>();
            });
        }
    }
}