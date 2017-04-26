using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class trungtamhotong : GeneralSite
    {
        const string DOMAIN_HOST = @"http://trungtamhotong.org/";
        public trungtamhotong() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.SelectSingleNode("//div[@id='content']"); //class="story-body" //span[@style='font-size: medium;']");

            var nodeToRemove = div.SelectSingleNode("//*[@class='discreet']");

            // this perfect to remove a node	
            // http://stackoverflow.com/questions/12092575/html-agility-pack-remove-element-but-not-innerhtml
            if (nodeToRemove != null)
                nodeToRemove.ParentNode.RemoveChild(nodeToRemove, false);

            return div;
        }

        protected override IEnumerable<KeyValuePair<string, string>> GetLinks(string htmlContent)
        {

            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            // the main div
            var div = root.SelectSingleNode("//*[@class='portletItem lastItem']");

            if (div != null)
            {
                var chapteritems = div.Descendants("a");

                if (chapteritems != null && chapteritems.Count() > 0)
                {
                    return chapteritems
                            .Select(item => new KeyValuePair<string, string>(
                                System.Net.WebUtility.HtmlDecode(item.InnerText), //key is name of each chapter
                                item.Attributes["href"].Value // value is the link
                                                              //"noidung1('tuaid=3452&chuongid=1')"
                            ));
                }
            }

            return null;
        }

        protected override Book GetBookInformation(HtmlNode contentNode)
        {
            var book = new Book();

            var doc = contentNode.OwnerDocument.DocumentNode;

            var badChars = new char[] { '\r', '\n', ' ' };
            var title = doc.SelectSingleNode("//*[contains(@class,'navTreeItem navTreeTopNode')]");

            // #2 senario
            if (title == null) 
                title = doc.SelectSingleNode("//*[@class='tile']");

            // tile
            var author = doc.SelectSingleNode("//*[@id='parent-fieldname-description']");

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