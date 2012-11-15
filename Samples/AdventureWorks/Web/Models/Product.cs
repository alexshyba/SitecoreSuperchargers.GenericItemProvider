using System;
using System.Collections.Generic;
using SitecoreSuperchargers.GenericItemProvider.Attributes;
using SitecoreSuperchargers.GenericItemProvider.Data;

namespace SitecoreSuperchargers.GenericItemProvider.AW.Models
{
   public class Product : IEntity
   {
      public int ProductID { get; set; }
      public string CultureID { get; set; }

      [FieldMapping("{EA9BF370-EE86-4109-AAED-625BACDC3640}")]
      public string Name { get; set; }

      [FieldMapping("{EF3BCEAD-8594-4612-9F90-B228DAF4B056}")]
      public string ProductModel { get; set; }

      [FieldMapping("{BB1D06FF-8035-4937-983B-1B22B5B4B073}")]
      public string Description { get; set; }

      public string GetItemName()
      {
         return this.ProductID.ToString();
      }

      public string GetLanguageName()
      {
         return CultureID;
      }
   }
}
