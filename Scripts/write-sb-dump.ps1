[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$server,
    [Parameter(Mandatory=$true)]
    [string]$sha,
    [Parameter(Mandatory=$false)]
    [bool]$skipEmptySubscriptions
)

Import-Module .\ServiceBus.v1_1.CmdLets.dll

$connection = New-ServiceBusConnection $server $sha
$topics = Get-ServiceBusTopics $connection

$time = [System.DateTime]::Now

$topics | sort $_path | % {
    $topic = $_
    $subscriptions = Get-ServiceBusSubscriptions $connection $_
    $subscriptions | sort $_.name | % {
        $skip = $_.MessageCountDetails.ActiveMessageCount -eq 0 -and $_.MessageCountDetails.DeadLetterMessageCount -eq 0 -and $skipEmptySubscriptions
        if( $skip -eq $false )
        {
            Write-Output ($time.ToString() + " - "  + $topic.path + ": " + $_.name + ": " + $_.MessageCountDetails.ActiveMessageCount + " (" + $_.MessageCountDetails.DeadLetterMessageCount + ")")
        }
    }
}
