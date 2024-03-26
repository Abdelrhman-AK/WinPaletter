$ErrorActionPreference = 'Stop' # stop on all errors
#$toolsDir   = "$(Split-Path -parent $MyInvocation.MyCommand.Definition)"
$desktopdir = [Environment]::GetFolderPath("Desktop")
$startdir = [Environment]::GetFolderPath('CommonStartMenu') + "\Programs\WinPaletter"
$appdir = Join-Path -Path ${env:ProgramFiles} -ChildPath '\Abdelrhman-AK\WinPaletter'

$packageArgs = @{
  packageName   = $env:ChocolateyPackageName
  fileType      = 'EXE'
  softwareName  = 'WinPaletter*'
  unzipLocation = $appdir
  url           = 'https://github.com/Abdelrhman-AK/WinPaletter/releases/download/v1.0.8.9/WinPaletter.zip'
  checksum      = '39CD66F1AC9B91211D511C0B6206C688'
  checksumType  = 'md5'
}

#Install-ChocolateyPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateypackage
Install-ChocolateyZipPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateyzippackage

Install-ChocolateyShortcut -ShortcutFilePath ($desktopdir + "\WinPaletter.lnk") -TargetPath ($appdir + "\WinPaletter.exe")
Install-ChocolateyShortcut -ShortcutFilePath ($startdir + "\WinPaletter.lnk") -TargetPath ($appdir + "\WinPaletter.exe")