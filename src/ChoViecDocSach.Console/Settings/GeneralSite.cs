using System;
using System.Collections.Generic;
using System.IO;
using HtmlAgilityPack;

namespace Onha.Kiet
{
    public abstract class GeneralSite
    {
        protected IEnumerable<KeyValuePair<string, string>> links;
        protected string domainHost;

        protected Webber webber;

        protected GetDataDeligate dataDeligate;

        public GeneralSite() : this(string.Empty)
        {
            
        }

        public GeneralSite(string domainHost)
        {
            this.domainHost = domainHost;   
            webber = new Webber(domainHost);  
            dataDeligate = webber.GetStringAsync;     
        }


        public virtual Book CheckBookDownloaded(string firstpage)
        {
            var html = string.Empty;
       
            // 1. download

            // special for note
            if (string.IsNullOrEmpty(domainHost))
            {
                var uri = new Uri(firstpage);
                html = File.ReadAllText(uri.AbsolutePath);
                return GetBookInformation(GetContentDiv(html));
            }

            // continue as normal
            

            try
            {
                 html = dataDeligate(firstpage).Result;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                throw;
            }
           

            // 2. parse to get links of chapters
            // links = GetLinks(html);
            // 3. get content div
            var contentDiv = GetContentDiv(html);
            // 4. get book information: title, publisher, author
            var book = GetBookInformation(contentDiv);

            return book;
        }

        // get the all content of a book and return a book data
        public virtual Book GetOneWholeHtml(string firstpage)
        {
            var html = string.Empty;
            var onlyOnePage = false;
            // 1. download

            // special for note
            if (string.IsNullOrEmpty(domainHost))
            {
                var uri = new Uri(firstpage);
                html = File.ReadAllText(uri.AbsolutePath);
                return GetBookInformation(GetContentDiv(html));
            }

            // continue as normal
            html = dataDeligate(firstpage).Result;

            // 2. parse to get links of chapters
            if (links== null)
                links = GetLinks(html);

            // 3. get content div
            var contentDiv = GetContentDiv(html);
            // 4. get book information: title, publisher, author
            var book = GetBookInformation(contentDiv);

            // only 1 page!
            if (links == null)
            {
                onlyOnePage = true;
                links = new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(book.Title, firstpage) };
            }

            // 6. get table of content to book
            book.TableOfContent = HtmlTableOfContent();
            // 7. loop and download each page per chapter
            var count = 1;
            
            foreach (var link in links)
            {              
                // current chapter
                System.Console.WriteLine(link.Key);
                // 8. download each page/content          
                if (!onlyOnePage)
                {
                    html = dataDeligate(link.Value).Result;
                }
                // 9. get main contain of chapter/page
                var div = GetContentDiv(html, cleanUp: true);
                // 10. download images
                var images = FixImages(div);
                // 11. add to book chapter
                book.Chapters.Add(new Chapter
                {
                    Title = link.Key,
                    Content = div,
                    Number = count,
                    Images = images
                });
                count = count + 1;
            };


            return book;
        }

        // every site has different structure for content
        abstract protected HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false);

        // and different structure to get links of table of content
        abstract protected IEnumerable<KeyValuePair<string, string>> GetLinks(string htmlContent);
        abstract protected Book GetBookInformation(HtmlNode contentNode);        
        protected virtual List<KeyValuePair<string, byte[]>> FixImages(HtmlNode div)
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
                        System.Console.WriteLine(ex);
                        // node.RemoveChild(node);
                    }
                    finally
                    {

                    }


                }
            }

            return images;
        }

        private HtmlNode HtmlTableOfContent()
        {
            // create a vietnamese document
            var toc = HtmlNode.CreateNode("<div id=\"toc\" class=\"center\"></div>");
            var h2 = HtmlNode.CreateNode("<h2>Mục Lục</h2>");
            toc.PrependChild(h2);
            var ul = HtmlNode.CreateNode("<ul style=\"list-style:none\"></ul>");
            var count = 1;

            //loop through each link
            foreach (var link in links)
            {
                var il = HtmlNode.CreateNode(string.Format("<li><a href=\"#ch{0}\">{1}</a></li>", count++, link.Key));
                ul.AppendChild(il);
            }

            toc.AppendChild(ul);
            return toc;
        }


    }
}