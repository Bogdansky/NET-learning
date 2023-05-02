using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProcessingService.Models
{
    internal class FileCreatedMessage
    {
        public string Name { get; set; }

        public FileCreatedMessage(string name)
        {
            Name = name;
        }
    }
}
