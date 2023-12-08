$ErrorActionPreference = 'Stop' # stop on all errors

$desktopdir = [Environment]::GetFolderPath("Desktop")
$startdir = [Environment]::GetFolderPath('CommonStartMenu') + "\Programs\WinPaletter"
$appdir = Join-Path -Path ${env:ProgramFiles} -ChildPath '\Abdelrhman-AK\WinPaletter'
$app = Join-Path -Path $appdir -ChildPath '\WinPaletter.exe'

Start-ChocolateyProcessAsAdmin -Statements "/uninstall" -ExeToRun $app -NoSleep -ValidExitCodes @(-1,0)

Uninstall-ChocolateyZipPackage 'WinPaletter' 'WinPaletter.zip'

$shortcutPath0 = $desktopdir + "\WinPaletter.lnk"
$shortcutPath1 = $startdir + "\WinPaletter.lnk"
if (Test-Path $shortcutPath0) { Remove-Item -Path $shortcutPath0 -Force }
if (Test-Path $shortcutPath1) { Remove-Item -Path $shortcutPath1 -Force }