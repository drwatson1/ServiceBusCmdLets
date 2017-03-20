using Microsoft.ServiceBus;
using System.Management.Automation;

namespace ServiceBus.v1_1.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "ServiceBusTopics")]
    public class GetServiceBusTopics : ServiceBusObjectBase
    {
        protected override void EndProcessing()
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(Connection.ConnectionString);
            WriteObject(namespaceManager.GetTopics(), true);
        }
    }
}