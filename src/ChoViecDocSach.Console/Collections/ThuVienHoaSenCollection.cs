using System;
using HtmlAgilityPack;

namespace Onha.Kiet
{
    public class ThuVienHoaSenCollection : CollectionBase
    {
        public override void GetWholeCollection(string firstColectionUrl)
        {
            var uri = new Uri(firstColectionUrl);
            var webber = new Webber(uri.Scheme + "://" + uri.Host);
            
            var url = firstColectionUrl;

            do
            {   
                var html = webber.GetStringAsync(url, "").Result;
                var doc = new HtmlDocument();  
                doc.LoadHtml(html);

                var div = doc.DocumentNode.SelectSingleNode("//*[@class='tbTabContent']");
                var nextPage = doc.DocumentNode.SelectSingleNode("//a[@class='tbPagingNext icon']");

                // get list of content
                if (div != null)
                {
                    var anchors = div.Descendants("a");

                    foreach (var anchor in anchors)
                    {
                        // get link for each book
                        var link = anchor.Attributes["href"].Value;
                        var setting = new ThuVienHoaSen();              
                        var bookHelper = new BookHelper(setting);
                        // create kindle file for each book
                        var kindleFile = bookHelper.CreateKindleFiles(link);
               
                    }
                }

                url = nextPage!= null ? nextPage.Attributes["href"].Value: string.Empty;
            // fetch next load!    
            } while (!string.IsNullOrEmpty(url) && !url.Contains("javascript"));
        }
       

    }
}