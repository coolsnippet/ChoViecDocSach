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
            dataDeligate = (path) =>
                {
                    var url = @"http://vnthuquan.net/truyen1/chuonghoi_moi.aspx";
                    return webber.GetStringPostAsync(url, path); 
                } ;
        }

        public override Book CheckBookDownloaded(string firstpage)
        {
            // get the link and post at below url with that link in body
            //http://vnthuquan.net/truyen/chuonghoi_moi.aspx

            var urlBook = GetSecretLink(firstpage);

            if (links== null)
            {
                links = GetLinks(webber.GetStringAsync(firstpage).Result);
            }
            
            return base.CheckBookDownloaded(urlBook); // we need to post again to get data 
        }

        public override Book GetOneWholeHtml(string firstpage)
        {
            // get the link and post at below url with that link in body
            //http://vnthuquan.net/truyen/chuonghoi_moi.aspx
            
            var urlBook = GetSecretLink(firstpage);

            if (links== null)
            {
                links = GetLinks(webber.GetStringAsync(firstpage).Result);
            }

            return base.GetOneWholeHtml(urlBook);
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            
            var parts = Regex.Split(htmlContent, "--!!tach_noi_dung!!--");
            html.LoadHtml(parts[1] +" "+ parts[2]); 

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
                var chapteritems =div.Descendants("li");

                if (chapteritems!= null && chapteritems.Count() >0)
                { 
                    return  chapteritems
                            .Where(n => n.Attributes["onClick"] != null)
                                    
                            .Select(item => new KeyValuePair<string, string>(
                                System.Net.WebUtility.HtmlDecode(item.InnerText), //key is name of each chapter
                                item.Attributes["onClick"].Value.ParseExact(@"noidung1('{0}')")[0] // value is the link
                                                                                //"noidung1('tuaid=3452&chuongid=1')"
                            ));
                }
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
            var findAuthor = contentNode.SelectSingleNode("//*[@class='tacgiaphai']");
            
            if (findTitle!=null)
                book.Title = findTitle.InnerText;
            
            if (findAuthor!=null)
                book.Creator = findAuthor.InnerText;

            return book;
        }
     

        // the first call only have html template 
        // <div id="fontchu" class="truyen_text">
        // <SCRIPT>
        // 					 if(firstload == false ){
        // 					   noidung1('tuaid=3452&chuongid=1'); // use this to request and then split

        // 					    }
        // </SCRIPT>
        // </div>
        private string GetSecretLink(string url)
        {
            var link = string.Empty; // secret link

            var htmlContent = webber.GetStringAsync(url).Result; // get the content
            
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent); // load it

            var root = html.DocumentNode;            
            var div = root.SelectSingleNode("//div[@id='fontchu']"); 
            
            link = div.InnerText.RemoveCatrigeReturn().ParseExact(@"if(firstload == false ){        noidung1('{0}');                }")[0];
              
            return link;
        }
        #endregion

    }
}