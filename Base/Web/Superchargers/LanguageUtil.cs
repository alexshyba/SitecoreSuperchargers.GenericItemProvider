using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.StringExtensions;

namespace SitecoreSuperchargers.GenericItemProvider.Superchargers
{
   public static class LanguageUtil
   {
      public static Language ParseLanguage(this string languagename)
      {
         Assert.IsNotNullOrEmpty(languagename, "languagename parameter cannot be null or empty");
         Assert.IsTrue(LanguageManager.LanguageRegistered(languagename), "Language '{0}' is not registered in Sitecore".FormatWith(languagename));
         Language parsedLang;
         Assert.IsTrue(Language.TryParse(languagename, out parsedLang), "Language '{0}' cannot be parsed".FormatWith(languagename));
         return parsedLang;
      }
   }
}
