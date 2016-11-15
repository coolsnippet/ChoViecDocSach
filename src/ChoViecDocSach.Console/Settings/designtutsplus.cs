using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class designtutsplus : GeneralSite
    {
        const string DOMAIN_HOST = @"https://design.tutsplus.com/";
        public designtutsplus() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.SelectSingleNode("//main[@class='content']"); //class="story-body" //span[@style='font-size: medium;']");

            return div;
        }

        protected override IEnumerable<KeyValuePair<string, string>> GetLinks(string htmlContent)
        {           
            return null;
        }

        protected override Book GetBookInformation(HtmlNode contentNode)
        {
            var book = new Book();

            var badChars = new char[] { '\r', '\n', ' '};
            var title = contentNode.SelectSingleNode("//div[@class='content-banner__body']");
            var author = contentNode.SelectSingleNode("//span[@class='content-heading__author-name']");

            book.Title = "design-tutsplus";
            book.Creator = "design-tutsplus";
            book.Copyright = "design-tutsplus";
            book.Publisher = "design-tutsplus";

            if (title != null)
            {
                book.Title = title.InnerText.Trim(badChars);
            }

            if (author != null)
            {               
                book.Creator = author.InnerText.Trim(badChars);
            }

            return book;
        }


        #endregion

    }
}