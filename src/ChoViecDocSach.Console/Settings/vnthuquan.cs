using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//using System.Text.RegularExpressions;

namespace Onha.Kiet
{

        //  noidung1('tuaid=3452&chuongid=1');

        // function noidung1(request) {
        //     if (url != '') {
        //         Goi_fun(cho_NOIDUNG_moi("tieude"), khong_gi("fontchu")); // xoa het va co loading sign
        //         SendQuery('chuonghoi_moi.aspx?', 'Displaylan0("solan"), Displaylan1("tieude"), Displaylan2("fontchu"), Displaylan3("nguon")', 'POST', 0, request);
        //     }
        // }

        // the first call only have html template 
        // <div id="fontchu" class="truyen_text">
        // <SCRIPT>
        // 					 if(firstload == false ){
        // 					   noidung1('tuaid=3452&chuongid=1'); // use this to request and then split

        // 					    }
        // </SCRIPT>
        // </div>
    public class vnthuquan : GeneralSite
    {
        const string DOMAIN_HOST = @"http://vnthuquan.net/";
        public vnthuquan() : base(DOMAIN_HOST)
        {
            dataDeligate = webber.GetStringPostAsync;
        }

        public virtual Book CheckBookDownloaded(string firstpage)
        {
            //http://vnthuquan.net/truyen/chuonghoi_moi.aspx
            //with databody

            return base.CheckBookDownloaded(firstpage);
        }

        public virtual Book GetOneWholeHtml(string firstpage)
        {
            //http://vnthuquan.net/truyen/chuonghoi_moi.aspx
            // with databody

            return base.GetOneWholeHtml(firstpage);
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            // html.LoadHtml(htmlContent);


            var parts = Regex.Split(htmlContent, "--!!tach_noi_dung!!--");
            html.LoadHtml(parts[1]); 

            return html.DocumentNode;
        }

        protected override IEnumerable<KeyValuePair<string, string>> GetLinks(string htmlContent)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            // the main div
            var div = root.SelectSingleNode("//div[@id='saomu']");

            if (div != null)
            { 
                
                return div.Descendants("li")
                           .Where(n => n.Attributes["onClick"] != null)
                                
                           .Select(item => new KeyValuePair<string, string>(
                               System.Net.WebUtility.HtmlDecode(item.InnerText), //key is name of each chapter
                               item.Attributes["onClick"].Value.ParseExact(@"noidung1('{0}')")[0] // value is the link
                                                                            //"noidung1('tuaid=3452&chuongid=1')"
                           ));
            }

            return null;
        }

        protected override Book GetBookInformation(HtmlNode contentNode)
        {
            var book = new Book();

            book.Title = "Việt Nam Thư Quán";
            book.Creator = "Việt Nam Thư Quán";
            book.Copyright = "Việt Nam Thư Quán";
            book.Publisher = "Việt Nam Thư Quán";

            var findTitle = contentNode.SelectSingleNode("//*[@class='chuto40']"); 
            var findAuthor = contentNode.SelectSingleNode("//li[@class='tacgiaphai']");
            
            if (findTitle!=null)
                book.Title = findTitle.InnerText;
            
            if (findAuthor!=null)
                book.Creator = findAuthor.InnerText;

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

        // the first call only have html template 
        // <div id="fontchu" class="truyen_text">
        // <SCRIPT>
        // 					 if(firstload == false ){
        // 					   noidung1('tuaid=3452&chuongid=1'); // use this to request and then split

        // 					    }
        // </SCRIPT>
        // </div>
        public string GetSecretLink(string url)
        {
            var link = string.Empty; // 

            var htmlContent = dataDeligate(url, "").Result; // get the content
            
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent); // load it

            var root = html.DocumentNode;

            // var root = html.DocumentNode;
            // var div = root.SelectSingleNode("//div[@id='fontchu' and class='truyen_text']");
            var div = root.SelectSingleNode("//div[@id='fontchu']"); //class="story-body" //span[@style='font-size: medium;']");
            link = div.InnerText.ParseExact(@"<SCRIPT>
       						 if(firstload == false ){
     						   noidung1('{0}');
   							     
    						    }
 								</SCRIPT>")[0];
            return link;
        }
        #endregion

    }
}