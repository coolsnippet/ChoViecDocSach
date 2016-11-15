using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class rapidfireart : GeneralSite
    {
        const string DOMAIN_HOST = @"http://rapidfireart.com/";
        public rapidfireart() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.SelectSingleNode("//article[@itemtype='http://schema.org/Article']"); //class="story-body" //span[@style='font-size: medium;']");

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
            var title = contentNode.SelectSingleNode("//h1[@class='entry-title']");
            var author = contentNode.SelectSingleNode("//div[@class='saboxplugin-authorname']");

            book.Title = "rapidfire art";
            book.Creator = "rapidfire art";
            book.Copyright = "rapidfire art";
            book.Publisher = "rapidfire art";

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