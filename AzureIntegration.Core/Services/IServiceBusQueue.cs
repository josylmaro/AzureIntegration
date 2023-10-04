using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIntegration.Core.Services
{
    public interface IServiceBusQueue
    {
        public Task<string> Queue(string message);
        public Task<string> Queue(string topic, string message);
    }
}
