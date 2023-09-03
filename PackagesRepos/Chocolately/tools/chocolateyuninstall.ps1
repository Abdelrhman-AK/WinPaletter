$ErrorActionPreference = 'Stop' # stop on all errors

$appdir = Join-Path -Path ${env:ProgramFiles} -ChildPath '\Abdelrhman-AK\WinPaletter'
$app = Join-Path -Path $appdir -ChildPath '\WinPaletter.exe'
$desktoplnk = [Environment]::GetFolderPath("Desktop") + '\WinPaletter.lnk'
$startdir = [Environment]::GetFolderPath('CommonStartMenu') + "\Programs\WinPaletter"

Start-Process -FilePath $app -ArgumentList '/uninstall' -Wait

Uninstall-ChocolateyZipPackage 'WinPaletter' 'WinPaletter.zip'

if (Test-Path $appdir) {Remove-Item $appdir -verbose}
if (Test-Path $startdir) {Remove-Item $startdir -verbose}
if (Test-Path $desktoplnk) {Remove-Item $desktoplnk -verbose}