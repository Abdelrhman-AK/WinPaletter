# Designing themes rules

---

## A. Summary:

- Design theme with WinPaletter 1.0.7.7 and higher (not lower) and make this theme included with its theme resources pack (if there are sounds and images not included inside system directories) and edit theme info to make it previewed correctly on the store, and test your theme before publishing.

---

## B. Steps that are done for one time before designing themes:

1. Use WinPaletter 1.0.7.7 and higher (not lower)

2. Open 'Settings' > 'Theme file type management'

3. Optional: Check `Make saving a theme file exports a resources pack that contains files used in theme` if you are going to design a theme contains sounds and images not included inside system directories
   
   > By checking this option, you're responsible for copyrights of 
   included resources (images and sounds), especially after 
   uploading to WinPaletter Store. 
   WinPaletter developer is not responsible for users' uploaded 
   themes copyrights.
   
   - Files inside `%windir%\Web`, `%windir%\Media`, `Windows XP: %windir%\Resources\Themes\Luna` and `Screen Savers: %windir\System32%` are not included inside themes resources pack. If you insist on a file inside these folders, copy them first to a new folder outside these folders and design your theme by WinPaletter and depend on the new files inside new folder.
   
   - When you save a theme with this option enabled, a file with extension `.wptp` is made, having exactly the same file name of the theme.
   
   - When this theme is loaded, WinPaletter tries to get resources pack file from the file name of the theme, if the pack exists with the same name, WinPaletter will open this theme and extract the associated resources pack file.
   
   - If this resources pack exists but doesn't have the same name as its theme file, it won't be loaded.
   
   - For example, files names must be as this to load the resources pack
     
     ```
     MyWinPaletterThemeFile.wpth
     MyWinPaletterThemeFile.wptp
     ```
   
   - In Windows, the files may be shown without extensions, make them have the same name
     
     ```
     MyWinPaletterThemeFile
     MyWinPaletterThemeFile
     ```
   
   - This pack is extracted at `%localappdata%\Abdelrhman-AK\WinPaletter\ThemeResPack_Cache`

4. Always make `Save theme files compressed` checked
   
---

## C. Steps that are done each time you design themes:

1. Use WinPaletter 1.0.7.7 and higher (not lower)

2. Design the theme as you want

3. `Optional` On main form, open 'WinPaletter application theme' (You will find it in the lower part of the form with other images buttons) and design a UI that fits your theme colors

4. Edit theme info (From pencil button in the upper part of the form)
   
   1. Enter all required info, especially description box (You can include tags in it to make Store search easier)
   
   2. Open Store item info:
      
      1. Choose two descriptive colors, they should give the user an idea or a figure about your theme
      
      2. Choose a decoration pattern for your theme
      
      3. Choose the operation systems that your theme is designed especially for
      
      4. Load into current theme

5. Save your theme

6. `Optional` test your theme (and theme resources pack file if it is created) inside a Virtual Machine using all supported operation systems (from Windows XP to Windows 11) to check if the theme is working as it should

7. Publish theme file (and theme resources pack file if it is created) into server\GitHub repository as explained in their documentation
