using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAutoOperator.Bll
{
    public class UrlBll
    {
        public static string GetUrl(string url)
        {
            if(url.IndexOf("http://") >= 0)
            {
                return url;
            }
            else
            {
                url = "http://" + url;
                return url;
            }
        }
    }
}
