## PE File Patching in WinPaletter

**WinPaletter** includes a feature to **patch PE files (`*.dll`, `*.exe`)**, which is most commonly used to **change the Windows startup sound**. This allows you to customize system behavior, but it involves modifying system files, so caution is required.  

---

### How It Works

- When you attempt to patch a **system file** located in any subfolder of `%windir%` (Windows directory), an **alert dialog** will appear:

![PE Patch Alert](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/PE_0.png?raw=true)

- Options in the dialog:  
  - **Modify** – proceed with patching the selected PE file.  
  - **Don't modify** – cancel the operation if you do not want to touch system files.

> [!WARNING]
> Modifying system files can potentially **break Windows functionality** if done incorrectly.  
> Always make sure you have a **backup** of the file you are modifying, or a system restore point.  

---

### PE Patching Settings

You can configure WinPaletter to **automate patching** or **restore defaults**.

- Navigate to:  
  **Settings** → **Theme Applying Behavior** → **PE Patching**

- **Disable alert dialog**:  
  - By checking this option, WinPaletter will **automatically apply your selected action** (Modify/Don't modify) without prompting each time.

![PE Patching Settings](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/PE_1.png?raw=true)

> [!INFO]
> Use this setting **only if you are confident** in which PE files WinPaletter should patch automatically. Skipping confirmation may modify critical system files without warning.

---

### Restoring System File Health

- WinPaletter provides an option to **restore `imageres.dll`** health via **SFC (System File Checker)** when restoring the default Windows startup sound.  

![Restore imageres.dll](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Media/Wiki/Settings/PE_2.png?raw=true)

> [!WARNING]
> - Always run this option **if you encounter issues** with Windows system sounds or UI after patching.  
> - This ensures that **critical system resources** are not left in an inconsistent state.  
> - Modifying other system PE files may require manual restoration or backup before patching.

---

### Best Practices for PE Patching

1. **Backup system files** before patching.  
2. Use **WinPaletter 1.0.8.9 or later** for best stability.  
3. Prefer using the **alert dialog** until you are confident in which files should be patched automatically.  
4. After patching, **restart Explorer** or log off/log on if necessary to apply changes fully.  
5. Keep track of your customizations to avoid conflicts during Windows updates.

> [!TIP]
> For frequent theme designers, consider creating a **test virtual machine** or separate Windows account to safely experiment with PE patches without risking your main system.