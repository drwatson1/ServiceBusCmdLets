using System.Management.Automation;

namespace ServiceBus.v1_1.CmdLets
{
    enum ParametersSet
    {
        CustomConnectionString,
        ConstructConnectionString
    }

    [Cmdlet(VerbsCommon.New, "ServiceBusConnection", DefaultParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
    public class NewServiceBusConnection : Cmdlet
    {
        private string connectionString;

        protected override void EndProcessing()
        {
            var connection = new ServiceBusConnection()
            {
                Host = Host,
                Namespace = Namespace,
                RuntimePort = RuntimePort,
                ManagementPort = ManagementPort,
                SharedAccessKeyName = SharedAccessKeyName,
                SharedAccessKey = SharedAccessKey,
                ConnectionString = ConnectionString
            };

            WriteObject(connection);
        }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = nameof(ParametersSet.ConstructConnectionString))]
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

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = nameof(ParametersSet.CustomConnectionString), ValueFromPipeline = true)]
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