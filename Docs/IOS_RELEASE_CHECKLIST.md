# iOS Release Checklist

To build and publish Tiny Garden Helper to the Apple App Store, follow these steps. Note: An Apple Mac computer and Xcode are required.

## 1. Pre-Build Validations
- Run **Tiny Garden > Check Build Readiness** from the Unity top menu. Ensure it prints `SUCCESS` in green.
- Verify `Docs/RELEASE_CANDIDATE_QA.md` has been fully executed on an iOS device via TestFlight or local build.

## 2. Unity Player Settings Configuration (Edit > Project Settings > Player)
- **Company Name**: Your Studio Name
- **Product Name**: `Tiny Garden Helper`
- **Version**: E.g., `0.1.0`
- **Build Number**: Increment this integer for every single build uploaded to App Store Connect (e.g., `1`, `2`, `3`).

### Resolution and Presentation
- **Orientation**: Default Orientation must be `Portrait`.
- **Hide Home Button on iPhone X**: Optional, but recommended to keep unchecked so the child doesn't accidentally trigger multitasking while playing.

### Other Settings (iOS Tab)
- **Bundle Identifier**: E.g., `com.yourstudio.tinygardenhelper`.
- **Target Device**: `iPhone + iPad`.
- **Target SDK**: `Device SDK`.
- **Target minimum iOS Version**: Set to `12.0` or `13.0` for broad compatibility.
- **Scripting Backend**: Must be `IL2CPP` (Apple requirement).
- **Architecture**: `ARM64`.

## 3. Exporting the Xcode Project
- Open **File > Build Settings**.
- Switch to the **iOS** platform.
- Ensure the scene list is correct.
- Click **Build**. Unity will ask for a folder. Create a folder named `iOS_Build_v0.1.0` outside of your main repository folder.

## 4. Xcode Configuration (On a Mac)
- Open the generated `Unity-iPhone.xcodeproj` in Xcode.
- Click the top-level `Unity-iPhone` project in the left navigator.
- Go to the **Signing & Capabilities** tab.
- Check `Automatically manage signing`.
- Select your Apple Developer Program Team from the dropdown.

## 5. Privacy Manifest
- Apple requires a Privacy Manifest for new apps. Unity 2022+ handles this automatically. Verify that `PrivacyInfo.xcprivacy` exists in the Xcode project and accurately declares that no tracking occurs.

## 6. Archiving and Uploading
- In Xcode, set the target device at the top center to **Any iOS Device (arm64)**.
- From the top menu, select **Product > Archive**.
- Once the archive completes, the Organizer window will open.
- Click **Distribute App** -> **TestFlight & App Store** -> **Upload**.

## 7. App Store Connect Submission
- In App Store Connect, go to App Privacy. Define that the app collects **No Data**.
- Check the box to enroll in the **Kids Category** (Ages 5 and Under, or Ages 6-8).
- Ensure the parental gate functionality works flawlessly before submitting to App Review, as Apple reviewers rigorously test Kids Category apps.
