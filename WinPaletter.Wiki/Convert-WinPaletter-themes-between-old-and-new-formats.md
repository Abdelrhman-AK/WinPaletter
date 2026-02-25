# ⚠ Obsolete: Theme Format Conversion in WinPaletter

> [!obsolete]
> This guide is **obsolete**.  
> Starting with WinPaletter **1.0.7.7 and later**, themes now use a JSON-based internal format. Manual conversion is rarely needed, as opening an old-format theme in WinPaletter will prompt automatic conversion.

---

## Introduction

- WinPaletter 1.0.7.7+ uses a **JSON-internally formatted theme file**, making it easier to code, edit, and maintain.  
- Older themes (pre-1.0.7.7) can be converted to the new format using the built-in conversion tools.  
- Conversion options include:
  - **GUI Converter**
  - **Command Line**
  - **Command Line with a List File**

> [!warning]
> If possible, prefer updating themes directly in WinPaletter 1.0.7.7+ rather than converting older files manually.

---

## 1. GUI Converter

1. Open WinPaletter and click the **Converter** button:  

2. Open your theme file:
   - Old-format files are automatically converted to the new JSON format.
   - You can also convert new-format files back to the old format (not recommended).

3. Options:
   - **Compress contents** (Recommended; for new JSON format)
   - **Make compatible with WinPaletter <1.0.6.9** (Not recommended)

4. Click **Export** to save the converted file.  
   ![Converter GUI](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/Settings/WPTHConvert.png?raw=true)

---

## 2. Command Line Conversion

1. Open **Command Prompt** in the folder containing `WinPaletter.exe`.

2. Use one of the following formats:

```cmd
WinPaletter.exe /convert:"themefile_to_be_converted"|"themefile_to_be_exported"
WinPaletter.exe /convert:"themefile_to_be_converted"|"themefile_to_be_exported"|x
WinPaletter.exe /convert:"themefile_to_be_converted"|"themefile_to_be_exported"|x|y
```

3. Arguments must be quoted and separated by `|`.

- `x` = Compress contents (0 = no, 1 = yes)
- `y` = Make compatible with old WinPaletter (<1.0.6.9) (0 = no, 1 = yes)

4. WinPaletter will detect the file type and perform the conversion automatically.

> [!warning]
> Manual command-line conversion is generally unnecessary with WinPaletter 1.0.7.7+.

## 3. Command Line: Batch Conversion Using a List of Themes

1. Create a text file using **Notepad**.
2. Each line should specify a theme file using one of the following formats:
```
"themefile_to_be_converted"
"themefile_to_be_converted"|x
"themefile_to_be_converted"|x|y
```
3. Example of a list file:
```
"C:\Users\Username\Desktop\theme0.wpth"
"C:\Users\Username\Desktop\theme1.wpth"
"C:\Users\Username\Desktop\theme2.wpth|0|1"
"C:\Users\Username\Desktop\theme3.wpth|1"
"C:\Users\Username\Desktop\theme4.wpth"
"C:\Users\Username\Desktop\theme5.wpth|1|1"
"C:\Users\Username\Desktop\theme6.wpth"
```
4. Save the text file (e.g., `themes_list.txt`) and run the following command in the folder containing `WinPaletter.exe`:
```
WinPaletter.exe /convert-list:"themes_list.txt"
```

5. **WinPaletter** will process each line, converting files between the old and new formats.

6. Converted files are saved in a folder named `WinPaletterConversion` located in the same directory as the original theme files.

> [!warning]
> This list file method is only needed for batch-converting legacy themes. Modern versions of WinPaletter do not support the legacy formatting.