using System;
using Sitecore.Data.Items;

namespace SitecoreSuperchargers.GenericItemProvider.Helpers
{
   public static class CacheHelper
   {
      public static Boolean IsExpired(Item item)
      {
         return (System.Web.HttpRuntime.Cache[GetCacheKey(item)] == null);
      }

      public static void SetAsCached(Item item)
      {
         var cacheKey = GetCacheKey(item);

         if (System.Web.HttpRuntime.Cache[cacheKey] != null)
            System.Web.HttpRuntime.Cache.Remove(cacheKey);

         System.Web.HttpRuntime.Cache.Insert(cacheKey, true, null, DateTime.Now.AddSeconds(30), new TimeSpan());
      }

      private static string GetCacheKey(Item item)
      {
         return string.Format("ItemProviderLastRefreshCache-{0}", item.ID);
      }
   }
}
