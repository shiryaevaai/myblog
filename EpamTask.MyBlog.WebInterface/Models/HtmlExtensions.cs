namespace EpamTask.MyBlog.WebInterface.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    using EpamTask.MyBlog.Entities;
    using EpamTask.MyBlog.Logic;

    public static class HtmlExtensions
    {        
        public static IHtmlString DisplayFormattedData(this HtmlHelper htmlHelper, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return MvcHtmlString.Empty;
            }

            var firstEdit = data
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToArray();

            List<string> secondEdit = new List<string>();

            for (int i = 0; i < firstEdit.Count(); i++)
            {
                if (firstEdit[i].Length <= 50)
                {
                    secondEdit.Add(firstEdit[i]);
                }
                else
                {
                    string input = firstEdit[i];
                    string part;

                    while (input.Length > 50)
                    {
                        part = input.Substring(0, 50);
                        secondEdit.Add(part);
                        input = input.Substring(50);
                    }

                    secondEdit.Add(input);
                }
            }

            var result = string.Join("<br/>", secondEdit.Select(htmlHelper.Encode));            

            return new HtmlString(result);
        }
    }
}