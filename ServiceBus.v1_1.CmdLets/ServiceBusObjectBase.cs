using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.v1_1.CmdLets
{
    public class ServiceBusObjectBase: Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ServiceBusConnection Connection { get; set; }
    }
}
