using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCaptureService
{
    internal static class MessagesHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileSize"></param>
        /// <param name="messageSize"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static int GetChunksNumber(long fileSize, long messageSize)
        {
            var division = fileSize / messageSize;
            var roundedValue = Math.Ceiling((decimal)division);

            if (roundedValue >= int.MaxValue)
            {
                throw new ArgumentException("File size is more than 1 GB");
            }

            return Convert.ToInt32(roundedValue);
        }
    }
}
