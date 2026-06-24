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
        public void AnimalUnlocked_WhenAllThreeActivitiesCompleted()
        {
            saveSystem.Load();
            
            saveSystem.MarkActivityCompleted(ActivityId.ColorMatch);
            Assert.IsFalse(saveSystem.CurrentData.AnimalFriendUnlocked);
            
            saveSystem.MarkActivityCompleted(ActivityId.CountingFruits);
            Assert.IsFalse(saveSystem.CurrentData.AnimalFriendUnlocked);
            
            saveSystem.MarkActivityCompleted(ActivityId.ShapeSort);
            Assert.IsTrue(saveSystem.CurrentData.AnimalFriendUnlocked);
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
