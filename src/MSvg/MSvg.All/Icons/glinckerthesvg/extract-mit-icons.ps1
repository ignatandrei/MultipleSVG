param(
    [string]$InputPath = "src\data\icons.json",
    [string]$OutputPath = "src\data\icons.mit.json"
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
$resolvedInputPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $InputPath))
$resolvedOutputPath = [System.IO.Path]::GetFullPath((Join-Path $repoRoot $OutputPath))

if (-not (Test-Path -LiteralPath $resolvedInputPath)) {
    throw "Input file not found: $resolvedInputPath"
}

$outputDirectory = Split-Path -Parent $resolvedOutputPath
if (-not (Test-Path -LiteralPath $outputDirectory)) {
    New-Item -ItemType Directory -Path $outputDirectory | Out-Null
}

$icons = Get-Content -LiteralPath $resolvedInputPath -Raw | ConvertFrom-Json
$mitIcons = @($icons | Where-Object { $_.license -eq "MIT" })
$json = $mitIcons | ConvertTo-Json -Depth 100

$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText($resolvedOutputPath, "$json`n", $utf8NoBom)

Write-Host "Wrote $($mitIcons.Count) MIT-licensed icons to $resolvedOutputPath"
