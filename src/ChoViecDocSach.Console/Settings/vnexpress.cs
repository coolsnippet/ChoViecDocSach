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

        protected override List<KeyValuePair<string, byte[]>> FixImages(HtmlNode div)
        {
            var imgNodes = div.Descendants("img");// .SelectNodes("//img");
            var images = new List<KeyValuePair<string, byte[]>>();

            foreach (var node in imgNodes)
            {
                var imagePath = node.GetAttributeValue("data-original", "");
                if (string.IsNullOrEmpty(imagePath))
                    imagePath = node.GetAttributeValue("src", "");

                var imageFile = System.IO.Path.GetFileName(imagePath);

                if (!FileNameSanitizer.IsBadName(imageFile))
                {
                    var imageBytesTask = webber.DownloadFile(imagePath);
                    byte[] imageBytes = null;

                    try
                    {
                        imageBytes = imageBytesTask.Result;

                        if (imageBytesTask.Status != System.Threading.Tasks.TaskStatus.Faulted)
                        {
                            images.Add(new KeyValuePair<string, byte[]>(imageFile, imageBytes));
                        }
                        node.SetAttributeValue("src", imageFile); // modify the name in source
                    }
                    catch (System.AggregateException ex)
                    {
                        // node.RemoveChild(node);
                    }
                    finally
                    {

                    }


                }
            }

            return images;
        }
        #endregion

    }
}