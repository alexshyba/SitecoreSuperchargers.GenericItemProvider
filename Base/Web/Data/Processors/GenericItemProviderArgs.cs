using System;
using Sitecore.Data.Items;
using Sitecore.Pipelines;

namespace SitecoreSuperchargers.GenericItemProvider.Data.Processors
{
   [Serializable]
   public class GenericItemProviderArgs : PipelineArgs
   {
      public GenericItemProviderArgs(Item rootItem)
      {
         RootItem = rootItem;
      }

      public Item RootItem { get; set; }
   }
}