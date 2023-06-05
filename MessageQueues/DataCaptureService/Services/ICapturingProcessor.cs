using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCaptureService.Services
{
    internal interface ICapturingProcessor
    {
        Task MonitorFolderAsync(CancellationToken token);
    }
}
