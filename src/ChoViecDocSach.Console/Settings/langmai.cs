using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class langmai : GeneralSite
    {
        const string DOMAIN_HOST = @"http://langmai.org/";
        public langmai() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.SelectSingleNode("//div[@id='content']"); //class="story-body" //span[@style='font-size: medium;']");

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
            var title = contentNode.SelectSingleNode("//h1[@id='parent-fieldname-title']");
            var author = contentNode.SelectSingleNode("//div[@id='parent-fieldname-description']");

            book.Title = "langmai";
            book.Creator = "langmai";
            book.Copyright = "langmai";
            book.Publisher = "langmai";

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