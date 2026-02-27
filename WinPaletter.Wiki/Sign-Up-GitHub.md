# Sign in with GitHub

WinPaletter allows you to connect your GitHub account to unlock advanced store and theme management features. This page explains why GitHub sign-in is required, what permissions are requested, and how your data is handled.

---

## Overview

By signing in with GitHub, WinPaletter can:

- Create and manage your personal fork of the WinPaletter Store repository.
- Upload, update, and manage your custom themes.
- Enable future GitHub-integrated features.
- Optionally allow you to contribute your work to the community.

---

## Why Sign In Is Required

GitHub authentication is necessary to enable the following features:

### 1. Fork the Official Store Repository
WinPaletter automatically forks the official WinPaletter Store repository to your GitHub account.  
This fork acts as your personal theme repository.

### 2. Upload and Manage Custom Themes
You can:
- Upload new themes
- Update existing themes
- Manage your theme repository directly from WinPaletter

### 3. Contribute to the Community (Optional)
- Send themes publication request (pull requests) -> Share your themes publicly

---

## Permissions Requested

WinPaletter requests GitHub’s **`repo` scope** (advanced repository access).

This includes permission to:

- Access repository code
- Create and manage issues
- Create and manage pull requests
- Access and edit wikis
- Modify repository settings
- Manage webhooks
- Handle collaboration invites
- Access organization projects and team management (if applicable)

### Why These Permissions Are Needed

GitHub requires the `repo` scope for applications that:

- Fork repositories
- Commit changes
- Push updates
- Create pull requests on your behalf

Without this scope, theme publishing and store integration would not function.

---

## Privacy & Security

WinPaletter is focusing on protecting your privacy and ensuring security:

### Credential Storage
- Your GitHub token and credentials are stored securely in **Windows Credential Manager**.
- Your GitHub password is never directly stored by WinPaletter.

### Repository Access
- Read/write access to public and private repositories is required for full functionality.
- Access is limited to operations needed for theme and store management.

### Revoking Access
You can revoke WinPaletter’s access at any time:

1. Open GitHub.
2. Go to **Settings → Applications → Authorized OAuth Apps**.
3. Remove WinPaletter from the list.

Once revoked, WinPaletter will no longer have access to your repositories.

### Extra Privacy Option
If you prefer separation from your main GitHub account:
- Create and use a secondary GitHub account specifically for WinPaletter.

---

## What Happens After Signing In

After successful authentication:

1. WinPaletter verifies your GitHub identity.
2. The official store repository is forked to your account (if not already).
3. Your local WinPaletter instance is linked to your fork.
4. You can immediately begin uploading or managing themes.

---

## Troubleshooting

### Sign-in Fails
- Ensure your browser is not blocking pop-ups.
- Confirm you granted full `repo` permissions.
- Check that GitHub is not experiencing service issues.

### Fork Not Created
- Verify that you have permission to create repositories.
- Ensure your GitHub account has not reached repository limits.

---

## How to Sign up with GitHub to use WinPaletter's GitHub Manager to upload and publish your themes

1. You can start by one method of these to sign up:

|Method 1|Method 2|
|--------|--------|
| Click on users button in main form and start sign up process | Open WinPaletter Store for themes, Click on users button in main form and start sign up process|
|![Main](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/ColorControlShape/shape.png?raw=true)
|   |

2. Read the terms and click on `Sign up`
3. Wait until a browser instance starts
4. Select your targer account
5. GitHub will require a code, it is provided by WinPaletter, copy it from this button.
6. Paste it in browser and continue
7. Read info and continue
8. `Optional` If you are protecting your account by 2-Factor Authentication, enter its code and continue.
9. Finally, your browser will tell you that you are connected and so WinPaletter.
10. You will find user name and avatar visible in Main form, an indication of loggin in.
11. Evreytime you open WinPaletter, it will sign in automatically using credentials saved in Windows Credentials.

---

## Sign out:
