﻿using eQACoLTD.ViewModel.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace eQACoLTD.AdminMvc.TagHelpers
{
    [HtmlTargetElement("pager", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PagerTagHelper : TagHelper
    {
        private readonly HttpContext _httpContext;
        private readonly IUrlHelper _urlHelper;

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public PagerTagHelper(IHttpContextAccessor accessor, IActionContextAccessor actionContextAccessor, IUrlHelperFactory urlHelperFactory)
        {
            _httpContext = accessor.HttpContext;
            _urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        [HtmlAttributeName("pager-model")]
        public PagedResultBase Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Model == null)
            {
                return;
            }

            if (Model.PageCount == 0)
            {
                return;
            }

            var PAGES_TO_SHOW = 5;
            var action = ViewContext.RouteData.Values["action"].ToString();
            var urlTemplate = WebUtility.UrlDecode(_urlHelper.Action(action, new { page = "{0}" }));
            var request = _httpContext.Request;
            foreach (var key in request.Query.Keys)
            {
                if (key == "page")
                {
                    continue;
                }

                urlTemplate += "&" + key + "=" + request.Query[key];
            }

            var startIndex = Math.Max((Model.CurrentPage - PAGES_TO_SHOW) - Math.Max(PAGES_TO_SHOW - (Model.PageCount - Model.CurrentPage), 0), 1);
            var finishIndex = Math.Min(Model.CurrentPage + PAGES_TO_SHOW + Math.Max(PAGES_TO_SHOW - Model.CurrentPage + 1, 0), Model.PageCount);

            output.TagName = "";
            output.Content.AppendHtml("<ul class=\"pagination\">");
            AddPageLink(output, string.Format(urlTemplate, 1), "&laquo;");

            for (var i = startIndex; i <= finishIndex; i++)
            {
                if (i == Model.CurrentPage)
                {
                    AddCurrentPageLink(output, i);
                }
                else
                {
                    AddPageLink(output, string.Format(urlTemplate, i), i.ToString());
                }
            }

            AddPageLink(output, string.Format(urlTemplate, Model.PageCount), "&raquo;");
            output.Content.AppendHtml("</ul>");
        }

        private void AddPageLink(TagHelperOutput output, string url, string text)
        {
            output.Content.AppendHtml("<li  class=\"page-item\" ><a href=\"");
            output.Content.AppendHtml(url);
            output.Content.AppendHtml("\" class=\"page-link\">");
            output.Content.AppendHtml(text);
            output.Content.AppendHtml("</a>");
            output.Content.AppendHtml("</li>");
        }

        private void AddCurrentPageLink(TagHelperOutput output, int page)
        {
            output.Content.AppendHtml("<li class=\"page-item active\"><a class=\"page-link\">");
            output.Content.AppendHtml(page.ToString());
            output.Content.AppendHtml(" <span class=\"sr-only\">(current)</span>");
            output.Content.AppendHtml("</li>");
        }
    }
}
