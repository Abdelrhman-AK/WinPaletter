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
  url           = 'https://github.com/Abdelrhman-AK/WinPaletter/releases/download/v1.0.9.3/WinPaletter.zip'
  checksum      = '571A7F27B989E233477BEF94AA0E5288'
  checksumType  = 'md5'
}

#Install-ChocolateyPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateypackage
Install-ChocolateyZipPackage @packageArgs # https://docs.chocolatey.org/en-us/create/functions/install-chocolateyzippackage

Install-ChocolateyShortcut -ShortcutFilePath ($desktopdir + "\WinPaletter.lnk") -TargetPath ($appdir + "\WinPaletter.exe")
Install-ChocolateyShortcut -ShortcutFilePath ($startdir + "\WinPaletter.lnk") -TargetPath ($appdir + "\WinPaletter.exe")