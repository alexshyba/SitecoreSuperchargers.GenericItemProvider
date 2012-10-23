using log4net;

namespace SitecoreSuperchargers.GenericItemProvider
{
   public static class Tracer
   {
      private static bool _enabled;

      static Tracer()
      {
         _enabled = Config.Tracing.Enabled;
      }

      private static bool IsEnabled { get { return _enabled; } }

      private static ILog Log { get { return LogManager.GetLogger("GIP.Tracer"); } }

      public static void Write(string message)
      {
         if (!IsEnabled) return;
         Log.Info(message);
      }
   }
}