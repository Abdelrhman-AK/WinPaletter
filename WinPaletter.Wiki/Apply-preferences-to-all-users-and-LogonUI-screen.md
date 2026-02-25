## Applying Themes to All Users and LogonUI

WinPaletter allows you to **apply theme elements** not only to the current user, but also to **all users and the LogonUI screen**. This section explains each configurable option and registry behavior.

---

### 1. User Preference Mask (UPM)

- UPM includes items like **Windows animations, shadows, cursor shadows**, and more.  
- Can be applied to **current user, all users, and LogonUI**.
- Navigate: **Settings** → **Theme Applying Behavior** → **General** → Check:  
  `Include User Preference Mask for all users (HKEY_USERS\.DEFAULT\Control Panel\Desktop : UserPreferencesMask)`

![User Preference Mask](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/Settings/UPM.png?raw=true)

> [!WARNING]
> - Modifying UPM for all users may affect login experience and system-wide behavior.  
> - Ensure you have **backups** or a restore point before applying changes to `.DEFAULT`.

---

### 2. Desktop Wallpaper

- You can change the **desktop wallpaper** for all users and LogonUI.  
- Navigate: **Settings** → **Theme Applying Behavior** → **General** → Choose one of:
```
Copy from current desktop
Don't change
Restore defaults (No wallpaper on LogonUI)
```

![Copy Wallpaper](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/Settings/CopyWallpaper.png?raw=true)

> [!INFO]
> - `Restore defaults` removes the LogonUI wallpaper while keeping user desktops intact.  
> - `Copy from current desktop` ensures consistency across all accounts.

---

### 3. Classic Colors

- Classic colors can be applied to all users and LogonUI.  
- Navigate: **Settings** → **Theme Applying Behavior** → **Classic Colors**  

Registry levels:

`HKEY_USERS.DEFAULT\Control Panel\Colors (All users & LogonUI)`
```
Overwrite
Don't change
```

`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\DefaultColors\Standard (LogonUI Windows 8.1+)`
```
Overwrite
Restore defaults
Erase (Remove)
Don't change
```

![Copy Classic Colors](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/Settings/CopyClassicColors.png?raw=true)

> [!WARNING]
> - Overwriting DefaultColors can affect LogonUI theme.  
> - Always verify changes on a test account before applying system-wide.

---

### 4. Metrics and Fonts

- Change **window metrics and fonts** for all users and LogonUI.  
- Navigate: **Settings** → **Theme Applying Behavior** → **Metrics and Fonts**

Registry:

`HKEY_USERS.DEFAULT\Control Panel\Desktop\WindowMetrics (All users & LogonUI)`
```
Overwrite
Don't change
```

![Copy Metrics & Fonts](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/Settings/CopyMetricsFonts.png?raw=true)

> [!WARNING]
> - Applying metrics/fonts with apps open may **crash the system**.  
> - Consider enabling the **logoff/logon delay option** for safer application.

---

### 5. Cursors

- Change **cursors** for all users and LogonUI.  
- Navigate: **Settings** → **Theme Applying Behavior** → **Cursors**

Registry:

`HKEY_USERS.DEFAULT\Control Panel\Cursors (All users & LogonUI)`
```
Overwrite
Don't change
```

![Copy Cursors](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/Settings/CopyCursors.png?raw=true)

> [!INFO]
> - Overwriting cursor settings ensures consistency across accounts and LogonUI.  
> - `Don't change` preserves user-customized cursors.

---

### 6. Consoles (Command Prompt & PowerShell)

- Apply console themes to **all users and LogonUI**.  
- Navigate: **Settings** → **Theme Applying Behavior** → **Consoles**

Registry paths and choices:

`Command Prompt`
`HKEY_USERS.DEFAULT\Console (All users & LogonUI)`
```
Overwrite
Don't change
```

`PowerShell x86`
`HKEY_USERS.DEFAULT\Console%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe`
```
Overwrite
Don't change
```

`PowerShell x64`
`HKEY_USERS.DEFAULT\Console%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe`
```
Overwrite
Don't change
```

- Optional: Override user preferences for Command Prompt:

![Override Command Prompt](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Asset/Settings/OverrideCommandPrompt.png?raw=true)

> [!WARNING]
> - Overwriting console settings may **reset user-customized preferences**.  
> - Only enable override if consistent behavior across accounts is required.

> [!TIP]
> - Test console changes in a secondary account before applying system-wide.  
> - Consider creating a **backup `.reg` file** of keys mentioned above.

---

**Summary:**  
- These options allow developers and power users to **apply theme elements system-wide**.  
- Always use caution when modifying `.DEFAULT` and system-level registry keys.  
- Recommended workflow: **backup → apply → test**.