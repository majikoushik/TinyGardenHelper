using NUnit.Framework;
using TinyGarden.SaveSystem;
using TinyGarden.MiniGames.Shared;
using System.IO;

namespace TinyGarden.Tests.EditMode
{
    public class SaveSystemTests
    {
        private LocalJsonSaveSystem saveSystem;
        private string testDirectory;

        [SetUp]
        public void Setup()
        {
            testDirectory = Path.Combine(UnityEngine.Application.temporaryCachePath, "TestSaves");
            if (!Directory.Exists(testDirectory))
            {
                Directory.CreateDirectory(testDirectory);
            }
            saveSystem = new LocalJsonSaveSystem(testDirectory);
            
            // Ensure clean state
            string saveFile = Path.Combine(testDirectory, "save.json");
            if (File.Exists(saveFile)) File.Delete(saveFile);
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }
        }

        [Test]
        public void Load_WhenNoFileExists_CreatesDefaultSave()
        {
            var data = saveSystem.Load();
            
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.SaveVersion);
            Assert.IsFalse(data.AnimalFriendUnlocked);
            Assert.AreEqual(0, data.Activities.Count);
        }

        [Test]
        public void MarkActivityCompleted_SavesProgress()
        {
            saveSystem.Load();
            saveSystem.MarkActivityCompleted(ActivityId.ColorMatch);
            
            // Reload from disk to verify it wrote
            var freshSystem = new LocalJsonSaveSystem(testDirectory);
            var freshData = freshSystem.Load();

            Assert.AreEqual(1, freshData.Activities.Count);
            Assert.AreEqual(ActivityId.ColorMatch, freshData.Activities[0].ActivityId);
            Assert.IsTrue(freshData.Activities[0].Completed);
            Assert.AreEqual(1, freshData.Activities[0].SuccessfulCompletions);
        }

        [Test]
        public void AllThreeActivitiesCompleted_SavedCorrectly()
        {
            // Verify that completing all 3 MVP activities records them as completed in the save data.
            // Note: AnimalFriendUnlocked is NOT set here — that is the RewardSystem's responsibility.
            // GardenProgressPresenter reads these completed flags and triggers RewardSystem.GrantReward().
            saveSystem.Load();

            saveSystem.MarkActivityCompleted(ActivityId.ColorMatch);
            saveSystem.MarkActivityCompleted(ActivityId.CountingFruits);
            saveSystem.MarkActivityCompleted(ActivityId.ShapeSort);

            bool colorDone = saveSystem.CurrentData.Activities.Exists(a => a.ActivityId == ActivityId.ColorMatch && a.Completed);
            bool countingDone = saveSystem.CurrentData.Activities.Exists(a => a.ActivityId == ActivityId.CountingFruits && a.Completed);
            bool shapeDone = saveSystem.CurrentData.Activities.Exists(a => a.ActivityId == ActivityId.ShapeSort && a.Completed);

            Assert.IsTrue(colorDone, "ColorMatch should be marked completed.");
            Assert.IsTrue(countingDone, "CountingFruits should be marked completed.");
            Assert.IsTrue(shapeDone, "ShapeSort should be marked completed.");

            // AnimalFriendUnlocked is ONLY set by RewardSystem.GrantReward("animal_benny_bunny").
            // This save-system test deliberately does NOT set it, to keep responsibilities clear.
            Assert.IsFalse(saveSystem.CurrentData.AnimalFriendUnlocked, "SaveSystem alone must not auto-unlock the animal.");
        }

        [Test]
        public void AnimalFriendUnlocked_PersistsAfterSaveAndLoad()
        {
            // When RewardSystem sets AnimalFriendUnlocked=true and saves, it persists on reload.
            var data = saveSystem.Load();
            data.AnimalFriendUnlocked = true;
            data.UnlockedRewardIds.Add("animal_benny_bunny");
            saveSystem.Save(data);

            var freshSystem = new LocalJsonSaveSystem(testDirectory);
            var freshData = freshSystem.Load();

            Assert.IsTrue(freshData.AnimalFriendUnlocked, "Animal unlock must persist after save/load.");
            Assert.IsTrue(freshData.UnlockedRewardIds.Contains("animal_benny_bunny"), "Reward ID must persist after save/load.");
        }

        [Test]
        public void SaveLoad_RoundTrip_PreservesData()
        {
            var data = saveSystem.Load();
            data.AnimalFriendUnlocked = true;
            data.Settings.MusicEnabled = false;
            saveSystem.Save(data);

            var freshSystem = new LocalJsonSaveSystem(testDirectory);
            var loadedData = freshSystem.Load();
            
            Assert.IsTrue(loadedData.AnimalFriendUnlocked);
            Assert.IsFalse(loadedData.Settings.MusicEnabled);
        }

        [Test]
        public void ResetProgress_ClearsData()
        {
            saveSystem.Load();
            saveSystem.MarkActivityCompleted(ActivityId.ColorMatch);
            Assert.AreEqual(1, saveSystem.CurrentData.Activities.Count);
            
            saveSystem.ResetProgress();
            Assert.AreEqual(0, saveSystem.CurrentData.Activities.Count);
        }

        [Test]
        public void CorruptedSave_CreatesBackupAndStartsFresh()
        {
            // Create a corrupt JSON file
            string saveFile = Path.Combine(testDirectory, "save.json");
            File.WriteAllText(saveFile, "{ invalid_json: ");

            // Load should catch exception, backup, and return fresh
            var data = saveSystem.Load();
            
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.SaveVersion);
            
            string backupFile = Path.Combine(testDirectory, "save.json.bak");
            Assert.IsTrue(File.Exists(backupFile), "Backup file was not created.");
        }
    }
}
