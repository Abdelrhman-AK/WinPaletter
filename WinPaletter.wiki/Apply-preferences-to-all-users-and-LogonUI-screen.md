## 1. User preference mask (UPM)

- UPM includes some items like Windows animations, shadows, cursors shadows, ...

- You can apply UPM not only to current user, but also to all users and LogonUI screen

- Open Settings `>` Theme applying behavior `>` General `>` Check `Include User Preference Mask for all users HKEY_USERS\.DEFAULT\Control Panel\Desktop : UserPreferencesMask)`

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/UPM.png?raw=true)

***

## 2. Desktop wallpaper

- You can change Wallpaper in all users and LogonUI screen

- Open Settings `>` Theme applying behavior `>` General `>` and check one choice from these

```
  1. Copy from current desktop
  2. Don't change
  3. Restore defaults (No wallpaper on LogonUI)
```

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/CopyWallpaper.png?raw=true)

***

## 3. Classic colors

- You can change classic colors in all users and LogonUI screen

- Open Settings `>` Theme applying behavior `>` Classic Colors

- There are 2 registry levels which can be changed:

```
HKEY_USERS\.DEFAULT\Control Panel\Colors (For all users & LogonUI): there are 2 choices
  1. Overwrite
  2. Don't change

HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard (For LogonUI in Windows 8.1 and later): there are 4 choices:
  1. Overwrite
  2. Restore defaults
  3. Erase (Remove)
  4. Don't change
```

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/CopyClassicColors.png?raw=true)

***

## 4. Metrics and fonts

- You can change metrics and fonts in all users and LogonUI screen

- Open Settings `>` Theme applying behavior `>` Metrics and fonts

```
HKEY_USERS\.DEFAULT\Control Panel\Desktop\WindowMetrics (For all users & LogonUI): there are 2 choices
  1. Overwrite
  2. Don't change
```

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/CopyMetricsFonts.png?raw=true)

***

## 5. Cursors

- You can change cursors in all users and LogonUI screen

- Open Settings `>` Theme applying behavior `>` Cursors

```
HKEY_USERS\.DEFAULT\Control Panel\Cursors (For all users & LogonUI): there are 2 choices
  1. Overwrite
  2. Don't change
```

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/CopyCursors.png?raw=true)

## 6. Consoles (Command Prompt and PowerShell)

- You can change consoles in all users and LogonUI screen

- Open Settings `>` Theme applying behavior `>` Consoles

```
On applying Command Prompt:
HKEY_USERS\.DEFAULT\Console (For all users & LogonUI): there are 2 choices
  1. Overwrite
  2. Don't change

On applying PowerShell x86:
HKEY_USERS\.DEFAULT\Console\%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe (For all users & LogonUI): there are 2 choices
  1. Overwrite
  2. Don't change

On applying PowerShell x64:
HKEY_USERS\.DEFAULT\Console\%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe (For all users & LogonUI): there are 2 choices
  1. Overwrite
  2. Don't change
```

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/CopyConsoles.png?raw=true)

- You can override user preferences made to Command Prompt, just check `Override Command Prompt custom user preferences (Manually edited preferences)`

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/OverrideCommandPrompt.png?raw=true)