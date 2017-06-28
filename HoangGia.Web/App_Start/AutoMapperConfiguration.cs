using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HoangGia.Entities;
using HoangGia.Model.Entities;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.App_Start
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
                x.CreateMap<ProjectImage, ProjectImageViewModel>();
                x.CreateMap<ProjectCategory, ProjectCategoryViewModel>();
            });
        }
    }
}