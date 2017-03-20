using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.v1_1.CmdLets
{
    [Cmdlet(VerbsCommon.Get, "ServiceBusSubscriptions")]
    public class GetServiceBusSubscriptions: ServiceBusObjectBase
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
