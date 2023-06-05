using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCaptureService
{
    internal static class MonitorValidator
    {
        public static bool ValidateArgs(string path, string format, out string errorMessage)
        {
            var stringBuilder = new StringBuilder();
            var result = true;

            if (!Directory.Exists(path))
            {
                stringBuilder.AppendLine("Path doesn't exist!");
                result = false;
            }

            if (format.Contains('.'))
            {
                stringBuilder.AppendLine("Format shouldn't have dots (.)!");
                result = false;
            }

            errorMessage = stringBuilder.ToString();

            return result;
        }
    }
}
