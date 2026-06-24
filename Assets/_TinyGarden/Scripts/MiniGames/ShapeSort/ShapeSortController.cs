using UnityEngine;
using System.Collections.Generic;
using TinyGarden.Core;
using TinyGarden.MiniGames.Shared;
using TinyGarden.Audio;

namespace TinyGarden.MiniGames.ShapeSort
{
    public class ShapeSortController : MiniGameBase
    {
        [SerializeField] private ShapeSortDefinition definition;
        [SerializeField] private GameObject celebrationPanel;

        private Dictionary<string, ShapeDropTarget> targets = new Dictionary<string, ShapeDropTarget>();
        private int correctMatches = 0;
        private int totalRequired = 0;

        protected override void Start()
        {
            base.Start();

            if (celebrationPanel != null)
                celebrationPanel.SetActive(false);

            // Register targets
            var foundTargets = FindObjectsOfType<ShapeDropTarget>();
            totalRequired = foundTargets.Length;

            foreach (var target in foundTargets)
            {
                if (!targets.ContainsKey(target.MatchId))
                {
                    targets.Add(target.MatchId, target);
                }
                target.OnItemDropped += HandleItemDropped;
            }

            if (definition != null && !string.IsNullOrEmpty(definition.introVoicePromptId))
            {
                AudioManager.Instance?.PlayVoice(definition.introVoicePromptId);
            }
        }

        private void HandleItemDropped(DropTargetBase target, DragItemBase item, bool isCorrect)
        {
            if (isCorrect)
            {
                correctMatches++;
                if (correctMatches >= totalRequired)
                {
                    OnGameComplete();
                }
            }
        }

        public void TriggerHintForShape(string matchId)
        {
            if (targets.TryGetValue(matchId, out ShapeDropTarget target))
            {
                target.PulseHint();
                AudioManager.Instance?.PlaySfx("sfx_hint_boing"); // soft hint
            }
        }

        private void OnGameComplete()
        {
            Debug.Log("[ShapeSort] All shapes matched! Showing celebration.");
            
            if (celebrationPanel != null)
            {
                celebrationPanel.SetActive(true);
            }

            if (GameManager.Instance != null && GameManager.Instance.SaveSystem != null)
            {
                var id = definition != null ? definition.activityId : ActivityId.ShapeSort;
                GameManager.Instance.SaveSystem.MarkActivityCompleted(id);
            }

            AudioManager.Instance?.PlaySfx("sfx_celebration");
        }

        public void ReturnToGarden()
        {
            SceneLoader.Instance.LoadScene(SceneNames.Garden);
        }

        private void OnDestroy()
        {
            foreach (var target in targets.Values)
            {
                target.OnItemDropped -= HandleItemDropped;
            }
        }
    }
}
