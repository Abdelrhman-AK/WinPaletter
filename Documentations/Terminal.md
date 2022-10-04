# Terminals Documentation (For version 1.0.5.9 and later)

---

## If you find an issue, please have a look at these questions, if you couldn't find your answer, please post a new Issue.

---

# Questions:

1) Which terminals can be customized?

2) What can I customize in Command Prompt and PowerShell?
   
   * I can't see any effect on Terminal, What's wrong?
   
   * Fonts look weird. What happened?
   
   * I can't colorize or change cursor style. What is the issue?

3) What can I customize in Windows Terminal?
   
   * How to set a default scheme to specific Windows Terminal profile?
   
   * I can't see effects of theme on Terminal, What's wrong?
   
   * I can't see any effect on Terminal too, What's wrong else?
   
   * What are the buttons of (+) and (pencil)?
   
   * How to restore Windows Terminal to default?
   
   * I don't like the preferences\customization I made to Windows Terminal and I feel that it is broken. What should I do?
   
   * Fonts look weird. What happened?

4) After applying the color palette, Command Prompt and PowerShell are not customized in spite of enabling their toggles. What is the issue?

---

# Answers:

## Q1) Which terminals can be customized?

| OS             | Component                                                                                  |
|:--------------:|:------------------------------------------------------------------------------------------:|
| Windows 7, 8.1 | Command Prompt, PowerShell                                                                 |
| Windows 10, 11 | Command Prompt, PowerShell and Windows Terminal (*It can be installed from Windows Store*) |

| OS         | Windows Terminal                                                                                                   |
|:----------:|:------------------------------------------------------------------------------------------------------------------:|
| Windows 10 | Not installed by default, install it from Windows Store                                                            |
| Windows 11 | Windows Terminal Stable is installed by default, while Preview and Developer versions are not installed by default |

* You can also customize another unmentioned terminal, if it exists as a key in registry in `HKEY_CURRENT_USER\Console`.

* In `HKEY_CURRENT_USER\Console`, please ignore `%%Startup`, `%SystemRoot%_System32_WindowsPowerShell_v1.0_powershell.exe` and `%SystemRoot%_SysWOW64_WindowsPowerShell_v1.0_powershell.exe` keys.
  
  * Anyway, you can add an executable to this key using button "New", it should be a console one (Not a GUI\WinForms\Desktop app\\...) and should be in System Drive (C:\\)

---

## Q2) What can I customize in Command Prompt and PowerShell?

| Command Prompt and PowerShell                                                                                                                             |
|:--------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Color Tables from 00 to 15 (16 colors)                                                                                                                    |
| Background and foreground color (Set its value by choosing a color table)                                                                                 |
| Background and foreground color for Pop-Ups (Set its value by choosing a color table)                                                                     |
| Font, Font Weight and Font Size                                                                                                                           |
| Cursor Type and Cursor Size                                                                                                                               |
| Tweaks for Windows 10 19H2 (1909) and Later; Cursor Color, Enhanced Terminal (Terminal Version 2), Line Selection, Terminal Scrolling and Windows Opacity |

### Q2-A) I can't see any effect on Terminal, What's wrong?

All Terminals (Command Prompt\PowerShell\Windows Terminal) have a specific lock to it to avoid unintentional actions on them, so if you want to apply effects to terminals you want, just Enable the toggle which you will find in the upper part of the form.

### Q2-B) Fonts look weird. What happened?

* Command Prompt uses the monospaced fonts only if you disable Raster Fonts, if you force a regular\non monospaced font, it will be rendered wrongly. And if you disable Enhanced Terminals, the regular fonts won't be used at all, and if you enable Raster Fonts, it won't be used at all and the Retro\VGA\Raster font will be used

* Raster fonts size isn't rendered in the same way of monospaced fonts, you will find both different in size in spite of selecting the same number of size

### Q2-C) I can't colorize or change cursor style. What is the issue?

* **Windows 7, 8.1 and 10 less than 19H2 (1909):** can't colorize or change cursor style as the terminal doesn't support that.

* **Windows 10 19H2 (1909) and Later:** You should enable enhanced terminal to colorize and change cursor style. 

---

## Q3) What can I customize in Windows Terminal?

