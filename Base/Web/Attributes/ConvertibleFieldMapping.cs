using System;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
   public abstract class ConvertibleFieldMapping : FieldMapping, IConvertibleAttribute
   {
      protected ConvertibleFieldMapping(string fieldId) : this(fieldId, false) { }

      protected ConvertibleFieldMapping(string fieldId, bool forTransation, bool disableSecurity = false)
         : base(fieldId, forTransation, disableSecurity)
      {

      }

      public abstract string Convert(string raw);
   }
}
