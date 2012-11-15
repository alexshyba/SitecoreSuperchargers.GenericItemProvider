using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SitecoreSuperchargers.GenericItemProvider.RSS
{
   public class RSSReader
   {
      public static IEnumerable<RSSEntry> Read(string feedUrl)
      {
         var rssFeed = XDocument.Load(feedUrl);

         var posts = from item in rssFeed.Descendants("item")
                     select new RSSEntry
                     {
                        Title = item.Element("title").Value,
                        Description = item.Element("description").Value,
                        Published = DateTime.Parse(item.Element("pubDate").Value),
                        Url = item.Element("link").Value
                     };

         return posts;
      }
   }
}
