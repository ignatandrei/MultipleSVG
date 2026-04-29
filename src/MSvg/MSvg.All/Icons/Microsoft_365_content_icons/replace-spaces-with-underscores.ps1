[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Position = 0)]
    [string]$RootPath = (Get-Location).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$files = Get-ChildItem -Path $RootPath -File -Recurse

foreach ($file in $files) {
    $targetName = $file.Name -replace ' ', '_'
    $targetName = $targetName -replace '&', '-'
    $targetName = $targetName -replace '\&', '-'
    
    $targetName = $targetName -replace '[ &]', '-'
    $targetName = $targetName -replace '\(1\)', '_'
    $targetName = $targetName -replace '\(2\)', '_'
    $targetName = $targetName -replace '_-_', '_'
    $targetName = $targetName -replace '_-_', '_'
    if ($file.Name -eq $targetName) {
        continue
    }

    $targetPath = Join-Path -Path $file.Directory.FullName -ChildPath $targetName

    if (Test-Path -LiteralPath $targetPath) {
        Write-Warning "Skipping because target already exists: $targetPath"
        continue
    }

    if ($PSCmdlet.ShouldProcess($file.FullName, "Rename to $targetName")) {
        Rename-Item -LiteralPath $file.FullName -NewName $targetName
    }
}
