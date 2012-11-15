using System.Collections.Generic;
using SitecoreSuperchargers.GenericItemProvider.AW.Models;
using SitecoreSuperchargers.GenericItemProvider.Data.Processors;

namespace SitecoreSuperchargers.GenericItemProvider.AW
{
   public class Processor : GenericProcessor<Product>
   {
      protected override Dictionary<string, string> LanguageMap
      {
         get { return new Dictionary<string, string> { { "en", "en" }, { "fr", "fr-FR" }}; }
      }

      protected override string EntityTemplateId
      {
         get { return "{4E8DF96E-97F9-40A7-AD3B-ED3756A8D171}"; }
      }

      protected override string ContainerTemplateId
      {
         get { return "{08449BE3-869E-46FE-9E01-7285CD91728B}"; }
      }

      protected override IEnumerable<Product> Data
      {
         get
         {
            using (var db = new ProductsContext())
            {
               var data = new List<Product>();
               data.AddRange(db.Products);
               return data;
            }
         }
      }

      protected override int MaxFetch
      {
         get { return 1000; }
      }
   }
}
