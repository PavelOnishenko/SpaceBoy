using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Menu
{
    internal class MenuState : MonoBehaviour
    {
        [SerializeField] GameObject levelCaption;
        [SerializeField] GameObject levelPreview;

        private int selectedLevel = 1;
        private int lastLevel;
        private LevelCaptionBehavior levelCaptionBehavior;
        private LevelPreviewBehavior levelPreviewBehavior;

        private Dictionary<int, string> levelSceneNames = new Dictionary<int, string>()
        {
            { 1, "LevelHallwayScene" },
            { 2, "LevelWindowScene" }
        };

        private void Start()
        {
            lastLevel = Enum.GetValues(typeof(Level)).Length;
            levelCaptionBehavior = levelCaption.GetComponent<LevelCaptionBehavior>();
            levelPreviewBehavior = levelPreview.GetComponent<LevelPreviewBehavior>();
            IntersceneState.Instance.SelectProtagonist(CharacterType.SpaceGirl);
        }

        public void SelectLevelToLeft()
        {
            if (selectedLevel <= 1)
                selectedLevel = lastLevel;
            else
                selectedLevel--;
            RefreshUi();
        }

        public void SelectLevelToRight()
        {
            if (selectedLevel >= lastLevel)
                selectedLevel = 1;
            else
                selectedLevel++;
            RefreshUi();
        }

        private void RefreshUi()
        {
            levelCaptionBehavior.SetLevelCaption(selectedLevel);
            levelPreviewBehavior.SetLevelPreview(selectedLevel);
            var nextSceneName = levelSceneNames[selectedLevel];
            SceneOrder.Instance.SetNextScene(nextSceneName);
        }
    }
}
