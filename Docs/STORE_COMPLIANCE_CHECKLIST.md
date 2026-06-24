# Store Compliance Checklist

To ensure approval on the Apple App Store (Apple Kids Category and App Store Review Guidelines) and Google Play (Families Program), the MVP must maintain strict compliance. Before submitting a build, verify all items below. This checklist is designed to support child-safe, offline-first compliance review.

## 1. Network & Privacy
- [ ] No third-party ad networks (AdMob, UnityAds, etc.).
- [ ] No third-party analytics (Firebase, Flurry, Adjust, AppsFlyer, etc.).
- [ ] No external web links accessible by the child.
- [ ] No social sharing functionality (Facebook, Twitter, etc.).
- [ ] No user-generated content (UGC) that is shared online.
- [ ] Progress is saved 100% locally on the device via `LocalJsonSaveSystem`.

## 2. Monetization
- [ ] No In-App Purchases (IAP) in the MVP.
- [ ] No virtual currency that behaves like a slot machine.
- [ ] No subscription prompts.

## 3. Account & Identity
- [ ] No login system required to play.
- [ ] No request for Personally Identifiable Information (PII) like name, age, email, or location.
- [ ] No push notifications.

## 4. Child Interface
- [ ] Any adult-oriented actions (e.g., accessing Settings, resetting progress, privacy policy viewing) must be behind a robust Parental Gate (e.g., a math question or "Press and Hold for 3 seconds" mechanic).
- [ ] Language and content are strictly suitable for ages 3-7.
- [ ] Icons and marketing screenshots accurately reflect the gameplay (no misleading advertisements).
