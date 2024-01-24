$psi = New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName = "$env:SL_REFERENCES/../../LocalAdmin.exe"
$psi.Arguments = "7777"
$psi.WorkingDirectory = "$env:SL_REFERENCES/../../"
$psi.UseShellExecute = $false
$psi.RedirectStandardInput = $true

$pr = [System.Diagnostics.Process]::Start($psi)
Start-Sleep -s 5
$pr.StandardInput.WriteLine("yes")
Start-Sleep -s 2
$pr.StandardInput.WriteLine("keep")
Start-Sleep -s 2
$pr.StandardInput.WriteLine("global")
Start-Sleep -s 60
$pr.StandardInput.WriteLine("exit")
Start-Sleep -s 10

for ($i = 0; $i -lt 2; $i++) {
    dotnet test --no-build
    Start-Sleep -s 10
}

exit 0
