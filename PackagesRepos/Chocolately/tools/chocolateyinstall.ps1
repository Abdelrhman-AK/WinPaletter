$ErrorActionPreference = 'Stop' # stop on all errors
#$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$appdir = Join-Path -Path ${env:ProgramFiles} -ChildPath '\Abdelrhman-AK\WinPaletter'
$desktopdir = [Environment]::GetFolderPath("Desktop")
$startdir = [Environment]::GetFolderPath('CommonStartMenu') + "\Programs\WinPaletter"

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  fileType      = 'EXE'
  softwareName  = 'WinPaletter*'
  unzipLocation = $appdir
  url           = 'https://github.com/Abdelrhman-AK/WinPaletter/releases/download/v1.0.8.0/WinPaletter.zip'
  checksum      = 'EF8A79746DA2E8D8273A39E9260EFFCE'
  checksumType  = 'md5'
}

#Install-ChocolateyPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateypackage
Install-ChocolateyZipPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateyzippackage

$WshShell = New-Object -comObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut($desktopdir + "\WinPaletter.lnk")
$Shortcut.TargetPath = $appdir + "\WinPaletter.exe"
$Shortcut.Arguments = ""
$Shortcut.Save()

New-Item -ItemType Directory -Force -Path $startdir
$WshShell = New-Object -comObject WScript.Shell
$Shortcut = $WshShell.CreateShortcut([Environment]::GetFolderPath('CommonStartMenu') + "\Programs\WinPaletter\WinPaletter.lnk")
$Shortcut.TargetPath = $appdir + "\WinPaletter.exe"
$Shortcut.Arguments = ""
$Shortcut.Save()