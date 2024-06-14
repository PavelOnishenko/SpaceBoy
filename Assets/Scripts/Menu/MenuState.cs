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
            IntersceneState.Instance.SelectLevel((Level)selectedLevel);
        }

        public void SelectLevelToRight()
        {
            if (selectedLevel >= lastLevel)
                selectedLevel = 1;
            else
                selectedLevel++;
            RefreshUi();
            IntersceneState.Instance.SelectLevel((Level)selectedLevel);
        }

        private void RefreshUi()
        {
            levelCaptionBehavior.SetLevelCaption(selectedLevel);
            levelPreviewBehavior.SetLevelPreview(selectedLevel);
        }
    }
}