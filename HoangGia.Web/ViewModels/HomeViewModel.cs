using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HoangGia.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<ServiceViewModel> ServiceList { get; set; }
    }
}