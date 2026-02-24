## Intro

- WinPaletter can patch PE files (`*.dll`, `*.exe`). This feature is used in changing Windows startup sound.

- When you patch a system file (that exists in any subfolder in `%windir%`), an alert dialog will appear:

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/PE_0.png?raw=true)

- You can press on `Modify` to continue modifying PE file. If you don't want to touch a system file, just press on `Don't modify`.

## Settings

- Open Settings `>` Theme applying behavior `>` PE patching

- Checking this option will disable the alert dialog before, and will modify/not modify according to your selection here without showing alert dialog

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/PE_1.png?raw=true)

- You can check the following choice if you want to restore `imageres.dll` health by a `SFC` scan on restoring default Windows startup sound

![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/PE_2.png?raw=true)