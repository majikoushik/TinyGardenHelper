using System.Collections.Generic;
using UnityEngine;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Core;

namespace TinyGarden.MiniGames.ColorMatch
{
    public class ColorMatchController : MiniGameBase
    {
        [SerializeField] private ColorMatchDefinition definition;
        [SerializeField] private GameObject celebrationPanel;
        
        private List<ColorDropTarget> dropTargets = new List<ColorDropTarget>();
        private int correctMatches = 0;
        private int totalMatchesRequired = 0;

        protected override void Start()
        {
            base.Start();
            
            if (celebrationPanel != null)
                celebrationPanel.SetActive(false);

            // Find all targets and subscribe
            dropTargets.AddRange(FindObjectsOfType<ColorDropTarget>());
            totalMatchesRequired = dropTargets.Count;

            foreach (var target in dropTargets)
            {
                target.OnItemDropped += HandleItemDropped;
            }

            // Play intro prompt
            if (definition != null && !string.IsNullOrEmpty(definition.voicePromptId))
            {
                TinyGarden.Audio.AudioManager.Instance?.PlayVoice(definition.voicePromptId);
            }
        }

        private void HandleItemDropped(DropTargetBase target, DragItemBase item, bool isCorrect)
        {
            if (isCorrect)
            {
                correctMatches++;
                if (correctMatches >= totalMatchesRequired)
                {
                    OnGameComplete();
                }
            }
        }

        private void OnGameComplete()
        {
            Debug.Log("[ColorMatch] All matches correct! Showing celebration.");
            
            if (celebrationPanel != null)
            {
                celebrationPanel.SetActive(true);
            }

            // Save Progress
            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                var id = definition != null ? definition.activityId : ActivityId.ColorMatch;
                GameManager.Instance.SaveSystem.MarkActivityCompleted(id);
            }

            TinyGarden.Audio.AudioManager.Instance?.PlaySfx("sfx_celebration"); // Placeholder
        }

        public void ReturnToGarden()
        {
            SceneLoader.Instance.LoadScene(SceneNames.Garden);
        }

        private void OnDestroy()
        {
            foreach (var target in dropTargets)
            {
                target.OnItemDropped -= HandleItemDropped;
            }
        }
    }
}
