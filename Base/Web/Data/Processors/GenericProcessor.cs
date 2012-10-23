using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Managers;
using Sitecore.Globalization;
using Sitecore.SecurityModel;
using Sitecore.StringExtensions;
using SitecoreSuperchargers.GenericItemProvider.Attributes;
using SitecoreSuperchargers.GenericItemProvider.Data.Providers;
using SitecoreSuperchargers.GenericItemProvider.Helpers;
using SitecoreSuperchargers.GenericItemProvider.Superchargers;

namespace SitecoreSuperchargers.GenericItemProvider.Data.Processors
{
   public abstract class GenericProcessor<T> : IItemProcessor
   {
      protected abstract Dictionary<string, string> LanguageMap { get; }
      protected abstract string EntityTemplateId { get; }
      protected abstract string ContainerTemplateId { get; }
      protected abstract IEnumerable<T> Data { get; }
      protected abstract int MaxFetch { get; }

      public void Process(GenericItemProviderArgs args)
      {
         var rootItem = args.RootItem;

         if (!rootItem.TemplateID.ToString().Equals(ContainerTemplateId)) return;

         if (!Switcher<bool, IntegrationDisabler>.CurrentValue && CacheHelper.IsExpired(rootItem))
         {
            CacheHelper.SetAsCached(rootItem);

            using (new IntegrationDisabler())
            {
               var startTime = DateTime.Now;

               var queryPath = rootItem.Paths.FullPath;
               var entityTemplate = rootItem.Database.GetTemplate(EntityTemplateId);

               using (new SecurityDisabler())
               {
                  using (new BulkUpdateContext())
                  {
                     foreach (var data in Data.Take(MaxFetch))
                     {
                        if (!(data is IEntity)) continue;

                        var entity = data as IEntity;

                        var entityName = ItemUtil.ProposeValidItemName(entity.GetItemName());

                        if (entityName.IsNullOrEmpty()) continue;

                        var language = ResolveLanguage(entity);

                        var path = string.Format("{0}{1}", StringUtil.EnsurePostfix('/', queryPath), entityName);
                        var startQuery = DateTime.Now;
                        var existingItem = rootItem.Database.GetItem(path);

                        if (existingItem != null && existingItem.HasVersions())
                        {
                           ProcessExisting(existingItem, entity, language, true);
                           continue;
                        }

                        Logger.Info("item not found creating : " + entityName);

                        ProcessNew(rootItem, entity, entityTemplate, language);

                        var endQuery = DateTime.Now;
                        var timeTaken = endQuery - startQuery;

                        Logger.Info(string.Format("query time : {0} ms for {1}", timeTaken.TotalMilliseconds.ToString(), path));
                     }
                  }
               }
               var endTime = DateTime.Now;

               Logger.Info("Contract Sync: Start Time {0}, End Time {1}".FormatWith(startTime.ToString(), endTime.ToString()));
            }
         }
      }

      protected Language ResolveLanguage(IEntity entity)
      {
         var entityLang = entity.GetLanguageName().TrimStart().TrimEnd();

         if (entityLang.IsNullOrEmpty()) return LanguageManager.DefaultLanguage;

         if (!LanguageMap.ContainsKey(entityLang)) return LanguageManager.DefaultLanguage;

         var scLanguage = LanguageMap[entityLang];

         if (scLanguage.IsNullOrEmpty()) return LanguageManager.DefaultLanguage;

         return scLanguage.ParseLanguage();
      }

      public void ProcessExisting(Item item, IEntity entity, Language language, bool append = false)
      {
         item = item.EnsureItemLanguage(language);

         var properties = entity.GetType().GetProperties();

         bool changeDetected;
         bool versionAdded = false;

         foreach (var property in properties)
         {
            var fieldID = GetFieldID(property);

            if (fieldID.IsNullOrEmpty()) continue;

            var propertyValue = GetPropertyValue(property, entity);

            changeDetected = !item[fieldID].Equals(propertyValue);

            if (append && changeDetected && !versionAdded)
            {
               item = item.Versions.AddVersion();
               versionAdded = true;
            }

            using (new EditContext(item))
            {
               item.Fields[fieldID].SetValue(propertyValue, false);
            }
         }
      }

      private string GetPropertyValue(PropertyInfo property, IEntity entity)
      {
         var mapping = GetFieldMapping(property);

         var rawValue = property.GetValue(entity, null).ToString();

         if (mapping is IConvertibleAttribute)
         {
            var convertibleMapping = mapping as IConvertibleAttribute;

            return convertibleMapping.Convert(rawValue);
         }

         return rawValue;
      }

      private FieldMapping GetFieldMapping(PropertyInfo property)
      {
         var attributes = property.GetCustomAttributes(typeof(FieldMapping), true);

         return attributes.Length <= 0 ? null : attributes[0] as FieldMapping;
      }

      private string GetFieldID(PropertyInfo property)
      {
         var mapping = GetFieldMapping(property);

         if (mapping == null)
         {
            return String.Empty;
         }

         return mapping.FieldId.IsNullOrEmpty() ? String.Empty : mapping.FieldId;
      }

      public void ProcessNew(Item rootItem, IEntity entity, TemplateItem template, Language language)
      {
         var langRootItem = rootItem.EnsureItemLanguage(language);
         var newItem = langRootItem.Add(ItemUtil.ProposeValidItemName(entity.GetItemName()), template);
         ProcessExisting(newItem, entity, language);
      }
   }
}
