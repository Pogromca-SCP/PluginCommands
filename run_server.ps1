$psi = New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName = "$env:SL_REFERENCES/../../LocalAdmin.exe"
$psi.Arguments = "7777"
$psi.WorkingDirectory = "$env:SL_REFERENCES/../../"
$psi.UseShellExecute = $false
$psi.RedirectStandardInput = $true

$pr = [System.Diagnostics.Process]::Start($psi)
Start-Sleep -s 5
$pr.StandardInput.WriteLine("yes")
Start-Sleep -s 30
$pr.StandardInput.WriteLine("exit")