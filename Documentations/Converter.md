# Convert WinPaletter themes between old and new formats

WinPaletter 1.0.7.7 now uses a new format for themes files, it is JSON-internally formatted which is easier in coding and also editing by a code\text editor.

To reuse a theme you made in earlier versions of WinPaletter, you can convert between old format and new format using WinPaletter conversion tool (Inside WinPaletter).

When you open a theme file with old format, WinPaletter will offer you instant conversion of this file. If you want to convert other themes files, you can use converter by any of these 3 methods: **GUI converter, Command line, or Command line with a list file of themes**

---

## A. GUI converter

1. Open WinPaletter and press on this button (![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Mini-Icons/Converter.png?raw=true))

2. Open file. If it is an old-formatted file, it will be converted to new format, and vise versa

3. Check options:
   
   1. Compress contents (valid for new JSON-internally-formatted themes) `Recommended`
   
   2. Make it valid for old version of WinPaletter less than 1.0.6.9 (valid for old themes format) `Not recommended`

4. Press on `Export`

---

## B. Command line

1. Open command prompt inside folder containing `WinPaletter.exe`

2. Use one of these line schemes to convert a theme file:
   
   ```
   WinPaletter.exe /convert:"themefile_to_be_converted"|"themefile_to_be_exported"
   ```
   
   ```
   WinPaletter.exe /convert:"themefile_to_be_converted"|"themefile_to_be_exported"|x
   ```
   
   ```
   WinPaletter.exe /convert:"themefile_to_be_converted"|"themefile_to_be_exported"|x|y
   ```

3. Please respect the quotes ("), and make the arguments separated by | (not by spaces).

4. `x` controls this option: `Compress contents (valid for new JSON-internally-formatted themes)`, it can be `0` if you don't want this option or `1` if you want.

5. `y` controls this option: `Make it valid for old version of WinPaletter less than 1.0.6.9 (valid for old themes format)`, it can be `0` if you don't want this option or `1` if you want.

6. WinPaletter will detect the file type and will convert between old and new formats

---

## C. Command line with a list file of themes

1. Open notepad

2. Type lines with one of these schemes:
   
   ```
   "themefile_to_be_converted"
   ```
   
   ```
   "themefile_to_be_converted"|x
   ```
   
   ```
   "themefile_to_be_converted"|x|y
   ```

3. Please respect the quotes ("), and make the arguments separated by | (not by spaces). 

4. `x` controls this option: `Compress contents (valid for new JSON-internally-formatted themes)`, it can be `0` if you don't want this option or `1` if you want.

5. `y` controls this option: `Make it valid for old version of WinPaletter less than 1.0.6.9 (valid for old themes format)`, it can be `0` if you don't want this option or `1` if you want.

6. For example, text file can be like this:
   
   ```
   "C:\Users\Username\Desktop\theme0.wpth"
   "C:\Users\Username\Desktop\theme1.wpth"
   "C:\Users\Username\Desktop\theme2.wpth|0|1"
   "C:\Users\Username\Desktop\theme3.wpth|1"
   "C:\Users\Username\Desktop\theme4.wpth"
   "C:\Users\Username\Desktop\theme5.wpth|1|1"
   "C:\Users\Username\Desktop\theme6.wpth"
   ```

7. Save this text file

8. Type in command prompt: 
   
   ```
   WinPaletter.exe /convert-list:"text_file"
   ```

9. WinPaletter will loop through lines of this file and detect the file type and will convert between old and new formats.

10. The new files will be saved inside folder named `WinPaletterConversion` and it is located in the same folders of `themefile_to_be_converted` used.