using System.Management.Automation;

namespace ServiceBus.v1_1.CmdLets
{
    public class ServiceBusConnection
    {
        internal ServiceBusConnection()
        {
        }

        public string Host { get; internal set; }

        public string Namespace { get; internal set; } = "ServiceBusDefaultNamespace";

        public int RuntimePort { get; internal set; } = 9354;

        public int ManagementPort { get; internal set; } = 9355;

        public string SharedAccessKeyName { get; internal set; } = "RootManageSharedAccessKey";

        public string SharedAccessKey { get; internal set; }

        public string ConnectionString { get; internal set; }
    }
}
