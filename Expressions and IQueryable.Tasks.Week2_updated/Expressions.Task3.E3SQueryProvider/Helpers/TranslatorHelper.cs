using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider.Helpers
{
    internal static class TranslatorHelper
    {
        const string Root = "\"statements\": [{0}]";

        public static string TransformAndQueriesToJson(List<string> queries)
        {
            var formattedQueries = queries.Select(query => $"{{\"query\":\"{query}\"}}");

            return string.Format(Root, string.Join(',', formattedQueries));
        }
    }
}
