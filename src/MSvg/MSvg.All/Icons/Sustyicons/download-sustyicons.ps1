param(
    [string]$OutputDirectory = "icons",
    [string]$LicensePath = "LICENSE",
    [string]$Owner = "codewordcreative",
    [string]$Repository = "susty-icons",
    [string]$Branch = "main",
    [string]$IconsPath = "source-icons",
    [string]$TreeFilePath = "",
    [string]$SourceRootPath = "",
    [string]$LicenseSourcePath = ""
)

$ErrorActionPreference = "Stop"

$headers = @{
    "Accept" = "application/vnd.github+json"
    "User-Agent" = "MSvg-Sustyicons-Downloader"
}

function Resolve-ScriptPath([string]$PathValue) {
    if ([System.IO.Path]::IsPathRooted($PathValue)) {
        return [System.IO.Path]::GetFullPath($PathValue)
    }

    return [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot $PathValue))
}

$targetOutputDirectory = Resolve-ScriptPath $OutputDirectory
$targetLicensePath = Resolve-ScriptPath $LicensePath
$targetLicenseDirectory = Split-Path -Parent $targetLicensePath

if (-not (Test-Path -LiteralPath $targetOutputDirectory)) {
    New-Item -ItemType Directory -Path $targetOutputDirectory | Out-Null
}

if (-not (Test-Path -LiteralPath $targetLicenseDirectory)) {
    New-Item -ItemType Directory -Path $targetLicenseDirectory | Out-Null
}

if ([string]::IsNullOrWhiteSpace($TreeFilePath)) {
    $treeUrl = "https://api.github.com/repos/$Owner/$Repository/git/trees/$Branch?recursive=1"
    $treeResponse = Invoke-RestMethod -Uri $treeUrl -Headers $headers
}
else {
    $resolvedTreeFilePath = Resolve-ScriptPath $TreeFilePath
    $treeResponse = Get-Content -LiteralPath $resolvedTreeFilePath -Raw | ConvertFrom-Json
}

$resolvedSourceRootPath = ""
if (-not [string]::IsNullOrWhiteSpace($SourceRootPath)) {
    $resolvedSourceRootPath = Resolve-ScriptPath $SourceRootPath
}

$resolvedLicenseSourcePath = ""
if (-not [string]::IsNullOrWhiteSpace($LicenseSourcePath)) {
    $resolvedLicenseSourcePath = Resolve-ScriptPath $LicenseSourcePath
}

$icons = @($treeResponse.tree | Where-Object {
    $_.type -eq "blob" -and
    $_.path.StartsWith("$IconsPath/") -and
    $_.path.EndsWith(".svg")
})

if ($icons.Count -eq 0) {
    throw "No SVG icons found at '$IconsPath' in $Owner/$Repository."
}

foreach ($icon in $icons) {
    $relativePath = $icon.path.Substring($IconsPath.Length + 1)
    $destinationPath = Join-Path $targetOutputDirectory $relativePath
    $destinationDirectory = Split-Path -Parent $destinationPath

    if (-not (Test-Path -LiteralPath $destinationDirectory)) {
        New-Item -ItemType Directory -Path $destinationDirectory | Out-Null
    }

    if ([string]::IsNullOrWhiteSpace($SourceRootPath)) {
        $rawUrl = "https://raw.githubusercontent.com/$Owner/$Repository/$Branch/$($icon.path)"
        Invoke-WebRequest -Uri $rawUrl -Headers $headers -OutFile $destinationPath
    }
    else {
        $sourcePath = Join-Path $resolvedSourceRootPath $icon.path
        Copy-Item -LiteralPath $sourcePath -Destination $destinationPath -Force
    }
}

if ([string]::IsNullOrWhiteSpace($LicenseSourcePath)) {
    $licenseUrl = "https://raw.githubusercontent.com/$Owner/$Repository/$Branch/LICENSE"
    Invoke-WebRequest -Uri $licenseUrl -Headers $headers -OutFile $targetLicensePath
}
else {
    Copy-Item -LiteralPath $resolvedLicenseSourcePath -Destination $targetLicensePath -Force
}

$downloadedIcons = @(Get-ChildItem -LiteralPath $targetOutputDirectory -Recurse -File -Filter "*.svg")
if ($downloadedIcons.Count -ne $icons.Count) {
    throw "Verification failed: expected $($icons.Count) SVG files, downloaded $($downloadedIcons.Count)."
}

$licenseContent = Get-Content -LiteralPath $targetLicensePath -Raw
if (-not $licenseContent.Contains("Copyright Notice") -or -not $licenseContent.Contains("Permission is hereby granted")) {
    throw "Verification failed: downloaded license does not contain expected terms."
}

Write-Host "Downloaded $($downloadedIcons.Count) SVG files to $targetOutputDirectory"
Write-Host "Downloaded and verified license at $targetLicensePath"