| Windows Terminal (Windows 10 and 11)                                                                                                         |
| -------------------------------------------------------------------------------------------------------------------------------------------- |
| Profile Name, Tab Title, its color and icon and Acrylic effect on titlebar                                                                   |
| Per-Profile customization                                                                                                                    |
| Color schemes names and colors (16 color + Background, Foreground, Selection and Cursor Colors) and the default color scheme for the profile |
| Cursor Type and Size                                                                                                                         |
| Font, Font Weight and Font Size                                                                                                              |
| Themes; you can customize Titlebar Background (Active and Inactive colors) and Tabs Colors (Active and Inactive colors)                      |
| Background Image, its opacity and Acrylic effects                                                                                            |

### Q3-A) How to set a default scheme to specific Windows Terminal profile?

It is simple, you pick-up a profile from profiles list and then pick-up a color scheme from schemes list.

### Q3-B) I can't see effects of theme on Terminal, What's wrong?

Themes are effective only for Windows Terminal 1.16 Preview and Developer and Later Versions

### Q3-C) I can't see any effect on Terminal too, What's wrong else?

All Terminals (Command Prompt\PowerShell\Windows Terminal) have a specific lock to it to avoid unintentional actions on them, so if you want to apply effects to terminals you want, just Enable the toggle which you will find in the upper part of the form.

### Q3-D) What are the buttons of (+) and (pencil)?

The green + is to create a new profile\color scheme\theme.

The blue + is to clone a profile\color scheme\theme.

The pencil is to edit name of color scheme\theme, and properties of selected profile.

### Q3-E) How to restore Windows Terminal to default?

Press on button "Open "settings.json" in editor" which you will find in the bottom of the form, then you will find Notepad or other editor is open. Erase all text of this "settings.json" and save and open your Windows Terminals, the default settings will be regenerated.

### Q3-F) I don't like the preferences\customization I made to Windows Terminal and I feel that it is broken. What should I do?

You can do one solution from these:

* Backup "settings.json" (You will find this button below) before you do any preference\customization to the terminal, then when you decide to restore the old preferences, open the backed-up file by notepad or other editor and copy all contents, and then press button: "Open "settings.json" in editor" and override contents of "settings.json", save and open your Windows Terminals, you will find old preferences are restored.

* Restore Defaults ( As answer of question `Q3-E`)

### Q3-G) Fonts look weird. What happened?

Windows Terminal uses the monospaced fonts only, if you force a regular\non monospaced font, it will be rendered wrongly.

---

# Q4) After applying the color palette, Command Prompt and PowerShell are not customized in spite of enabling their toggles. What is the issue?

* In most version of Windows, Command Prompt applies the colors and the preference from two separate sources, from Registry or from the Shortcut file (.lnk) itself.

* If you change the preference of terminal itself (Not from WinPaletter), it will save the colors into registry, and if the open terminal is by a shortcut file (.lnk) the preferences will be saved into this file (.lnk) not into registry

* When you open start menu and type "Command Prompt", "cmd" or "PowerShell" in search, it will open them from a pre-saved shortcut files saved in `C:\Users\<username>\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\System Tools` and so the terminal will load colors from the (.lnk) file if it was pre-modified.

* WinPaletter only modifies the colors and preferences in registry, and so the problem.

* The solution is easy. You:
  
  1. Open Start Menu, search for the terminal that is not working as it should, then right click and open file location.
  
  2. Right Click on the Shortcut File (.lnk) and click on properties.
  
  3. Backup the following information into a memo or notepad:
     
     * File Name
     
     * Target
     
     * Start In
     
     * Comment
  
  4. Delete the shortcut file
  
  5. Re-Create a shortcut:
     
     * Right Click on the open folder
     
     * New > Shortcut
     
     * Type "Target" value which you backed-up before, then press on next.
     
     * Type the File Name which you backed-up before, then press on Finish.
  
  6. Right Click on the new generated shortcut, press on Properties.
  
  7. Re-Enter these information which you backed-up in step `3` 
  
  8. Save, and then the terminal will get the colors and preferences from registry, not from the newly generated shortcut.
  
  9. To maintain this effect, never modify preferences from this terminal shortcut.

* These rules don't apply on the new Windows 10\11 Terminal (Stable\Preview\Developer).
