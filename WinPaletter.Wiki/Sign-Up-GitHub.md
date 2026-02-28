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

## How to Sign In with GitHub to Use WinPaletter’s GitHub Manager

Follow these steps to connect your GitHub account and enable theme upload and publishing.

---

### 1. Start the Sign-In Process

You can begin using one of the following methods:

| Method 1 | Method 2 |
|----------|----------|
| Click the **Users** button on the main form to open the user dashboard. | Open **WinPaletter Store**, then click the **Users** button to open the user dashboard. |
| ![Main](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/MainForm_User.png?raw=true) ![UsersDashboard](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Users_Dashboard.png?raw=true) | ![Store](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Store_UsersDashboard.png?raw=true) |

---

### 2. Accept Terms

- Read the information displayed.
- Click **`Sign up`** to continue.

---

### 3. Wait for Browser Authentication

A browser window will automatically open for GitHub authentication.

![Wait](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/SignUp_Wait.png?raw=true)

---

### 4. Choose Your GitHub Account

Select the GitHub account you want to connect.

![SelectAccount](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Browser_ChooseAccount.png?raw=true)

---

### 5. Enter the Authorization Code

GitHub will request a verification code.

- Copy the code provided by WinPaletter using the button shown below.
- Paste it into the browser page.

![AuthCode](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/SignUp_AuthCode.png?raw=true)

---

### 6. Confirm Authorization

- Review the requested permissions.
- Click **Continue** to approve.

![Terms](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Browser_Auth.png?raw=true)

---

### 7. Two-Factor Authentication (If Enabled)

If your GitHub account uses 2FA:

- Enter your authentication code.
- Continue to complete the process.

![2FA](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Browser_2FA.png?raw=true)

---

### 8. Successful Connection

Once authentication is complete:

- The browser will confirm successful connection.
- WinPaletter will also display a success message.

![Success](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Browser_Success.png?raw=true)  
![Success](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/SignUp_Success.png?raw=true)

---

### 9. Logged-In Indicator

Your username and avatar will appear on the main form, confirming that you are signed in.

![MainFormAvatar](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/MainForm_LoggedIn.png?raw=true)

---

### 10. Automatic Sign-In

Each time you open WinPaletter, it will automatically sign you in using credentials stored in **Windows Credential Manager**.

---

# Sign Out

To disconnect your GitHub account:

---

### 1. Start the Sign-Out Process

You can sign out using one of the following methods:

| Method 1 | Method 2 |
|----------|----------|
| Click the **Users** button on the main form and choose sign out. | Open **WinPaletter Store**, then click the **Users** button and choose sign out. |
| ![MainFormAvatar](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/MainForm_LoggedIn.png?raw=true) ![UsersDashboard](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Users_Dashboard_SignOut.png?raw=true) | ![Store](https://github.com/Abdelrhman-AK/WinPaletter/blob/master/WinPaletter.Wiki/Assets/GitHub_SignUp/Store_UsersDashboard_SignOut.png?raw=true) |

---

### 2. Complete Sign Out

- Your saved GitHub credentials will be removed from Windows Credential Manager.
- A browser page will open allowing you to revoke WinPaletter’s access.
- Revoke access manually from that page to fully disconnect.

You are now signed out.