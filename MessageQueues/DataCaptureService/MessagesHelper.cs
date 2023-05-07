using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCaptureService
{
    internal static class MessagesHelper
    {
        public static long GetChunksNumber(long fileSize, long messageSize)
        {
            var division = fileSize / messageSize;
            var roundedValue = Math.Ceiling((decimal)division);

            return Convert.ToInt64(roundedValue);
        }
    }
}
