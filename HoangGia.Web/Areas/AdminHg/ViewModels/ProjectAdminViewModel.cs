using System.Collections.Generic;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.ViewModels
{
    public class ProjectAdminViewModel
    {
        public ProjectCategoryViewModel ProjectCategoryViewModel { get; set; }
        public ProjectViewModel ProjectViewModel { get; set; }

        public IEnumerable<ProjectViewModel> Projects { get; set; }
        public IEnumerable<ProjectCategoryViewModel> ProjectCategories { get; set; }
    }
}