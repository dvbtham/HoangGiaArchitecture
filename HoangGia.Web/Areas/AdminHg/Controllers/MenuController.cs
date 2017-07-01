using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.Infrastructure.Helpers;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public ActionResult Index()
        {
            var data = _menuService.GetAll();
            var viewModel = Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(data);
            return View(viewModel);
        }
        public ActionResult Create()
        {
            var viewModel = new MenuViewModel
            {
                Heading = "Thêm mới menu"
            };
            PrepareMenusModel(viewModel);
            return View("MenuForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(MenuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = new Menu();
                menu.MenuMapper(viewModel);
                _menuService.Add(menu);
                _menuService.SaveChanges();
                return RedirectToAction("Index", "Menu");
            }
            PrepareMenusModel(viewModel);
            viewModel.Heading = "Thêm mới menu";
            return View("MenuForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var menu = _menuService.FindById(id);
            var viewModelMapper = Mapper.Map<Menu, MenuViewModel>(menu);
            viewModelMapper.Heading = "Cập nhật menu";
            PrepareMenusModel(viewModelMapper);
            return View("MenuForm", viewModelMapper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MenuViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var menu = _menuService.FindById(viewModel.Id);
                menu.MenuMapper(viewModel);
                _menuService.Update(menu);
                _menuService.SaveChanges();
                return RedirectToAction("Index", "Menu");
            }
            PrepareMenusModel(viewModel);
            viewModel.Heading = "Cập nhật menu";
            return View("MenuForm", viewModel);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
           _menuService.Delete(id);
            _menuService.SaveChanges();
            return Json(new { response = "Xóa thành công" });
        }
        #region Helpers
        [NonAction]
        protected virtual void PrepareMenusModel(MenuViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(MenuViewModel));
            if (model.Menus == null)
                model.Menus = new List<SelectListItem>();
            model.Menus.Add(new SelectListItem
            {
                Text = @" -- Chưa chọn danh mục cha -- ",
                Value = ""
            });
            var categories = MenuSelectListHelper.GetCategoryList(_menuService);
            foreach (var c in categories)
                model.Menus.Add(c);
        }
        #endregion Helpers
    }
}