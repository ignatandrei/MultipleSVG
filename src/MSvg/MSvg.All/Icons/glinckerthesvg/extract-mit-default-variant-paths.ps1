param(
    [string]$InputPath = "src\data\icons.mit.json",
    [string]$OutputPath = "src\data\icons.mit.default-paths.json"
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
$defaultVariantPaths = @($icons | ForEach-Object {
    if (-not $_.variants -or -not $_.variants.default) {
        throw "Missing variants.default for icon: $($_.slug)"
    }

    $_.variants.default
})

$json = $defaultVariantPaths | ConvertTo-Json -Depth 10
$utf8NoBom = New-Object System.Text.UTF8Encoding($false)
[System.IO.File]::WriteAllText($resolvedOutputPath, "$json`n", $utf8NoBom)

Write-Host "Wrote $($defaultVariantPaths.Count) default variant paths to $resolvedOutputPath"
