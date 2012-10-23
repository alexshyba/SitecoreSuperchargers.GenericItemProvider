using Sitecore.Data;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;

namespace SitecoreSuperchargers.GenericItemProvider.Superchargers
{
   public static class DatabaseExtension
   {
      public static Template GetTemplate(this Database database, string templateId)
      {
         Assert.IsNotNull(database, "database cannot be null");
         Assert.IsTrue(ID.IsID(templateId), "templateId is not a proper GUID");

         return TemplateManager.GetTemplate(ID.Parse(templateId), database);
      }
   }
}
