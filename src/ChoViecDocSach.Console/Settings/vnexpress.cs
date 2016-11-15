using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class vnexpress : GeneralSite
    {
        const string DOMAIN_HOST = @"http://suckhoe.vnexpress.net/";
        public vnexpress() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.SelectSingleNode("//div[@class='block_col_480']"); //class="story-body" //span[@style='font-size: medium;']");

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
            var title = contentNode.SelectSingleNode("//div[@class='title_news']");
            var author = contentNode.SelectSingleNode("//p[@style='text-align:right;']");

            book.Title = "vnexpress";
            book.Creator = "vnexpress";
            book.Copyright = "vnexpress";
            book.Publisher = "vnexpress";

            if (title != null)
            {
                book.Title = title.InnerText.Trim(badChars);
            }

            if (author != null)
            {               
                book.Creator = author.InnerText.Trim();
            }

            return book;
        }

        #endregion

    }
}