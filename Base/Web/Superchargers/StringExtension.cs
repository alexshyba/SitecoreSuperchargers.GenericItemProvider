using System.Web;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;

namespace SitecoreSuperchargers.GenericItemProvider.Superchargers
{
    public static class StringExtension
    {
        public static string DecodeUri(this ItemUri origin)
        {
            return HttpUtility.UrlDecode(origin.ToString());
        }

        public static string EncodeUri(this ItemUri origin)
        {
            return HttpUtility.UrlEncode(origin.ToString());
        }

        public static string DecodeUrl(this string origin)
        {
            return HttpUtility.UrlDecode(origin);
        }

        public static string EncodeUrl(this string origin)
        {
            return HttpUtility.UrlEncode(origin);
        }

        public static ItemUri ToItemUri(this string uri)
        {
            Assert.IsNotNullOrEmpty(uri, "Uri parameter is expected");
            uri = uri.DecodeUrl();
            Assert.IsTrue(ItemUri.IsItemUri(uri), "Uri parameter is not properly formed");
            return ItemUri.Parse(uri);
        }

        public static bool IsItemUri(this string uri)
        {
            if (uri.IsNullOrEmpty()) return false;
            uri = uri.DecodeUrl();
            return ItemUri.IsItemUri(uri);
        }
    }
}
