# 1. Choose a Windows Terminal edition

1. Click on this button

![alt text](Assets/WinTerminal/WT_0.png?raw=true)

2. Select Windows Terminal edition you want to modify

- It can be Stable or Preview

![alt text](Assets/WinTerminal/WT_1.png?raw=true)

- If the selected Windows Terminal is not installed, you will face this error:

![alt text](Assets/WinTerminal/WT_2.png?raw=true)

- This error can be bypassed by modifying WinPaletter settings (This will export Windows Terminal preferences in WinPaletter theme file, but won't be applied to your system). This will be explained in Settings wiki.

***

# 2. Backup/Restore Windows Terminal settings

- It is very important to do this before you start modification by WinPaletter, to avoid errors in Windows Terminal and avoid its settings data loss

![alt text](Assets/WinTerminal/WT_3.png?raw=true)

- To restore previous settings, open the JSON file you backed-up before in a text editor, copy all contents, press on `Open "Settings.json" in editor`, delete all contents and paste into this file, save this file and open Windows Terminal.

- To reset all settings into default, press on `Open "Settings.json" in editor`, delete all contents, save this file and open Windows Terminal.

***

# 3. `Optional` Toolbar buttons

## 1. Open from WinPaletter theme
This will copy preferences from a WinPaletter theme file into current open theme

## 2. Open from Current applied one
This will copy preferences from current Windows Terminal settings into current open theme (looks like an undo)

## 3. Open from JSON
This will copy preferences from Windows Terminal settings file that has `.JSON` file extension into current open theme

![alt text](Assets/WinTerminal/WT_Toolbar.png?raw=true)

***

# 4. Enable/Disable lock toggle

Make it checked to make WinPaletter apply changes to Windows Terminal

![alt text](Assets/WinTerminal/WT_4.png?raw=true)

***

# 5. Profile selection

Windows Terminal has profiles (for example, a profile for Command Prompt, a profile for PowerShell, ...)

1. Select a profile from this list

![alt text](Assets/WinTerminal/WT_5_0.png?raw=true)

2. `Optional` You can create a new profile from this button: 

![alt text](Assets/WinTerminal/WT_5_1.png?raw=true)

3. `Optional` You can clone the selected profile into a new profile from this button:

![alt text](Assets/WinTerminal/WT_5_3.png?raw=true)

4. `Optional` You can copycat into the selected profile from another profile this button:

![alt text](Assets/WinTerminal/WT_5_4.png?raw=true)

![alt text](Assets/WinTerminal/WT_5_4_1.png?raw=true)

***

# 6. `Optional` Edit profile information

1. Select a profile and press on this button:

![alt text](Assets/WinTerminal/WT_5_2.png?raw=true)

2. You can change profile name, tab title, tab icon, tab color and acrylic effect on titlebar (for all profiles)

![alt text](Assets/WinTerminal/WT_6.png?raw=true)

3. Tab icon can be a file path or emoji/symbol from font "Segoe Fluent Icons"

4. Press on `Load`

***

# 7. Parts

There are 5 parts: colors, theme, fonts, cursor and background

![alt text](Assets/WinTerminal/WT_7.png?raw=true)

## 1. Colors

- There are profiles for colors, you can select one or create a new one

- You can modify its name, clone it or copycat into it.

![alt text](Assets/WinTerminal/WT_8_0.png?raw=true)

- A color profile has items: background, foreground, text selection, cursor color and 16 colors tables

![alt text](Assets/WinTerminal/WT_8_1.png?raw=true)

- These colors are labeled by their names. Each name has two colors, the second one has a lighter tone than the first one. The user who will modify them should respect colors according to their names to give another users correct experience

## 2. Theme

- There are profiles for themes, you can select one or create a new one

- You can modify its name, clone it or copycat into it.

- A theme profile has items: active titlebar, active tab, inactive titlebar, inactive tab and dark/light mode toggle

- The default profiles (system, dark, light) are not modifiable, if you want to modify one, clone it or create a new theme profile

![alt text](Assets/WinTerminal/WT_9.png?raw=true)

## 3. Font

- You can modify terminal font (they are monospaced fonts), weight and size

![alt text](Assets/WinTerminal/WT_10_0.png?raw=true)

- You can force using other fonts by checking this, but the console will render these fonts wrongly

![alt text](Assets/WinTerminal/WT_10_1.png?raw=true)

## 4. Cursor

- You can change cursor type and size

![alt text](Assets/WinTerminal/WT_11_0.png?raw=true)

- These are the types of cursors

![alt text](Assets/WinTerminal/WT_11_1.png?raw=true)

## 5. Background

- You can change background, and its source can be from current wallpaper or an image in your device. To make there is no background, keep this box empty.

![alt text](Assets/WinTerminal/WT_12_0.png?raw=true)

- You can adjust opacity of background image:

![alt text](Assets/WinTerminal/WT_12_1.png?raw=true)

- You can make the window of Windows Terminal in acrylic, with a track bar to control its intensity:

![alt text](Assets/WinTerminal/WT_12_2.png?raw=true)

***

# 7. Actions

1. **Load:** it will load current Windows Terminal preferences into current open WinPaletter theme, and will close this form so you can continue modifying other Windows aspects and finally press on Apply on main form to apply all aspects in the open theme.

2. **Quick apply:** it will apply current Windows Terminal preferences instantly to test their real effects on system.

3. **Cancel:** it will close this form without loading any modification made to Windows Terminal

![alt text](Assets/WinTerminal/WT_13.png?raw=true)

***

# 8. Preview (Testing)

If you have pressed on 'Quick apply' or applied the whole theme, this button will open an instance of Windows Terminal so you can see the real effects of modifications you made.

![alt text](Assets/WinTerminal/WT_14.png?raw=true)

