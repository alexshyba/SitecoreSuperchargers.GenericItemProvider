using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Items;
using SitecoreSuperchargers.GenericItemProvider.Data.Providers;
using SitecoreSuperchargers.GenericItemProvider.Superchargers;

namespace SitecoreSuperchargers.GenericItemProvider.Data
{
   public static class ContentService
   {
      public static bool IsRepository(this Item item)
      {
         if (!Switcher<bool, IntegrationDisabler>.CurrentValue)
         {
            using (new IntegrationDisabler())
            {
               return IsRepositoryItem(item);
            }
         }

         return IsRepositoryItem(item);
      }

      private static bool IsRepositoryItem(Item item)
      {
         return item.IsDerived(ID.Parse(IDs.TemplateIDs.IRepository));
      }
   }
}
