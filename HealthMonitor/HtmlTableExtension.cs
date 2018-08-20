using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HealthMonitor
{
    public static class HtmlTableExtension
    {
        public static string ToHtmlTable<T>(this List<T> listOfClassObjects, List<String> allowedHeaders)
        {
            var ret = string.Empty;

            return listOfClassObjects == null || !listOfClassObjects.Any()
                ? ret
                : "<table>" +
                  listOfClassObjects.First().GetType().GetProperties()
                                    .Select(p => p.Name)
                                    .ToList()
                                    .Where(i => allowedHeaders.Contains(i))
                                    .ToList()
                                    .ToColumnHeaders() +
                  listOfClassObjects.Aggregate(ret, (current, t) => current + t.ToHtmlTableRow(allowedHeaders)) +
                  "</table>";
        }

        public static string ToColumnHeaders<T>(this List<T> listOfProperties)
        {
            var ret = string.Empty;

            return listOfProperties == null || !listOfProperties.Any()
                ? ret
                : "<tr>" +
                  listOfProperties.Aggregate(ret,
                      (current, propValue) =>
                          current +
                          ("<th style='font-size: 8pt; font-weight: bold;'>" +
                           (Convert.ToString(propValue).Length <= 100
                               ? Convert.ToString(propValue)
                               : Convert.ToString(propValue).Substring(0, 100)) + "..." + "</th>")) +
                  "</tr>";
        }

        static bool alternate = true;
        public static string ToHtmlTableRow<T>(this T classObject, List<string> allowedHeaders)
        {
            var ret = string.Empty;
            alternate = !alternate;
            var tr = alternate ? "<tr style='border: 0.5pt solid black;background-color:#FEF9E7'>" :
                            "<tr style='border: 0.5pt solid black;background-color:#F7F9F9'>";
            return classObject == null
                ? ret
                : tr +
                  classObject.GetType()
                      .GetProperties()
                      .Where(p => allowedHeaders.Contains(p.Name))
                      .Aggregate(ret,
                          (current, prop) =>
                              current + ("<td style='font-size: 8pt; font-weight: normal;'>" +
                                         (Convert.ToString(prop.GetValue(classObject, null)).Length <= 100
                                             ? Convert.ToString(prop.GetValue(classObject, null))
                                             : Convert.ToString(prop.GetValue(classObject, null)).Substring(0, 100) +
                                               "...") +
                                         "</td>")) + "</tr>";
        }
    }
}
