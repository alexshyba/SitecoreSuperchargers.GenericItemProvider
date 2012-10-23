using System;
using SitecoreSuperchargers.GenericItemProvider.Attributes;
using SitecoreSuperchargers.GenericItemProvider.Data;

namespace SitecoreSuperchargers.GenericItemProvider.RSS
{
   public class RSSEntry : IEntity
   {
      [FieldMapping("{563AC409-FC84-4128-BC3E-813ED286670E}")]
      public string Title { get; set; }

      [FieldMapping("{3A27D016-2C4E-45FF-A7E1-F5CEA846FB00}")]
      public string Description { get; set; }

      [DateTimeToDateConversion("{D0713326-E16A-4DA7-931D-C6FE5C1C936F}")]
      public DateTime Published { get; set; }

      [UrlToExternalLinkConversion("{778484DF-E28C-4B03-86D8-1E34006FEEC5}")]
      public string Url { get; set; }

      public string GetItemName()
      {
         return Title;
      }

      public string GetLanguageName()
      {
         return String.Empty;
      }
   }
}
