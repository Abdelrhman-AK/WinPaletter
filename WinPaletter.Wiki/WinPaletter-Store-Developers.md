# Developer Guide: WinPaletter Store and Sources

> [!IMPORTANT]
> This page is intended for **developers** who want to upload themes to the WinPaletter Store repository or create custom online/offline sources for hosting WinPaletter themes.  
> Normal users should use the **GitHub manager inside WinPaletter** to publish themes safely.

---

## 1. Uploading Themes to the WinPaletter Store Repository

### Steps:

1. Read first: [Designing Themes Rules](https://github.com/Abdelrhman-AK/WinPaletter/wiki/Designing-themes-rules).  
2. Create a GitHub account or log in if you already have one.  
3. Go to the [WinPaletter Store Repository](https://github.com/Abdelrhman-AK/WinPaletter-Store).  
4. Click **Fork** to create your own copy of the repository.  
5. Navigate to your fork under your GitHub profile.  
6. In your fork, go to the `Themes` directory:  
   - Create a folder with your name, then a subfolder with your theme name.  
   - Upload your theme file (`.wpth`).  
   - If your theme has extra resources (images or sounds), also upload the resources pack (`.wptp`).  
7. Create a **pull request**. Once approved, your theme will be available in the WinPaletter Store.

> [!Important]
> - Pull requests are merged as soon as possible, but the WinPaletter developer may not always be available.  

> [!Warning]
> 1. You are responsible for the copyrights of uploaded themes and resource packs.  
> 2. Only themes created with WinPaletter **`1.0.7.7` or newer** are supported in the store. Use the convert button in WinPaletter to update older themes.  
> 3. To update an existing theme, replace the files in your fork and create a new pull request.

---

## 2. Creating an Online WinPaletter Store Source

### Information:

- WinPaletter allows adding **online or offline sources**.  
- You are responsible for copyrights of themes/resources in your server/repository. The WinPaletter developer is not responsible for copyright violations.  
- Only themes created with **WinPaletter `1.0.7.7` or higher** are compatible with online sources. Offline sources can load themes from any version.  

### Steps to Build an Online Source:

1. Prepare a file server or use a file hosting service with **direct download links**.  
2. If you don’t have a server, create a GitHub repository.  
3. A **store source file** is a text file where each line represents a theme:
```
MD5_ThemeFile|MD5_ResourcesPackFile|DirectLink_To_ThemeFile|DirectLink_To_PackFile
```
- `MD5` ensures WinPaletter checks for theme updates.  
- If a theme has no resources pack, use `0` and omit the last part.  
- Example:
```
3D503317FA22B4999A980E8B6C23E916|4FBC9EE7157F580D036129EA26AD307A|https://link_to_theme_1|https://link_to_theme_resources_pack1
AC28715B458F8B3EA3CCF4E933C60D36|0|https://link_to_theme_2
2FED07C211A0E58AD88D6F06FC513A66|D697F2F674658814D0CA074125067BB1|https://link_to_theme_3|https://link_to_theme_resources_pack3
```

---

### Using GitHub as a Source:

- Append `?raw=true` to all file links for direct download.  
- Fork or clone [WinPaletter-Store repository](https://github.com/Abdelrhman-AK/WinPaletter-Store) to understand the structure.  
- Key files:  
  - [`store.wpdb`](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/store.wpdb) – main source file.  
  - [`main.py`](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/main.py) – Python script that updates `store.wpdb` **and now automatically detects your repository name and author**. No manual URL editing is required.  
  - [`Themes`](https://github.com/Abdelrhman-AK/WinPaletter-Store/tree/main/Themes) – directory for theme files.  
  - [`enum_themes.yml`](https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/.github/workflows/enum_themes.yml) – GitHub Actions file to auto-update the source.

> [!IMPORTANT]
> The new `main.py` automatically retrieves the repository name and author from the environment, simplifying setup. You no longer need to manually configure `url_file` or `url_pack`.  

---

### Configuring `enum_themes.yml`:

- Ensure GitHub Actions are enabled.  
- Create a new action using this YAML file.  
- Edit the following lines to match your repository setup:

```yaml
branches: [ main ]
paths: Themes/**
```
- `branches` – branch name (main, master, etc.)
- `paths` – themes directory.

Add the run command:
```yaml
run: python main.py Themes store.wpdb
```
> [!WARNING]
> Respect YAML indentation, otherwise the Action will fail.

## 3. Extending WinPaletter Sources
Information:
- You can extend or create your own WinPaletter themes hosting server.
- Sources can come from friends, online, or your own repository.
- Some sources are built-in and cannot be removed.
Examples of Online Sources:
```
https://github.com/Abdelrhman-AK/WinPaletter/blob/master/Store/store.wpdb?raw=true
```
```
https://github.com/Abdelrhman-AK/WinPaletter-Store/blob/main/store.wpdb?raw=true
```

### Adding Online Sources in WinPaletter

- Open WinPaletter → Settings → Store.
- Toggle Online.
- Press the `+` button and add your source URL.
- Append ?raw=true for GitHub repository links.
- Remove sources as desired (built-in sources cannot be removed).
- Click Save and open WinPaletter Store.

### Adding Offline Sources in WinPaletter

- Open WinPaletter → Settings → Store.
- Toggle Offline.
- Press the `+` button and add folders containing your saved themes.
- Remove sources as needed.
- Click Save and open WinPaletter Store.


### Summary:

- This guide allows developers to host custom online or offline theme sources.
- Supports GitHub repositories, direct links, and local directories.
- Built-in sources in WinPaletter are permanent and cannot be removed.