# Android Release Checklist

To build and publish Tiny Garden Helper to the Google Play Store, follow these steps strictly within the Unity Editor.

## 1. Pre-Build Validations
- Run **Tiny Garden > Check Build Readiness** from the Unity top menu. Ensure it prints `SUCCESS` in green.
- Verify `Docs/RELEASE_CANDIDATE_QA.md` has been fully executed on a physical Android device.

## 2. Player Settings Configuration (Edit > Project Settings > Player)
- **Company Name**: Your Studio Name (e.g., `Tiny Studio`)
- **Product Name**: `Tiny Garden Helper`
- **Version**: E.g., `0.1.0`
- **Default Icon**: Ensure the 1024x1024 placeholder icon is assigned.

### Resolution and Presentation
- **Orientation**: Default Orientation must be `Portrait`.

### Other Settings (Android Tab)
- **Package Name**: Replace `com.DefaultCompany.TinyGardenHelper` with `com.yourstudio.tinygardenhelper`.
- **Minimum API Level**: Set to `Android 8.0 (API level 26)` for broad compatibility.
- **Target API Level**: Set to `Automatic (highest installed)` (Must be at least API 33+ for Google Play).
- **Scripting Backend**: Change from `Mono` to `IL2CPP`.
- **Api Compatibility Level**: `.NET Standard 2.1`.
- **Target Architectures**: Check both `ARMv7` and `ARM64`. (Google Play requires 64-bit support).

## 3. Publishing Settings (Keystore)
*Note: Never commit your keystore or passwords to version control.*
- Under **Publishing Settings**, check `Custom Keystore`.
- Click `Keystore Manager` -> `Create New`.
- Save the `.keystore` file securely *outside* of the Unity project directory.
- Enter strong passwords for both the Keystore and the Key alias.
- Enter these passwords into the Unity Player Settings before building.

## 4. Build Generation
- Open **File > Build Settings**.
- Ensure **Android** is the active platform.
- Check the box for **Build App Bundle (Google Play)**. This generates an `.aab` file instead of an `.apk`.
- Click **Build**. Save the file as `TinyGardenHelper_v0.1.0.aab`.

## 5. Google Play Console Submission
- Upload the `.aab` to a new Internal Testing track.
- Fill out the **Data Safety** form stating no data is collected.
- Opt into the **Google Play Families Program** (Target age: 3-5 and 6-8).
- Declare that the app contains **No Ads**.
