[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$path,
    [Parameter(Mandatory=$true)]
    $serverFQDN,
    [Parameter(Mandatory=$true)]
    $sharedAccessKey,
    [string]$logFileName = "servicebus",
    [string]$maxFileSize = "10m",
    [int]$maxFiles = 10,
    [bool]$skipEmptySubscriptions
)

[string]$logFileExt = ""
[int]$fileSize = 0

function NormalizeMaxSize()
{
    if( $Script:maxFileSize.ToLower().EndsWith("m") )
    {
        $Script:fileSize = [int]::Parse($Script:maxFileSize.TrimEnd('M', 'm')) * 1024 * 1024
    }
    else
    {
        if($Script:maxFileSize.ToLower().EndsWith("k") )
        {
            $Script:fileSize = [int]::Parse($Script:maxFileSize.TrimEnd('K', 'k')) * 1024
        }
        else {
            $Script:fileSize = [int]::Parse($Script:maxFileSize)
        }
    }
}

function NormalizeFileName()
{
    $parts = $Script:logFileName.Split(".")
    if( $parts.Length -eq 1)
    {
        $Script:logFileExt = ".log"
    }
    else
    {
        $Script:logFileExt = "." + $parts[$parts.Length-1]
        $Script:logFileName = $logFileName.Replace($logFileExt, "")
    }

    $Script:path = $path.TrimEnd('\', '/')
}

# TODO: maxFileSize should be > 0
# TODO: maxFiles can be = 0 or 1 or greater

function GetLogFilePath($number)
{
    if( $number -eq $null )
    {
        return $Script:path + "\" + $Script:logFileName + $Script:logFileExt
    }
    else
    {
        return $Script:path + "\" + $Script:logFileName + "." + $number.ToString() + $Script:logFileExt
    }
}

function RollArchiveFiles()
{
    $files = Get-ChildItem $Script:path ( "*.*" + $Script:logFileExt) | % {
        $parts = $_.Name.Split('.')
        [int]$n = [int]::Parse($parts[$parts.Length-2])
        [PSCustomObject]@{
            File = $_
            Number = $n
        }
    } | sort -Property "Number" -Descending | % {
        $newName = GetLogFilePath ($_.Number + 1)
        Rename-Item $_.File.FullName $newName
        $_.File = Get-Item $newName
        $_.Number = $_.Number + 1
        $_
    }

    if( $files.Length -ge $Script:maxFiles )
    {
        Remove-Item $files[0].File.FullName
    }
}

function LogRollover()
{
    if( -not (Test-Path (GetLogFilePath) -PathType Leaf) )
    {
        return
    }

    $file = Get-Item (GetLogFilePath)
    if( $file.Length -gt $Script:fileSize )
    {
        RollArchiveFiles
        Rename-Item (GetLogFilePath) (GetLogFilePath 1)
    }
}

NormalizeMaxSize
NormalizeFileName
LogRollover

.\write-sb-dump.ps1 $serverFQDN $sharedAccessKey | Out-File (GetLogFilePath) -Append -Encoding utf8
