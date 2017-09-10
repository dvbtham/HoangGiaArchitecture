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

        public static MvcHtmlString TextSummary(this HtmlHelper htmlHelper, string text, int limit, string type)
        {
            switch (type)
            {
                case "title":
                    var titleBuilder = new TagBuilder("p");
                    var title = limit > 40 ? text.Substring(0, 40) + "..." : text;
                    titleBuilder.InnerHtml = title;
                    titleBuilder.MergeAttribute("data-toggle", "tooltip");
                    titleBuilder.MergeAttribute("title", text);
                    return new MvcHtmlString(titleBuilder.ToString(TagRenderMode.Normal));
                case "description":
                    var descriptionBuilder = new TagBuilder("p");
                    var description = limit > 70 ? text.Substring(0, 70) + "..." : text;
                    descriptionBuilder.InnerHtml = description;
                    descriptionBuilder.MergeAttribute("data-toggle", "tooltip");
                    descriptionBuilder.MergeAttribute("title", text);
                    return new MvcHtmlString(descriptionBuilder.ToString(TagRenderMode.Normal));
                default: return null;
            }
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

        public static MvcHtmlString ProjectStatusHelper(this HtmlHelper helper, ProjectStatus status, bool isDeleted = false)
        {
            var classes = "label ";
            var spanBuilder = new TagBuilder("span");
            if (isDeleted)
            {
                classes += "label-danger ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Không hoạt động";
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
            if (status == ProjectStatus.Working)
            {
                classes += "label-info ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Đang thi công";
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
            spanBuilder.InnerHtml = "Dự án chưa khởi tạo";
            return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString LabelActived(this HtmlHelper helper, bool status)
        {
            var classes = "label ";
            var spanBuilder = new TagBuilder("span");
            if (status)
            {
                classes += "label-success ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Hoạt động";
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }

            classes += "label-danger ";
            spanBuilder.MergeAttribute("class", classes);
            spanBuilder.InnerHtml = "Ngừng hoạt động";
            return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString LabelDeleted(this HtmlHelper helper, bool status)
        {
            var classes = "label ";
            var spanBuilder = new TagBuilder("span");
            if (!status)
            {
                classes += "label-success ";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "Hoạt động";
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }

            classes += "label-danger ";
            spanBuilder.MergeAttribute("class", classes);
            spanBuilder.InnerHtml = "Ngừng hoạt động";
            return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString LabelIsEdited(this HtmlHelper helper, DateTime? dateTime, bool isDeleted)
        {
            var classes = "";
            var spanBuilder = new TagBuilder("span");
            if (dateTime != null && isDeleted == false)
            {
                classes += "edited";
                spanBuilder.MergeAttribute("class", classes);
                spanBuilder.InnerHtml = "edited";
                spanBuilder.MergeAttribute("data-toggle", "tooltip");
                spanBuilder.MergeAttribute("title", $"vào lúc {dateTime:HH:mm} ngày {dateTime:dd/MM/yyyy}");
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
            return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString ShowTrashButton(this HtmlHelper helper, bool showButton, string trashLink, string backLink)
        {
            var classes = "btn btn-sm margin-left-8 ";
            var linkBuilder = new TagBuilder("a");
            var icon = new TagBuilder("i");

            if (showButton)
            {
                icon.MergeAttribute("class", "fa fa-trash");
                classes += "btn-warning";
                linkBuilder.MergeAttribute("class", classes);
                linkBuilder.MergeAttribute("href", trashLink);
                linkBuilder.InnerHtml = icon + " Thùng rác";
                return new MvcHtmlString(linkBuilder.ToString(TagRenderMode.Normal));
            }
            icon.MergeAttribute("class", "fa fa-reply");
            classes += "btn-success";
            linkBuilder.MergeAttribute("class", classes);
            linkBuilder.MergeAttribute("href", backLink);
            linkBuilder.InnerHtml = icon + " Trở về";
            return new MvcHtmlString(linkBuilder.ToString(TagRenderMode.Normal));
        }
    }
}