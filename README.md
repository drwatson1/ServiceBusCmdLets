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

$topics = Get-ServiceBusTopics $connection

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

