using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class msdn : GeneralSite
    {
        const string DOMAIN_HOST = @"https://msdn.microsoft.com/";
        public msdn() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.SelectSingleNode("//div[@class='mag']"); //class="story-body" //span[@style='font-size: medium;']");

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
            var title = contentNode.SelectSingleNode("//div[@class='FeatureTitle']//h1");
            var author = contentNode.SelectSingleNode("//div[@class='FeatureTitle']//p");

            book.Title = "MSDN";
            book.Creator = "MSDN";
            book.Copyright = "MSDN";
            book.Publisher = "MSDN";

            if (title != null)
            {
                book.Title = title.InnerText.Trim(badChars);
            }

            if (author != null)
            {          
                var rawAuthor = author.InnerText.Trim(badChars); // By Julie Lerman | April 2016 | Get the Code: C#   VB     
                var rawAuthorSegs = rawAuthor.Split('|');

                if (rawAuthorSegs.Count() >=2)
                {
                    book.Title = rawAuthorSegs[1] + "-" + book.Title;
                    book.Creator = rawAuthorSegs[0];//+ "(" + rawAuthorSegs[1]+ ")";
                }
                else
                {
                    book.Creator = author.InnerText.Trim(badChars);
                }
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