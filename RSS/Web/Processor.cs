using System;
using System.Collections.Generic;
using SitecoreSuperchargers.GenericItemProvider.Data.Processors;

namespace SitecoreSuperchargers.GenericItemProvider.RSS
{
   public class Processor : GenericProcessor<RSSEntry>
   {
      public Processor(string feedUrl)
      {
         FeedUrl = feedUrl;
      }

      public string FeedUrl { get; set; }

      protected override Dictionary<string, string> LanguageMap
      {
         get { return new Dictionary<string, string>(0);}
      }

      protected override string EntityTemplateId
      {
         get { return "{A00CB970-9CDE-4759-86F6-399524AEA4E0}"; }
      }

      protected override string ContainerTemplateId
      {
         get { return "{A8CE323A-A8DA-41A3-93B0-F7305E7844EB}"; }
      }

      protected override IEnumerable<RSSEntry> Data
      {
         get { return !String.IsNullOrEmpty(FeedUrl) ? RSSReader.Read(FeedUrl) : new RSSEntry[0]; }
      }

      protected override int MaxFetch
      {
         get { return 100; }
      }
   }
}
