## 1. Create Language file

1) Open WinPaletter > Settings > Language > Language Developing Tools > Create\Modify Language Files (Go)

2) You can do one of these two options:
   
   - Press on `Generate New (English) only` and save JSON file as you want anywhere, then you can modify the JSON file by a Code\Text Editor (VSCode, Notepad ++, ...)
   
   **or**
   
   - Press on `Generate New (English) and Open it` and save JSON file as you want anywhere. You will find the JSON file is deserialized into a tree node inside the application.
     
      ![alt text](https://github.com/Abdelrhman-AK/WinPaletter//blob/master/WinPaletter.Wiki/Asset/Lang/0.png?raw=true)

***

## 2. 1. Modify contents using a code\text editor

> Ignore\Skip this if you can't\won't  use a code\text editor
> 
> We will use VSCode for example

   ![alt text](https://github.com/Abdelrhman-AK/WinPaletter//blob/master/WinPaletter.Wiki/Asset/Lang/3.png?raw=true)

- Modify the strings in the right part colored in orange

- Never modify name of a variable in the left part colored in blue as this will lead to errors

- In `Information`'s part, `righttoleft` can be `"righttoleft": false` or `"righttoleft": true` (Not TRUE or True, keep it the same as explained before)

- Don't remove or modify brackets (`{0}`), slashes (`\` or `/`) or quotation marks (`"`), keep them consistent with your new translation.
  
  > For example: `There are {0} files` is translated into Arabic like that: `يوجد {0} ملفات`

- Load your language file as [explained here](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Change-language) to test it.

- [Upload Your Language File to GitHub](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Language-upload)

---

## 2. 2. Modify contents using WinPaletter itself

> Ignore this if you prefer a code\text editor

1. Open WinPaletter > Settings > Language > Language Developing Tools > Create\Modify Language Files (Go) > Press on `Generate New (English) and Open it` and save JSON file as you want anywhere, or press on `Open From`. You will find the JSON file is deserialized into a tree node inside the application.

2. JSON file is composed of three main regions: Information, Global Strings & Forms Strings:
   
   - **Information consists** of the translator name, language and other important data used for identification of your file.
   
   - **Global Strings**: contains text inside the code (For example: the strings used in messages boxes, ...)
   
   - **Forms Strings**: contains text of the controls inside the form.
     
     ![alt text](https://github.com/Abdelrhman-AK/WinPaletter//blob/master/WinPaletter.Wiki/Asset/Lang/1.png?raw=true)

3. How to modify contents?
   
   - Navigate through nodes until you reach the end, like this screenshot:
   
   - You will find two textboxes: (Old Value) & (New Value), modify the contents of New Value's textbox and press on `Submit Change` 
   
   - Navigate to the following nodes, and so on ...
   
   - When you finish, press on `Save As ...` and close the form
   
     ![alt text](https://github.com/Abdelrhman-AK/WinPaletter//blob/master/WinPaletter.Wiki/Asset/Lang/2.png?raw=true)

- Load your language file as [explained here](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Change-language) to test it.

- [Upload Your Language File to GitHub](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Language-upload)

5. Important notes:
   
   - In `Information\righttoleft`'s Section, only use True or False
   
   - Language Snippets used to add values like name, code or layout of a language (load these snippets when you are in `Information\lang`, `Information\langcode` & `Information\righttoleft`)
   
   - Numbers in Curly brackets should be left unchanged.
   
   - > For example: `There are {0} files` is translated into Arabic like that: `يوجد {0} ملفات`

---

## 3. Update your language file when a new WinPaletter is released

1. Open WinPaletter > Settings > Language > Language Developing Tools > Update JSON Language File (Go)

2. Open an old JSON file (That you want to be updated)

3. Open an updated JSON or Generate New English `Recommended`

4. Press on `Save As ...` and the updated file will be generated.

5. Modify the newly generated file by any method you like (explained above: code editor or WinPaletter itself)

6. Navigate through all parts of the file, you will find new entries added to the file with a different language (Mainly English). Modify them and save the file.
   
   > Never forget to check for "Information"'s section as it will be overwritten by other values everytime you do a language file update

     ![alt text](https://github.com/Abdelrhman-AK/WinPaletter//blob/master/WinPaletter.Wiki/Asset/Lang/4.png?raw=true)

- Load your language file as [explained here](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Change-language) to test it.

- [Upload Your Language File to GitHub](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Language-upload)