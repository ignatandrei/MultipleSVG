param(
    [string]$OutputDirectory = "icons",
    [string]$LicensePath = "LICENSE",
    [string]$Owner = "codewordcreative",
    [string]$Repository = "susty-icons",
    [string]$Branch = "main",
    [string]$IconsPath = "source-icons",
    [string]$TreeFilePath = "",
    [string]$SourceRootPath = "",
    [string]$LicenseSourcePath = "",
    [string]$GitHubToken = $env:GITHUB_TOKEN
)

$ErrorActionPreference = "Stop"

$headers = @{
    "Accept" = "application/vnd.github+json"
    "User-Agent" = "MSvg-SustyIcons-Downloader"
}

if (-not [string]::IsNullOrWhiteSpace($GitHubToken)) {
    $headers["Authorization"] = "Bearer $GitHubToken"
}

$requiredLicenseTerms = @(
    "Copyright Notice",
    "Permission is hereby granted"
)

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
    try {
        $treeResponse = Invoke-RestMethod -Uri $treeUrl -Headers $headers
    }
    catch {
        $authState = if ([string]::IsNullOrWhiteSpace($GitHubToken)) { "no GitHub token was provided" } else { "a GitHub token was provided" }
        throw "Failed to fetch repository tree from $treeUrl. Check repository/branch values, connectivity, rate limiting, and token permissions ($authState)."
    }
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
    $relativePath = $icon.path -replace ("^{0}/" -f [regex]::Escape($IconsPath)), ""
    $destinationPath = Join-Path $targetOutputDirectory $relativePath
    $destinationDirectory = Split-Path -Parent $destinationPath

    if (-not (Test-Path -LiteralPath $destinationDirectory)) {
        New-Item -ItemType Directory -Path $destinationDirectory | Out-Null
    }

    if ([string]::IsNullOrWhiteSpace($SourceRootPath)) {
        $rawUrl = "https://raw.githubusercontent.com/$Owner/$Repository/$Branch/$($icon.path)"
        try {
            Invoke-WebRequest -Uri $rawUrl -Headers $headers -OutFile $destinationPath
        }
        catch {
            throw "Failed to download icon '$($icon.path)' from $rawUrl."
        }
    }
    else {
        $sourcePath = Join-Path $resolvedSourceRootPath $icon.path
        try {
            Copy-Item -LiteralPath $sourcePath -Destination $destinationPath -Force
        }
        catch {
            throw "Failed to copy local icon '$sourcePath' to '$destinationPath'."
        }
    }
}

if ([string]::IsNullOrWhiteSpace($LicenseSourcePath)) {
    $licenseUrl = "https://raw.githubusercontent.com/$Owner/$Repository/$Branch/LICENSE"
    try {
        Invoke-WebRequest -Uri $licenseUrl -Headers $headers -OutFile $targetLicensePath
    }
    catch {
        throw "Failed to download license from $licenseUrl."
    }
}
else {
    try {
        Copy-Item -LiteralPath $resolvedLicenseSourcePath -Destination $targetLicensePath -Force
    }
    catch {
        throw "Failed to copy local license '$resolvedLicenseSourcePath' to '$targetLicensePath'."
    }
}

$downloadedIcons = @(Get-ChildItem -LiteralPath $targetOutputDirectory -Recurse -File -Filter "*.svg")
if ($downloadedIcons.Count -ne $icons.Count) {
    throw "Verification failed: expected $($icons.Count) SVG files, downloaded $($downloadedIcons.Count)."
}

$licenseContent = Get-Content -LiteralPath $targetLicensePath -Raw
$missingLicenseTerms = @($requiredLicenseTerms | Where-Object { -not $licenseContent.Contains($_) })
if ($missingLicenseTerms.Count -gt 0) {
    throw "Verification failed: downloaded license is missing expected term(s): $($missingLicenseTerms -join ', ')."
}

Write-Host "Downloaded $($downloadedIcons.Count) SVG files to $targetOutputDirectory"
Write-Host "Downloaded and verified license at $targetLicensePath"
