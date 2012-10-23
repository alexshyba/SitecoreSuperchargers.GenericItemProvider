using System;
using Sitecore;
using Sitecore.Exceptions;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
   public class DateTimeToDateConversionAttribute : ConvertibleFieldMapping
   {
      public DateTimeToDateConversionAttribute(string fieldId) : base(fieldId) {}

      public override string Convert(string raw)
      {
         DateTime date;

         if (!DateTime.TryParse(raw, out date))
         {
            throw new InvalidTypeException("DateTime is expected as a parameter");
         }

         return DateUtil.ToIsoDate(date);
      }
   }
}
