using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Links;
using Sitecore.StringExtensions;

namespace SitecoreSuperchargers.GenericItemProvider.Superchargers
{
   public static class LinkDatabaseService
   {
      //public static List<Item> GetReferrers(this Item item, string templateId, string sourceFieldID = "")
      //{
      //   Assert.IsNotNull(item, "item");
      //   Assert.IsTrue(ID.IsID(templateId), "Template ID is not a valid GUID");

      //   var dbName = item.Database.Name;

      //   var itemLinks = new List<ItemLink>();

      //   if (!sourceFieldID.IsNullOrEmpty() && ID.IsID(sourceFieldID))
      //   {
      //      itemLinks = Globals.LinkDatabase.GetReferrers(item, ID.Parse(sourceFieldID)).Where(l => l.MatchesDatabase(dbName)).ToList();
      //   }
      //   else
      //   {
      //      itemLinks = Globals.LinkDatabase.GetReferrers(item).Where(l => l.MatchesDatabase(dbName)).ToList();
      //   }

      //   var itemList = new List<Item>();

      //   var template = item.Database.GetTemplate(templateId);

      //   foreach (var link in itemLinks)
      //   {
      //      var referrer = link.GetSourceItem();

      //      if (referrer != null)
      //      {
      //         if (template == null || referrer.IsDerived(template.ID))
      //            itemList.Add(referrer);
      //      }
      //   }

      //   return itemList;
      //}

      //public static bool MatchesDatabase(this ItemLink link, string dbName)
      //{
      //   return link.SourceDatabaseName.Equals(dbName, StringComparison.InvariantCultureIgnoreCase) &&
      //          link.TargetDatabaseName.Equals(dbName, StringComparison.InvariantCultureIgnoreCase);
      //}
   }
}