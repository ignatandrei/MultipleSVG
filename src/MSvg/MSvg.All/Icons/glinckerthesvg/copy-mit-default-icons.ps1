param(
    [string]$InputPath = "src\data\icons.mit.default-paths.json",
    [string]$SourceRoot = "public",
    [string]$OutputDirectory = "public\icons-mit-default"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$resolvedInputPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $InputPath))
$resolvedSourceRoot = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $SourceRoot))
$resolvedOutputDirectory = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $OutputDirectory))

if (-not (Test-Path -LiteralPath $resolvedInputPath)) {
    throw "Input file not found: $resolvedInputPath"
}

if (-not (Test-Path -LiteralPath $resolvedSourceRoot)) {
    throw "Source root not found: $resolvedSourceRoot"
}

if (-not (Test-Path -LiteralPath $resolvedOutputDirectory)) {
    New-Item -ItemType Directory -Path $resolvedOutputDirectory | Out-Null
}

$iconPaths = Get-Content -LiteralPath $resolvedInputPath -Raw | ConvertFrom-Json
$copiedCount = 0

foreach ($iconPath in $iconPaths) {
    if (-not ($iconPath -is [string]) -or [string]::IsNullOrWhiteSpace($iconPath)) {
        throw "Encountered an invalid icon path entry."
    }

    if (-not $iconPath.StartsWith("/icons/")) {
        throw "Expected icon path to start with '/icons/': $iconPath"
    }

    $relativePath = $iconPath.TrimStart('/') -replace '/', '\'
    $sourcePath = Join-Path $resolvedSourceRoot $relativePath
    $flattenedFileName = ($relativePath -replace '^icons\\', '') -replace '\\', '_'
    $destinationPath = Join-Path $resolvedOutputDirectory $flattenedFileName

    if (-not (Test-Path -LiteralPath $sourcePath)) {
        throw "Source icon not found: $sourcePath"
    }

    Copy-Item -LiteralPath $sourcePath -Destination $destinationPath -Force
    $copiedCount++
}

Write-Host "Copied $copiedCount icons to $resolvedOutputDirectory"
