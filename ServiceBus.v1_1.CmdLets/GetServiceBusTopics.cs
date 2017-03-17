using Microsoft.ServiceBus;
using System.Management.Automation;
using System.Linq;

namespace ServiceBus.v1_1.CmdLets
{

    enum ParametersSet
    {
        CustomConnectionString,
        ConstructConnectionString
    }

    [Cmdlet(VerbsCommon.Get, "ServiceBusTopics", DefaultParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
    public class GetServiceBusTopics: Cmdlet
    {
        private string connectionString;

        protected override void EndProcessing()
        {
            var namespaceManager = NamespaceManager.CreateFromConnectionString(ConnectionString);
            WriteObject(namespaceManager.GetTopics(), true);
        }

        [Parameter(Mandatory =true, Position =0, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
        public string Host { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
        public string Namespace { get; set; } = "ServiceBusDefaultNamespace";

        [Parameter(Mandatory = false, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
        public int RuntimePort { get; set; } = 9354;

        [Parameter(Mandatory = false, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
        public int ManagementPort { get; set; } = 9355;

        [Parameter(Mandatory = false, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
        public string SharedAccessKeyName { get; set; } = "RootManageSharedAccessKey";

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
        public string SharedAccessKey { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = nameof(ParametersSet.CustomConnectionString), ValueFromPipeline =true)]
        public string ConnectionString
        {
            get
            {
                return connectionString ?? $"Endpoint=sb://{Host}/{Namespace};StsEndpoint=https://{Host}:{ManagementPort}/{Namespace};RuntimePort={RuntimePort};ManagementPort={ManagementPort};SharedAccessKeyName={SharedAccessKeyName};SharedAccessKey={SharedAccessKey}";
            }
            set
            {
                connectionString = value;
            }
        }
    }
}
 