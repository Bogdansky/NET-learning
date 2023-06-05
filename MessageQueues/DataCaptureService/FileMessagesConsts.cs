using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCaptureService
{
    internal class FileMessagesConsts
    {
        public const string DefaultDirectory = "\\files";
        public const string DefaultFormat = "pdf";
        public const long DataTransferSizeInBytes = 1024;
        public const string Topic = "file-messages";
    }
}
