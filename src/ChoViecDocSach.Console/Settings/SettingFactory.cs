using System;

namespace Onha.Kiet
{
    public class SettingFactory
    {
        public static GeneralSite Get(string url)
        {
            GeneralSite setting = null;
            var uri = new Uri(url); 

            switch (uri.Host)
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
                case "quangduc.com":
                    setting = new quangduc();
                    break;
                case "vnthuquan.net":
                    setting = new vnthuquan();
            
                    break;
            }

            return setting;
        }
    }
}



