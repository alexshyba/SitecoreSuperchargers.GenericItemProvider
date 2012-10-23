namespace SitecoreSuperchargers.GenericItemProvider.Helpers
{
   public static class QueryUtil
   {
      public static string[] InvalidQueryTokens = new[] { " and ", " or ", "-" };

      public static string EscapeQuery(this string sourceQuery)
      {
         var query = sourceQuery;

         foreach (var queryPart in sourceQuery.Split('/'))
         {
            foreach (var token in InvalidQueryTokens)
            {
               if (queryPart.Contains(token) && !queryPart.Contains("[") && !queryPart.Contains("]"))
               {
                  query = query.Replace(queryPart, "#" + queryPart + "#");
               }
            }
         }

         return query;
      }
   }
}
