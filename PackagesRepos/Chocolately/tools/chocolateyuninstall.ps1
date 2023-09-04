$ErrorActionPreference = 'Stop' # stop on all errors

$appdir = Join-Path -Path ${env:ProgramFiles} -ChildPath '\Abdelrhman-AK\WinPaletter'
$app = Join-Path -Path $appdir -ChildPath '\WinPaletter.exe'

Start-ChocolateyProcessAsAdmin -Statements "/uninstall" -ExeToRun $app -NoSleep -ValidExitCodes @(-1,0)

Uninstall-ChocolateyZipPackage 'WinPaletter' 'WinPaletter.zip'