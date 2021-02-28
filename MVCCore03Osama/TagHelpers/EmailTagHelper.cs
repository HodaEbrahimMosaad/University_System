using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace osamav3.TagHelpers
{
    [HtmlTargetElement("email")]
    public class EmailTagHelper:TagHelper
    {
        public string MailTo { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", "mailto:" + MailTo);
            output.Content.SetContent(MailTo);
        }
    }
}
