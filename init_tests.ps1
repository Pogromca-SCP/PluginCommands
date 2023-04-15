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
dotnet test --no-build --verbosity normal

if ( !$? ) {
    Start-Sleep -s 300
    Exit 0
}

Start-Sleep -s 300
