## 1. Summary

- Design your theme using WinPaletter version 1.0.7.7 or higher. Make sure to include a theme resources pack if your theme uses sounds or images that are not part of the Windows system directories. Edit the theme info so it previews correctly in the store, and test your theme before publishing.

---

## 2. One-Time Setup Before Designing Themes

1. Use WinPaletter version 1.0.7.7 or newer (older versions are not supported).

2. Go to `Settings` > `Theme file type management` and make sure **Save theme files compressed** is checked.

![Save theme files compressed](Assets/Settings/WPTHCompress.png?raw=true)

---

## 3. Steps to Follow Each Time You Design a Theme

#### 1. Use WinPaletter 1.0.7.7 or higher.

#### 2. Design your theme as you want.

#### 3. *(Optional)* In the main window, open **WinPaletter application theme**.

- [Learn how to edit it here.](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Edit-WinPaletter-application-theme)

![Application Theme](Assets/Store/WPTheme.png?raw=true)

![Theme Editing](Assets/WPTheme/4_0.png?raw=true)

#### 4. [Edit Theme Info](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Edit-theme-info)

1. Fill in all required info, especially the description (you can include tags to make your theme easier to find in the Store).

---

2. Open `Theme Resources Pack`

   A. *(Optional)* Check **Make saving this theme as a file exports a resources pack that contains files used in theme** if your theme uses sounds or images not included in system directories.

![Theme Resources Pack](Assets/ThemeInfo/i3.png?raw=true)

   > If you check this, you are responsible for the copyrights of included resources (images, sounds, screen savers). WinPaletter's developer is not responsible for user-uploaded theme copyrights in the WinPaletter Store.
   
   - Files from `%windir%\Web`, `%windir%\Media`, `Windows XP: %windir%\Resources\Themes\Luna`, and `Screen Savers: %windir%\System32` are **not** included in the resources pack. If you want to use files from these folders, copy them to a new folder outside these directories and use them from there in your theme.
   
   - When saving a theme with this option enabled, a `.wptp` file will be created with the same file name as your theme.
   
   - When loading a theme, WinPaletter looks for a resources pack file with the same name. If found, it will extract and use the resources pack automatically.
   
   - If the resources pack exists but does not have the same name as the theme file, the theme will load without its resources pack.
   
   - Example: The file names must match to load the resources pack correctly.
     
     |                                | File extension hidden         | File extension visible        |
     | ------------------------------ | ----------------------------- | ----------------------------- |
     | Theme file name                | `MyWinPaletterThemeFile`      | `MyWinPaletterThemeFile.wpth` |
     | Theme resources Pack file name | `MyWinPaletterThemeFile`      | `MyWinPaletterThemeFile.wptp` |
     | Screenshot                     | ![hidden ext](Assets/Store/extscheme0.png?raw=true) | ![visible ext](Assets/Store/extscheme1.png?raw=true) |

   - This is incorrect—this will **not** load the theme resources pack:

![Wrong File Name Example](Assets/Store/extscheme2.png?raw=true) 

   - The resources pack is extracted to `%localappdata%\Abdelrhman-AK\WinPaletter\ThemeResPack_Cache`
   
   ---
   
   B. Add credits and licenses for any files you used in your theme (images, audio, screensavers, etc.). Leave this empty if you didn't use any extra files. This is shown to users in the WinPaletter Store. If the user doesn't accept the licenses, the theme won't be applied.

![Credits and Licenses](Assets/ThemeInfo/i4.png?raw=true)

---

3. Open the **Store item info** tab

   1. Pick two colors that best represent your theme—they should give users a sense of your theme's style.

![Descriptive Colors](Assets/ThemeInfo/i5.png?raw=true)

   2. Choose a decoration pattern for your theme.

![Decoration Pattern](Assets/ThemeInfo/i6.png?raw=true)

   3. Select the operating systems that your theme is designed for.

![Operating Systems](Assets/ThemeInfo/i7.png?raw=true)

   4. Load the info into your current theme.

![Load into Theme](Assets/ThemeInfo/i8.png?raw=true)

---

#### 5. Save your theme.

#### 6. *(Optional)* Test your theme (and the resources pack file, if created) in a virtual machine on all supported Windows versions (from Windows XP to Windows 11) to make sure it works as expected.

#### 7. Publish your theme file (and the resources pack file, if created) to your server or GitHub repository, as explained in [this guide](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Upload-themes-to-WinPaletter-Store-repository).