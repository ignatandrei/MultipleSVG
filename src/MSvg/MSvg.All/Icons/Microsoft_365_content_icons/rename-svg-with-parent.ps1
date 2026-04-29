[CmdletBinding(SupportsShouldProcess)]
param(
    [Parameter(Position = 0)]
    [string]$RootPath = (Get-Location).Path
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

function Get-ParentSuffix {
    param(
        [Parameter(Mandatory)]
        [string]$ParentName
    )

    $suffix = $ParentName -replace '^\s*48x48\s*', ''
    $suffix = $suffix.Trim()

    if ([string]::IsNullOrWhiteSpace($suffix)) {
        return $ParentName.Trim()
    }

    return $suffix
}

$svgFiles = Get-ChildItem -Path $RootPath -Filter *.svg -File -Recurse

foreach ($file in $svgFiles) {
    $parentSuffix = Get-ParentSuffix -ParentName $file.Directory.Name
    $targetBaseName = "$($file.BaseName) - $parentSuffix"

    if ($file.BaseName -eq $targetBaseName) {
        Write-Host "Skipping already-renamed file: $($file.FullName)"
        continue
    }

    $targetPath = Join-Path -Path $file.Directory.FullName -ChildPath ($targetBaseName + $file.Extension)

    if ($file.FullName -eq $targetPath) {
        Write-Host "Skipping already-renamed file: $($file.FullName)"
        continue
    }

    if (Test-Path -LiteralPath $targetPath) {
        Write-Warning "Skipping because target already exists: $targetPath"
        continue
    }

    if ($PSCmdlet.ShouldProcess($file.FullName, "Rename to $targetBaseName$($file.Extension)")) {
        Rename-Item -LiteralPath $file.FullName -NewName ($targetBaseName + $file.Extension)
    }
}
