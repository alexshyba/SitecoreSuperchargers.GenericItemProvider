using System.Linq;
using Sitecore;
using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Data.Items;
using Sitecore.Globalization;
using Sitecore.Pipelines;
using SitecoreSuperchargers.GenericItemProvider.Data.Processors;

namespace SitecoreSuperchargers.GenericItemProvider.Data.Providers
{
   public class GenericItemProvider : Sitecore.Data.Managers.ItemProvider
   {
      protected override Item GetItem(ID itemId, Language language, Version version, Database database)
      {
         Assert.ArgumentNotNull(itemId, "itemId");
         Assert.ArgumentNotNull(language, "language");
         Assert.ArgumentNotNull(version, "version");
         Assert.ArgumentNotNull(database, "database");

         if (Switcher<bool, IntegrationDisabler>.CurrentValue) return base.GetItem(itemId, language, version, database);

         if (Context.Site == null || !Context.Site.Name.Equals("shell") || Config.ContentDatabase == null)
            return base.GetItem(itemId, language, version, database);

         // checking for the database validity. we don't want to run this for core db for ex.
         if (!Config.SupportedDatabases.Any(d => d.Equals(database.Name)))
            return base.GetItem(itemId, language, version, database);

         var item = base.GetItem(itemId, language, version, database);

         // checking if the item implements IRepository
         bool isRepository = item.IsRepository();

         if (item == null || !isRepository || item.Name.Equals(Constants.StandardValuesItemName))
         {
            //if we are not a repository, then leave
            return item;
         }

         var args = new GenericItemProviderArgs(item);
         CorePipeline.Run("genericItemProvider", args);

         return args.RootItem;
      }
   }
}
