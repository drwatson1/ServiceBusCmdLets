using System.Management.Automation;

namespace ServiceBus.v1_1.CmdLets
{
    public class ServiceBusObjectBase : Cmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public ServiceBusConnection Connection { get; set; }
    }
}