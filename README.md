# ServiceBus Commandlets for PowerShell scripts

This is a very simple set of PowerShell cmdlets to enumerate and read Windows (Azure) ServiceBus topics and theirs subscriptions. 
You can use this code as you want without any restrictions. Please let me know if you needed in some more features, open issue or create pull request to contribute.

Here is minimal usage example.

1. Download latest version
2. Load module in your powershell script or environment

```powershell
Import-Module .\ServiceBus.v1_1.CmdLets.dll
```

3. Create connection to your ServiceBus:

```powershell
$connection = New-ServiceBusConnection "host name or IP-address" "Shared access key"
```

or provide connection string:

```powershell
$connection = New-ServiceBusConnection -ConnectionString "connection string"
```

4. Get all topics. This returns IEnumerable of [TopicDescription](https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.messaging.topicdescription)

```powershell
$topics = Get-ServiceBusTopics $connection
```

5. Use topics somehow:

```powershell
$topics | sort $_path | % { Write-Host $_.path }
```

6. Enumerate topic's subscriptions ([SubscriptionDescription](https://docs.microsoft.com/en-us/dotnet/api/microsoft.servicebus.messaging.subscriptiondescription))

```powershell
$topics | sort $_path | % {
    Write-Host $_.path
    $subscriptions = Get-ServiceBusSubscriptions $connection $_
    $subscriptions | sort $_.name | % {
        Write-Host ("    " + $_.name + ": " + $_.MessageCountDetails.ActiveMessageCount + " (" + $_.MessageCountDetails.DeadLetterMessageCount + ")" )
    }
}
```

This code will generate something like this:

```
mvk-dev/wellsservice/bypasspipechanged
    WellsService-9f91d24: 0 (0)
    SyncService-b7b7db9-BypassMapSymantic: 5 (0)
mvk-dev/wellsservice/bypassmeasurementchanged
    WellsService-9f91d24: 8 (0)
    SyncService-b7b7db9-BypassMapSymantic: 0 (15)
mvk-dev/regulatorzonecalculated
    SyncService-b7b7db9: 0 (0)
mvk-dev/pumpsumpservice/pumpsumpchanged
    SyncService-b7b7db9-PumpSegmentMapSymantic: 0 (0)
    SyncService-b7b7db9-PumpSumpMapSymantic: 4 (0)
    SyncService-b7b7db9-SumpDistrictMapSymantic: 0 (0)
    SyncService-b7b7db9-PumpPointMapSymantic: 1 (0)
    NTObjectChangeListener-f158ad0: 0 (8)
    PumpSumpService-4370260: 0 (0)
    STObjectChangeListener-c6af841: 3 (0)
```