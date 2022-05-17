using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Eshop.RazorPage.TagHelpers
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("cancel", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class CancelButton : TagHelper
    {
        private readonly IHttpContextAccessor _accessor;

        public CancelButton(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string BackUrl { get; set; }
        public string Text { get; set; } = "انصراف";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var backUrl = RefererUrl();

            output.TagName = "a";
            output.Attributes.Add("href", BackUrl ?? backUrl);
            output.Attributes.Add("class", "btn btn-danger glow");
            output.Content.SetContent(Text);
        }

        private string RefererUrl()
        {
            var backUrl = _accessor.HttpContext.Request.Headers["Referer"];
            if (string.IsNullOrWhiteSpace(backUrl))
                backUrl = "/";

            return backUrl;
        }
    }
}