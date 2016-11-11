using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onha.Kiet;
using Microsoft.Extensions.Logging;

namespace ChoViecDocSach.Controllers
{
    public class HomeController : Controller
    {

        //http://stackoverflow.com/questions/5826649/returning-a-file-to-view-download-in-asp-net-mvc


        // I have file not found issue
        // here is a work around
        // https://github.com/aspnet/Mvc/issues/5053
        public IActionResult GetKindleFile(string url)
        {
            // check domain
            var firstPageUri = new Uri(url);

            if (firstPageUri.Scheme == "file")
            {
                var note = new MyNote();
                var bookHelper = new BookHelper(note);
                var kindleFile = bookHelper.CreateKindleFiles(url, true);
            }
            else
            {
                // special for thuvienhoasen 1 tac gia
                if (firstPageUri.Host == "thuvienhoasen.org" && firstPageUri.Segments.Contains("author/"))
                {
                    var collection = new ThuVienHoaSenCollection();
                    collection.GetWholeCollection(url);
                }
                else
                {
                    GeneralSite setting = null;

                    switch (firstPageUri.Host)
                    {
                        case "thuvienhoasen.org":
                            setting = new ThuVienHoaSen();
                            break;
                        case "langmai.org":
                            setting = new langmai();
                            break;
                        case "bbc.com":
                            setting = new BBC();
                            break;
                        case "suckhoe.vnexpress.net":
                            setting = new vnexpress();
                            break;
                        case "msdn.microsoft.com":
                            setting = new msdn();
                            break;     
                        case "rapidfireart.com":    
                            setting = new rapidfireart();    
                            break;            
                        case "design.tutsplus.com":    
                            setting = new designtutsplus();    
                            break;                                 
                    }

                    if (setting != null)
                    {
                        var bookHelper = new BookHelper(setting);
                        var kindleFile = bookHelper.CreateKindleFiles(url);
                    }
                }


            }

            return View("Index");
        }

        public IActionResult GetKindleFileCollection(string url)
        {
            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }


    }
}
