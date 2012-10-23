using System;
using Sitecore.StringExtensions;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
   public class UrlToExternalLinkConversionAttribute : ConvertibleFieldMapping
   {
      public UrlToExternalLinkConversionAttribute(string fieldId) : base(fieldId) { }

      public override string Convert(string raw)
      {
         var extFieldValue = @"<link linktype=""external"" url=""{0}"" anchor="""" target="""" />";
         return extFieldValue.FormatWith(raw);
      }
   }
}
