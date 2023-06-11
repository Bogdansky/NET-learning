using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlteringBehavior.Interfaces;

namespace AlteringBehavior.Services
{
    public class LoggingService : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
