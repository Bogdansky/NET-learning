using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProcessingService.Services
{
    internal interface IFileProcessor
    {
        Task ConsumeTopicMessagesAsync(CancellationToken token);
    }
}
