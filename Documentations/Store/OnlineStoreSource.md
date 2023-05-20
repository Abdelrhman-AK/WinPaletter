# How to create an online WinPaletter Store source

- WinPaletter allows adding sources to the Store, it can be online or offline. In this documentation, you will learn the mechanism of an online store source (How to build a theme server\repository and how to add sources to current WinPaletter setup).

---

### Important notes:

### **1. You are responsible for the copyrights of the themes\resources pack in the server\repository you build. WinPaletter developer is not responsible for copyrights violation by other users.**

### 2. Themes that are included online in a server\repository must be made by WinPaletter `1.0.7.7` and higher, otherwise the themes won't be loaded. If you insist on uploading a theme made by an old version of WinPaletter, use WinPaletter `1.0.7.7` or higher to convert this theme into the new format version (You can find convert button in main form in the upper part that has buttons)

### 3. Offline source mode can load themes made by any WinPaletter version

---

### 1. Add an online source to my WinPaletter setup:

1. Open WinPaletter
2. Open `Settings`
3. Click on `Store`
4. Toggle `Online` option
5. Press on `+` button and add source link that you have from a friend or obtained it online or even built it. This link must be a direct link
6. Don't forget to add `?raw=true` to the end of link if it is from a GitHub Repository to make it a direct link. If this part already exists, skip this step.
7. You can change order of the sources or remove the sources you want except the essential two that are made specifically for WinPaletter
8. Click on `Save` button and open `WinPaletter Store` from the main form

---

### 2. Add an offline source to my WinPaletter setup:

1. Open WinPaletter
2. Open `Settings`
3. Click on `Store`
4. Toggle `Offline` option
5. Press on `+` button and add folder\s in which the themes you saved are located
6. You can change order of the sources or remove the sources you want
7. Click on `Save` button and open `WinPaletter Store` from the main form

---

### 3. Build an online source (server\repository):

1. You should have your own files server or a files uploading service, and the links you provide in the source must be direct links (that doesn't prompt user to wait before download or click to download or anything else that interrupt direct download process).

2. If you don't have a server, you can create a GitHub repository.

3. A **store source** is a text file contains lines, each line is responsible for a theme. And the composition of this line is: `MD5_ThemeFile|MD5_ResourcesPackFile|DirectLink_To_ThemeFile|DirectLink_To_PackFile`
   
   - `MD5` is used to make WinPaletter check if the downloaded theme has been updated\changed or not, if `MD5` inside source doesn't equal `MD5` of downloaded theme, WinPaletter will redownload the theme and override the old one. If they equal each other, no change will be made.
   
   - If the theme doesn't have a resources pack, its `MD5` will be `0` and ignore the last part of line: `|DirectLink_To_PackFile`
   
   - "|" (without the quotes) is a separator between arguments in the line
   
   - Example for a source file: 
     
     ```
     3D503317FA22B4999A980E8B6C23E916|4FBC9EE7157F580D036129EA26AD307A|https://link_to_theme_1|https://link_to_theme_resources_pack1
     AC28715B458F8B3EA3CCF4E933C60D36|0|https://link_to_theme_2
     2FED07C211A0E58AD88D6F06FC513A66|D697F2F674658814D0CA074125067BB1|https://link_to_theme_3|https://link_to_theme_resources_pack3
     ```

4. If you are using GitHub Repository as your own server, read the following:
   
   1. don't forget to add `?raw=true` to the end of any link (source link or links inside the source file), to make it a direct link
   
   2. You can fork and edit [WinPaletter-Theme repository](https://github.com/Abdelrhman-AK/WinPaletter-Store) to understand the previous points 
   
   3. [store.wpdb](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/store.wpdb "store.wpdb") is the source file
   
   4. [main.py](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/main.py "main.py") is a Python script that updates [store.wpdb](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/store.wpdb) (Will be discussed later)
   
   5. Add your exported themes inside [Themes](https://github.com/Abdelrhman-AK/WinPaletter-Store/tree/main/Themes "Themes") directory 
   
   6. [enum_themes.yml](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/.github/workflows/enum_themes.yml) is a GitHub Actions yaml file that starts automatically when new files are added to [Themes](https://github.com/Abdelrhman-AK/WinPaletter-Store/tree/main/Themes "Themes") directory inside the repository, and it depends on [main.py](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/main.py "main.py") to update the source file. (Will be discussed later)
      
      ---
      
      #### [A. main.py](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/main.py "main.py"):
      
      Edit these two lines to make it fetch the themes and their packs from the right GitHub Repository (Without modifying `+ targetfile + '?raw=true'` and `+ targetpack + '?raw=true'`):
      
      ```
         url_file = 'https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/' + targetfile + '?raw=true'
         url_pack = 'https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/' + targetpack + '?raw=true'
      ```
      
      - Please respect the indentations inside this Python script file or the Action will fail after running
      
      #### [B. enum_themes.yml](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/.github/workflows/enum_themes.yml)
      
      1. Make your GitHub repository can have Actions (Search for it)
      
      2. Make a new Action, with its yaml file is the same as [enum_themes.yml](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/.github/workflows/enum_themes.yml)
      
      3. Please respect the indentations inside this yaml Action file or it will fail after running
      
      4. Edit the following three lines to make it suitable with your repository:
         
         ```
            branches: [ main ]
            paths: Themes/**
         ```
         
         - Make the branch name consistent with current GitHub repository branch. It may be [ main ] or [ master ] or something else.
         - Make paths have the name of themes directory, for example: `paths: Themes/**` or `paths: MyWinPaletterThemes/**` or something else.
         - The benefit of line `paths: Themes/**` is that when any change is made to this folder, this action will run automatically and update the source list
         
         
         ```
         run: python main.py Themes store.wpdb 
         ```
         
         - `main.py` is the path of Python script that builds the source list
         
         - `Themes` is the path of themes directory
         
         - `store.wpdb` is the target source file that will be updated
