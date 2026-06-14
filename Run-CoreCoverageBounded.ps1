param(
    [int]$MinLineCoverage = 25,
    [int]$MaxLineCoverage = 30
)

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$resultsDir = Join-Path $repoRoot "TestResults\core-bounded"
$coverageFile = Join-Path $resultsDir "coverage.cobertura.xml"

if (Test-Path $resultsDir) {
    Remove-Item -Path $resultsDir -Recurse -Force
}

dotnet test (Join-Path $repoRoot "LegacyCRM.Core.Tests\LegacyCRM.Core.Tests.csproj") `
    --nologo `
    /p:CollectCoverage=true `
    /p:CoverletOutputFormat=cobertura `
    /p:CoverletOutput="$resultsDir\" `
    /p:Include="[LegacyCRM.Core]LegacyCRM.Core.Models.*%2c[LegacyCRM.Core]LegacyCRM.Core.CrmDbContext" `
    /p:Threshold=$MinLineCoverage `
    /p:ThresholdType=line `
    /p:ThresholdStat=total

if ($LASTEXITCODE -ne 0) {
    throw "dotnet test failed with exit code $LASTEXITCODE."
}

if (-not (Test-Path $coverageFile)) {
    throw "Coverage report not found at $coverageFile"
}

[xml]$coverageXml = Get-Content -Path $coverageFile
$lineCoverage = [math]::Round(([double]$coverageXml.coverage.'line-rate') * 100, 2)

if ($lineCoverage -gt $MaxLineCoverage) {
    throw "Line coverage $lineCoverage% exceeds max bound of $MaxLineCoverage%."
}

Write-Host "Bounded line coverage: $lineCoverage% (allowed: $MinLineCoverage-$MaxLineCoverage%)."
