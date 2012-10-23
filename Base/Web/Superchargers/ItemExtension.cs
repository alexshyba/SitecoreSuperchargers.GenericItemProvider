using System;
using System.Collections.Generic;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Security.AccessControl;
using Sitecore.SecurityModel;
using Sitecore.StringExtensions;

namespace SitecoreSuperchargers.GenericItemProvider.Superchargers
{
   public static class ItemExtension
   {
      public static Item EnsureItemLanguage(this Item item, Language language)
      {
         if (!item.Language.Equals(language))
            item = item.Database.GetItem(item.ID, language);
         return item;
      }

      // TODO: find a more optmized way of checking for this
      public static bool HasVersions(this Item item)
      {
         return item.Versions.GetVersions(true).Length > 0;
      }

      public static string GetFieldFromAncestorOrSelf(this Item item, string field)
      {
         Assert.IsNotNull(item, "item cannot be null");
         Assert.IsNotNullOrEmpty(field, "field cannot be null");

         // we wanna stop at the /sitecore/content node
         if(item.ID.Equals(ItemIDs.ContentRoot)) return String.Empty;

         if (!item[field].IsNullOrEmpty()) return item[field];

         // recursive call
         return GetFieldFromAncestorOrSelf(item.Parent, field);
      }

      public static void Rename(this Item item, string newName, bool checkSecurity = false)
      {
         Assert.IsNotNull(item, "item");

         var displayName = newName;
         var itemName = ItemUtil.ProposeValidItemName(newName);

                  var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            using (new EditContext(item))
            {
               item.Name = itemName;
               item.ChangeDisplayName(displayName);
            }
         }
      }

      public static Item GetItemFromUri(this ItemUri uri, bool checkSecurity = false)
      {
         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            return Database.GetItem(uri);
         }
      }

      public static bool IsStandardValues(this Item item, bool checkSecurity = false)
      {
         return item.Name.Equals(Constants.StandardValuesItemName);
      }

      public static bool IsDerived(this Item item, ID templateId, bool checkSecurity = false)
      {
         if (item == null)
            return false;

         if (templateId.IsNull)
            return false;

         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            var templateItem = item.Database.Templates[templateId];

            var isDerived = false;
            if (templateItem != null)
            {
               var template = TemplateManager.GetTemplate(item);
               if(template == null) return false;
               isDerived = template.ID == templateItem.ID || template.DescendsFrom(templateItem.ID);
            }

            return isDerived;
         }
      }

      public static Item CreateChild(this Item parent, string name, string id, bool checkSecurity = false)
      {
         Assert.IsTrue(ID.IsID(id), "id parameter is not a valid GUID");

         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            var branch = parent.Database.Branches[id];

            if (branch != null)
            {
               return parent.Add(name, branch);
            }

            var template = parent.Database.Templates[id];

            Assert.IsNotNull(template, "Neither branch nor template with ID {0} could not be found.".FormatWith(id));
            return parent.Add(name, template);
         }
      }

      public static void ChangeDisplayName(this Item item, string displayName, bool checkSecurity = false)
      {
         Assert.IsNotNull(item, "item");

         if (!displayName.IsNullOrEmpty())
         {
            var state = SecurityState.Enabled;
            if (!checkSecurity) state = SecurityState.Disabled;

            using (new SecurityStateSwitcher(state))
            {
               using (new EditContext(item))
               {
                  item.Appearance.DisplayName = displayName;
               }
            }
         }
      }

      public static void ChangeFieldValue(this Item item, string fieldId, string newValue, bool checkSecurity = false)
      {
         var field = item.Fields[fieldId];

         if (field == null) return;

         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            using (new EditContext(item))
            {
               field.Value = newValue;
            }
         }
      }

      public static IEnumerable<Item> RemoveIdFromMultilist(this Item item, string fieldId, string valueToRemove, bool checkSecurity = false)
      {
         var field = item.Fields[fieldId];

         if (field == null) return new Item[0];

         if (!field.IsOfType<MultilistField>()) return new Item[0];

         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            using (new EditContext(item))
            {
               MultilistField mlField = field;
               mlField.Remove(valueToRemove);
               return mlField.GetItems();
            }
         }
      }

      public static IEnumerable<Item> AddIdToMultilist(this Item hostItem, string fieldName, string newId, bool checkSecurity = false)
      {
         Assert.IsNotNull(hostItem, "hostItem cannot be null");
         Assert.IsNotNullOrEmpty(fieldName, "fieldName cannot be null or empty");
         Assert.IsNotNullOrEmpty(newId, "newId cannot be null or empty");
         Assert.IsTrue(ID.IsID(newId), "newId is not a proper GUID");

         var field = hostItem.Fields[fieldName];
         var targetItem = hostItem.Database.GetItem(newId);

         if (field != null && field.IsOfType<MultilistField>())
         {
            var state = SecurityState.Enabled;
            if (!checkSecurity) state = SecurityState.Disabled;

            using (new SecurityStateSwitcher(state))
            {
               using (new EditContext(hostItem))
               {
                  MultilistField mlField = field;
                  mlField.Add(targetItem.ID.ToString());
                  return mlField.GetItems();
               }
            }
         }

         return new Item[0];
      }

      public static void AddSecurityRules(this Item item, AccessRuleCollection addRules, bool checkSecurity = false)
      {
         Assert.IsNotNull(item, "item cannot be null");

         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            var accessRules = item.Security.GetAccessRules();
            accessRules.AddRange(addRules);
            item.Security.SetAccessRules(accessRules);
         }
      }

      public static void RemoveSecurityRule(this Item item, AccessRule right, bool checkSecurity = false)
      {
         Assert.IsNotNull(item, "item cannot be null");

         var state = SecurityState.Enabled;
         if (!checkSecurity) state = SecurityState.Disabled;

         using (new SecurityStateSwitcher(state))
         {
            var accessRules = item.Security.GetAccessRules();
            accessRules.Remove(right);
            item.Security.SetAccessRules(accessRules);
         }
      }
   }
}
