using System.Collections.Generic;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using SitecoreSuperchargers.GenericItemProvider.Helpers;

namespace SitecoreSuperchargers.GenericItemProvider
{
   public static class Config
   {
      private static IEnumerable<string> _supportedDatabases;

      public static class Tracing
      {
         public static bool Enabled
         {
            get
            {
               return Settings.GetBoolSetting("GIP.Tracing.Enabled", false);
            }
         }
      }

      public static string DataFolder
      {
         get { return StringUtil.EnsurePostfix('/', Settings.DataFolder) + "gip"; }
      }

      public static string LogFolder
      {
         get { return StringUtil.EnsurePostfix('/', DataFolder) + "logs"; }
      }

      public static Database ContentDatabase
      {
         get
         {
            return Context.ContentDatabase ?? Context.Database;
         }
      }

      public static IEnumerable<string> SupportedDatabases
      {
         get
         {
            if (_supportedDatabases != null) return _supportedDatabases;
            _supportedDatabases = Parser.ParseString(Settings.GetSetting("GIP.SupportedDatabases"));
            return _supportedDatabases;
         }
      }
   }
}