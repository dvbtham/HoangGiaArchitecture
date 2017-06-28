using System.Collections.Generic;
using System.Globalization;
using HoangGia.Entities;
using HoangGia.Model.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HoangGia.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HoangGia.Data.HoangGiaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HoangGiaDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            CreateUser(context);
            CreateMenu(context);
            InitializeSetting(context);
        }

        private void InitializeSetting(HoangGiaDbContext context)
        {
            if (!context.Settings.Any())
            {
                var settings = new Setting
                {
                    Name = "Nội thất Hoàng Gia",
                    PageTitle = "Nội thất Hoàng Gia",
                    MetaDesc = "Nội thất Hoàng Gia, Nội thất Đà Nẵng",
                    Url = "http://localhost:51745",
                    AdminEmailAddress = "thamit1996@gmail.com",
                    CompanyAddress = "66 Trường Chinh, Q.Liên Chiểu, TP.Đà Nẵng ",
                    Lat = "16.0857245",
                    Long = "108.1768541",
                    SmtpPort = "587",
                    Smtp = "smtp.gmail.com",
                    SmtpEnableSsl = true,
                    SmtpUsername = "thamit1996@gmail.com",
                    SmtpPassword = "Abc123456$",
                    Tel = "01234345454",
                    WorkingDay = "Monday - Friday: 7.00 am - 18:00 pm",
                    NotificationReplyEmail = "thamdv96@gmail.com",
                    ServiceScheme = "Chúng tôi làm việc với chất lượng tốt nhất của dịch vụ cho khách hàng của chúng tôi và khách hàng của chúng tôi đánh giá cao chúng tôi cho điều đó.",
                    ServiceDescription = "Hoàng Gia chuyên cung cấp các dịch vụ kiến ​​trúc và thiết kế nội thất đặc biệt cho khách hàng trong và ngoài nước.",
                    //WhyChooseUsTitles = @"Kinh nghiệm/Chuyên nghiệp/Uy tín & Chất lượng/Tư vấn miễn phí",
                    
                };

                context.Settings.Add(settings);
                context.SaveChanges();
            }
        }

        private void CreateMenu(HoangGiaDbContext context)
        {
            if (!context.Menus.Any())
            {
                var menus = new List<Menu>
                {
                    new Menu{ Name = "Dịch vụ", ActionName = "Index", ControllerName = "Services", Order = 1, IsActived = true },
                    new Menu{ Name = "Giới thiệu", ActionName = "Index", ControllerName = "Services", Order = 2, IsActived = true },
                    new Menu{ Name = "Dự án hoàn thành", ActionName = "Index", ControllerName = "Projects",  Order = 3,  IsActived = true },
                    new Menu{ Name = "Cẩm nang", ActionName = "Index", ControllerName = "Blog", Order = 4, IsActived = true },
                    new Menu{ Name = "Liên hệ", ActionName = "Index", ControllerName = "Contact", Order = 5, IsActived = true },
                    new Menu{ Name = "Nội thất", ActionName = "Index", ParentId = 1, ControllerName = "Contact", Order = 1, IsActived = true },
                    new Menu{ Name = "Ngoại thất", ActionName = "Index", ParentId = 1, ControllerName = "Contact", Order = 1, IsActived = true },
                };
                context.Menus.AddRange(menus);
                context.SaveChanges();
            }
        }

        private void CreateUser(HoangGiaDbContext context)
        {
            if (!context.Users.Any())
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new HoangGiaDbContext()));

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new HoangGiaDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "dvbtham",
                    Email = "dvbtham@gmail.com",
                    EmailConfirmed = true,
                    Image = "",
                    CreatedDate = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    BirthDay = DateTime.ParseExact("15/09/1996", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Gender = "Nam",
                    Fullname = "Thâm Davies",
                    Address = "Gia Lai",
                    PhoneNumber = "01652130546",
                    IsDeleted = false
                };
                if (!manager.Users.Any())
                {
                    manager.Create(user, "123123$");

                    if (!roleManager.Roles.Any())
                    {
                        roleManager.Create(new IdentityRole { Name = "Admin" });
                        roleManager.Create(new IdentityRole { Name = "User" });
                    }

                    var adminUser = manager.FindByEmail("thamdv96@gmail.com");

                    manager.AddToRoles(adminUser.Id, "Admin", "User");

                }
            }
        }
    }
}
