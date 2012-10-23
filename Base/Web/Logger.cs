using System;
using Sitecore.Diagnostics;
using Sitecore.StringExtensions;
using log4net;

namespace SitecoreSuperchargers.GenericItemProvider
{
   public static class Logger
   {
      private static ILog Log { get { return LoggerFactory.GetLogger("GIP.Logger"); } }

      public static void Error(string message, Exception exception, params object[] parameters)
      {
         try
         {
            Log.Error(message.FormatWith(parameters) + " \n\nMessage: " + exception.Message + "\n\nDetails: " + exception.StackTrace);
         }
         catch (Exception ex)
         {
            Log.Error("Logger.Error failed with", ex);
         }
      }

      public static void Error(string message, params object[] parameters)
      {
         try
         {
            Log.Error(message.FormatWith(parameters));
         }
         catch (Exception ex)
         {
            Log.Error("Logger.Error failed with", ex);
         }
      }

      public static void Info(string message)
      {
         Log.Info(message);
      }
   }
}