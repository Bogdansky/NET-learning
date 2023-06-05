using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class FileTransferMessage
    {
        public string Name { get; }
        public long Position { get; }
        public byte[] Data { get; }

        public FileTransferMessage(string name, long position, byte[] data)
        { 
            Name = name; 
            Position = position;
            Data = data; 
        }
    }
}
