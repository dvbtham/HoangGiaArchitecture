using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using HoangGia.Model.Enums;

namespace HoangGia.Web.Infrastructure
{
    public static class MvcHtmlStringHelper
    {
        /// <summary>
        /// Extension method for <see cref="MvcHtmlStringHelper"/> to support highlighting the active tab on the default MVC menu
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="linkText">The text to display in the link</param>
        /// <param name="actionName">Link target action name</param>
        /// <param name="controllerName">Link target controller name</param>
        /// <param name="activeClass">The CSS class to apply to the link if active</param>
        /// <param name="checkAction">If true, checks the current action name to determine if the menu item is 'active', otherwise only the controller name is matched</param>
        /// <returns></returns>
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string activeClass, bool checkAction)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            if (string.Compare(controllerName, currentController, StringComparison.OrdinalIgnoreCase) == 0 && ((!checkAction) || string.Compare(actionName, currentAction, StringComparison.OrdinalIgnoreCase) == 0))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, null, new { @class = activeClass });
            }

            return htmlHelper.ActionLink(linkText, actionName, controllerName);

        }
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, string activeClass)
        {
            return MenuLink(htmlHelper, linkText, actionName, controllerName, activeClass, true);

        }
        public static MvcHtmlString HgDropdownListFor<TModel, TValue>(this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression, IEnumerable<SelectListItem> itemList,
            object htmlAttributes = null, bool renderFormControlClass = true)
        {
            var result = new StringBuilder();

            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (renderFormControlClass)
                attrs = AddFormControlClassToHtmlAttributes(attrs);

            result.Append(helper.DropDownListFor(expression, itemList, attrs));

            return MvcHtmlString.Create(result.ToString());
        }

        private static RouteValueDictionary AddFormControlClassToHtmlAttributes(IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(htmlAttributes["class"]?.ToString()))
                htmlAttributes["class"] = "form-control";
            else
            if (!htmlAttributes["class"].ToString().Contains("form-control"))
                htmlAttributes["class"] += " form-control";

            return htmlAttributes as RouteValueDictionary;
        }

        public static MvcHtmlString ProjectStatusHelper(this HtmlHelper helper, ProjectStatus status)
        {
            var classes = "label ";
            var spanBuilder = new TagBuilder("span");
            if (status == ProjectStatus.Working)
            {
                classes += "label-info ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Đang làm";
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
            if (status == ProjectStatus.Delay)
            {
                classes += "label-warning ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Trì hoãn";
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
            if (status == ProjectStatus.Done)
            {
                classes += "label-success ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Hoàn thành";
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
            classes += "label-danger ";
            spanBuilder.MergeAttribute("class", classes);
            spanBuilder.InnerHtml = "Chưa khởi tạo";
            return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
        }
    }
}