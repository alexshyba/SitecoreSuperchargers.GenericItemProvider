using System;

namespace SitecoreSuperchargers.GenericItemProvider.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FieldMapping : Attribute
    {
        public string FieldId { get; set; }

        public FieldMapping(string fieldId) : this(fieldId, false) { }

        public FieldMapping(string fieldId, bool forTransation, bool disableSecurity = false)
        {
            FieldId = fieldId;
        }
    }
}
