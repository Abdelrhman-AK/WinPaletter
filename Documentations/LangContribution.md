# Instructions for Language Creation\Modification for WinPaletter Versions equal or higher than 1.0.6.9

> If you are using WinPaletter less than 1.0.6.9, please [read this instead](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/TranslationContribution.md)

---

# This new method has been made instead of the older one, being dependent on JSON

to try to minimize errors as much as possible, increase functionality and make it easier to modify.

---

# Get Languages and apply them

[Visit this folder in WinPaletter GitHub Repository and download the language you want](https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Languages)

> The New Format is `JSON`, this is the right file
> 
> Don't navigate through folder `For Versions Below 1.0.6.9` as it is for older versions, with file extension `*.wplng`

Download Language > Open app > Settings > Languages > Activate Custom Language > Open the downloaded file > Save Settings

> After saving settings, you will find language is applied without restarting WinPaletter.
> 
> Restarting is required only when you return to English

---

# Return To English

Open app > Settings > Languages > Deactivate Custom Language and erase the textbox of language file path > Save Settings and finally restart the application

--- 

# Create Language File

1) Open WinPaletter > Settings > Language > Language Developing Tools > Create\Modify Language Files (Go)

2) You can do one of these two options:
   
   - Press on `Generate New (English) only` and save JSON file as you want anywhere, then you can modify the JSON file by a Code\Text Editor (VSCode, Notepad ++, ...)
   
   **or**
   
   - Press on `Generate New (English) and Open it` and save JSON file as you want anywhere. You will find the JSON file is deserialized into a tree node inside the application.
     
      ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Lang/0.png?raw=true)

---

# Modify contents using a code\text editor

> Ignore\Skip this if you can't\won't  use a code\text editor
> 
> We will use VSCode for example

   ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Lang/3.png?raw=true)

- Modify the strings in the right part colored in orange

- Never modify name of a variable in the left part colored in blue as this will lead to errors

- In `Information`'s part, `righttoleft` can be `"righttoleft": false` or `"righttoleft": true` (Not TRUE or True, keep it the same as explained before)

- Don't remove or modify brackets (`{0}`), slashes (`\` or `/`) or quotation marks (`"`), keep them consistent with your new translation.

- Load your language file as explained above in [*Get Languages and applying them*](https://github.com/Abdelrhman-AK/WinPaletter/edit/master/Documentations/LangContribution1.0.6.9.md#get-languages-and-applying-them) to test it.

- [Upload Your Language File to GitHub (Will be explained below)](https://github.com/Abdelrhman-AK/WinPaletter/edit/master/Documentations/LangContribution1.0.6.9.md#upload-your-language-file-to-github-so-others-can-use-it)

---

# Modify contents using WinPaletter itself

> Ignore this if you prefer a a code\text editor

1. Open WinPaletter > Settings > Language > Language Developing Tools > Create\Modify Language Files (Go) > Press on `Generate New (English) and Open it` and save JSON file as you want anywhere, or press on `Open From`. You will find the JSON file is deserialized into a tree node inside the application.

2. JSON file is composed of three main regions: Information, Global Strings & Forms Strings:
   
   - **Information consists** of the translator name, language and other important data used for identification of your file.
   
   - **Global Strings**: contains text inside the code (For example: the strings used in messages boxes, ...)
   
   - **Forms Strings**: contains text of the controls inside the form.
     
     ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Lang/1.png?raw=true)

3. How to modify contents?
   
   - Navigate through nodes until you reach the end, like this screenshot:
   
   - You will find two textboxes: (Old Value) & (New Value), modify the contents of New Value's textbox and press on `Submit Change` 
   
   - Navigate to the following nodes, and so on ...
   
   - When you finish, press on `Save As ...` and close the form
   
   - Load your language file as explained above in [*Get Languages and applying them*](https://github.com/Abdelrhman-AK/WinPaletter/edit/master/Documentations/LangContribution1.0.6.9.md#get-languages-and-applying-them) to test it.
     
     ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Lang/2.png?raw=true)

4. [Upload Your Language File to GitHub (Will be explained below)](https://github.com/Abdelrhman-AK/WinPaletter/edit/master/Documentations/LangContribution1.0.6.9.md#upload-your-language-file-to-github-so-others-can-use-it)

5. Important notes:
   
   - In `Information\righttoleft`'s Section, only use True or False
   
   - Language Snippets used to add values like name, code or layout of a language (load these snippets when you are in `Information\lang`, `Information\langcode` & `Information\righttoleft`)
   
   - Numbers in Curly brackets should be left unchanged.
   
   - > For example: `There is {0} files` is translated into Arabic like that: `يوجد {0} ملف`

---

# Update your language file when a new WinPaletter is released

1. Open WinPaletter > Settings > Language > Language Developing Tools > Update JSON Language File (Go)

2. Open an old JSON file (That you want to be updated)

3. Open an updated JSON or Generate New English `Recommended`

4. Press on `Save As ...` and the updated file will be generated.

5. Modify the newly generated file by any method you like (explained above: code editor or WinPaletter itself)

6. Navigate through all parts of the file, you will find new entries added to the file with a different language (Mainly English). Modify them and save the file.
   
   > Never forget to check for "Information"'s section as it will be overwritten by other values everytime you do a language file update

7. Load your language file as explained above in *"Get Languages and applying them"* to test it.

8. [Upload Your Language File to GitHub (Will be explained)](https://github.com/Abdelrhman-AK/WinPaletter/edit/master/Documentations/LangContribution1.0.6.9.md#upload-your-language-file-to-github-so-others-can-use-it)
   
     ![alt text](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Lang/4.png?raw=true)

---

# Upload Your Language File to GitHub, so others can use it

- Create a GitHub Account if you don't have an account, or login if you already have one.

- Visit WinPaletter Repository: https://github.com/Abdelrhman-AK/WinPaletter/

- Fork (You will find this button in the upper part of the page).

- Now visit your personal account main page, you will find WinPaletter Forked (as if cloned to your account if you don't know the meaning of Fork).

- Navigate to folder `Languages` and create a new file \ or edit it if you already uploaded a previous one.

- After creating the file, open it and press on edit (Pencil) icon.

- Override contents of the created file by the contents of file you previously created.

- Confirm saving the file.

- Create a pull request, once this request is approved, your new language file will be merged with the Main WinPaletter Repository and so anyone can access your translation\language from this link: https://github.com/Abdelrhman-AK/WinPaletter/tree/master/Languages
