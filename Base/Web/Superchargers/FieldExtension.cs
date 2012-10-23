using System.Collections.Generic;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SecurityModel;

namespace SitecoreSuperchargers.GenericItemProvider.Superchargers
{
   public static class FieldExtension
   {
      public static void SetValue(this Field field, string value, bool force = false)
      {
         using (new SecurityDisabler())
         {
            using (new EditContext(field.Item))
            {
               field.SetValue(value, force);
            }
         }
      }

      public static bool IsOfType<T>(this Field field)
      {
         return FieldTypeManager.GetField(field) is T;
      }

      public static void ReverseCheckboxField(this Field field)
      {
         if (field.IsOfType<CheckboxField>())
         {
            var checkboxField = ((CheckboxField)field);
            field.SetValue(checkboxField.Checked ? "0" : "1");
         }
      }

      public static void SetCheckboxField(this Field field, bool isChecked)
      {
         if (field.IsOfType<CheckboxField>())
         {
            field.SetValue(isChecked ? "1" : "0");
         }
      }

      public static IEnumerable<Item> GetItemsFromMultilist(this Item item, string fieldName, bool checkSecurity = false)
      {
         Assert.IsNotNull(item, "item cannot be null");
         Assert.IsNotNullOrEmpty(fieldName, "fieldName cannot be null or empty");

         var field = item.Fields[fieldName];

         if (field != null && field.IsOfType<MultilistField>())
         {
            var state = SecurityState.Enabled;
            if (!checkSecurity) state = SecurityState.Disabled;

            using (new SecurityStateSwitcher(state))
            {
               return ((MultilistField)field).GetItems();
            }
         }

         return new Item[0];
      }
   }
}
