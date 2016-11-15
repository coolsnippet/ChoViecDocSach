using HtmlAgilityPack;
using System.Linq;
using System.Collections.Generic;
//using System.Text.RegularExpressions;


namespace Onha.Kiet
{
    public class quangduc : GeneralSite
    {
        const string DOMAIN_HOST = @"http://quangduc.com/";
        public quangduc() : base(DOMAIN_HOST)
        {
        }

        #region Override methods
        protected override HtmlNode GetContentDiv(string htmlContent, bool cleanUp = false)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            var div = root.Descendants()
                              .Where(n => n.GetAttributeValue("class", "").Equals("pd_description nw_zoomcontent normal"))
                              .FirstOrDefault();

            if (!cleanUp)
                return div;

            var nodeToRemove = div.Descendants()
                              .Where(n => n.GetAttributeValue("class", "").Equals("nw_book_tree"))
                              .FirstOrDefault();

            var nodeToRemove2 = div.Descendants()
                              .Where(n => n.GetAttributeValue("class", "").Equals("nw_adspot nw_adspot_postcontent"))
                              .FirstOrDefault();

            var nodeToRemove3 = div.Descendants()
                              .Where(n => n.GetAttributeValue("class", "").Equals("clear"))
                              .FirstOrDefault();

            
            // this perfect to remove a node	
            // http://stackoverflow.com/questions/12092575/html-agility-pack-remove-element-but-not-innerhtml
            if (nodeToRemove != null)
                nodeToRemove.ParentNode.RemoveChild(nodeToRemove, false);

            if (nodeToRemove2 != null)
                nodeToRemove2.ParentNode.RemoveChild(nodeToRemove2, false);

            if (nodeToRemove3 != null)
                nodeToRemove3.ParentNode.RemoveChild(nodeToRemove3, false);

            var tableNodes = div.SelectNodes("//table");
            if (tableNodes != null)
            {
                foreach (var node in tableNodes)
                {
                    node.ParentNode.RemoveChild(node, true);
                }
            }


            return div;
        }

        protected override IEnumerable<KeyValuePair<string, string>> GetLinks(string htmlContent)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlContent);

            var root = html.DocumentNode;
            // the main div
            var div = root.Descendants()
                              .Where(n => n.GetAttributeValue("class", "").Equals("pd_description nw_zoomcontent normal"))
                              .FirstOrDefault();
            var chapter_div = div.Descendants()
                              .Where(n => n.GetAttributeValue("class", "").Equals("nw_book_tree"))
                              .FirstOrDefault();

            if (chapter_div != null)
            {
                return chapter_div.Descendants("a")
                           .Where(n => n.Attributes["title"] != null
                                && !n.Attributes["href"].Value.Contains("javascript"))
                           .Select(item => new KeyValuePair<string, string>(
                               System.Net.WebUtility.HtmlDecode(item.InnerHtml), //key is name of each chapter
                               item.Attributes["href"].Value // value is the link

                           ));
            }

            return null;
        }

        protected override Book GetBookInformation(HtmlNode contentNode)
        {
            var book = new Book();
            //style="font-size: medium;"

            book.Title = "Quảng Đức";
            book.Creator = "Quảng Đức";
            book.Copyright = "Quảng Đức";
            book.Publisher = "Quảng Đức";

            HtmlNode multipleLinkTitle = contentNode.SelectSingleNode("//*[@class='nw_boxing_title' and ancestor::*[@class='nw_book_tree']]"); // above the content of the link      
     

            var findTitle = contentNode.SelectSingleNode("//*[@class='pd_title']"); //pd_title
            var findAuthor = contentNode.SelectSingleNode("//li[@itemprop='author']");

            if (multipleLinkTitle!=null)
            {
                findTitle = multipleLinkTitle; // overwrite this, because findTitle just a chapter name if multiple page is there!
            }

            var badChars = new char[] { '\r', '\n', ' '};

            if (findTitle !=null && findAuthor != null)
            {
                book.Title = System.Net.WebUtility.HtmlDecode(findTitle.InnerText).Trim(badChars);
                book.Creator = System.Net.WebUtility.HtmlDecode(findAuthor.InnerText).Trim(badChars);
                return book;
            }

            //var span = contentNode.SelectSingleNode("//span[@style='font-size: medium;']");
            var texts = contentNode.Descendants("#text")
                                   .Where(n => n.HasChildNodes == false

                                     //&& System.Net.WebUtility.HtmlDecode(n.InnerText).Contains((char)13) == false // <> '\r'
                                     & !System.Net.WebUtility.HtmlDecode(n.InnerText).Contains("Tủ Sách Đạo Phật Ngày Nay") // ignore this title
                                     & !string.IsNullOrEmpty(System.Net.WebUtility.HtmlDecode(n.InnerText).Trim()) // no empty line
                                     & System.Net.WebUtility.HtmlDecode(n.InnerText).Trim().Length <= 50 // don't want to long length
                                    & 
                                        (
                                            ( 
                                            n.ParentNode.Name == "span"
                                            &&  n.ParentNode.Attributes["style"] != null 
                                            && n.ParentNode.Attributes["style"].Value.Contains("#0000ff")  // blue 
                                            )
                                        ||
                                            ( 
                                                n.ParentNode.ParentNode.Name == "span"
                                                &&  n.ParentNode.ParentNode.Attributes["style"] != null 
                                                && n.ParentNode.ParentNode.Attributes["style"].Value.Contains("#0000ff")  //blue 
                                            )                                        
                                         ||
                                            ( 
                                                n.ParentNode.ParentNode.ParentNode.Name == "span"
                                                &&  n.ParentNode.ParentNode.ParentNode.Attributes["style"] != null 
                                                && n.ParentNode.ParentNode.ParentNode.Attributes["style"].Value.Contains("#0000ff")  // blue 
                                            )
                                        )

                                        
                                        
                                     );



            if (texts != null)
            {
                var firstLine = texts.FirstOrDefault();
                var secondLine = texts.Count() > 1? texts.ElementAt(1) : firstLine;
                var thirdLine = (texts.Count() > 2 && secondLine != null) ? texts.ElementAt(2) : firstLine;

                book.Title = System.Net.WebUtility.HtmlDecode(firstLine.InnerText).Trim();
                book.Creator = System.Net.WebUtility.HtmlDecode(secondLine.InnerText).Trim();

                if (book.Title.EndsWith(",")
                || book.Title.EndsWith(":")
                )
                {
                    book.Title = book.Title + book.Creator;
                    book.Creator = System.Net.WebUtility.HtmlDecode(thirdLine.InnerText).Trim();
                }
            }

            return book;
        }

        #endregion

    }
}