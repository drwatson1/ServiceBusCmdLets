using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System.Management.Automation;

namespace ServiceBus.v1_1.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "ServiceBusSubscriptions")]
    public class GetServiceBusSubscriptions : ServiceBusObjectBase
    {
        protected override void EndProcessing()
        {
            //MessagingFactory messagingFactory = MessagingFactory.Create();
            var namespaceManager = NamespaceManager.CreateFromConnectionString(Connection.ConnectionString);
            //namespaceManager.GetTopics().First().MessageCountDetails.
            WriteObject(namespaceManager.GetSubscriptions(Topic.Path), true);
        }

        [Parameter(Mandatory = true, Position = 1)]
        public TopicDescription Topic { get; set; }
    }
}