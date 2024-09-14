using Assets.Scripts.Menu;
using System;
using UnityEngine;

namespace Assets.Scripts.Behaviors.Menu
{
    internal class MenuState : MonoBehaviour
    {
        [SerializeField] GameObject levelCaption;
        [SerializeField] GameObject levelPreview;
        [SerializeField] GameObject characterPickerGo;

        private int selectedLevel = 1;
        private int lastAvailableLevel;
        private LevelCaptionBehavior levelCaptionBehavior;
        private LevelPreviewBehavior levelPreviewBehavior;
        private CharacterPicker characterPicker;

        private bool needToRefreshUi = false;

        private void Start()
        {
            var lastCompletedLevel = PlayerPrefs.GetInt(PlayerPrefNames.LastCompletedLevel.ToString());
            var lastPossibleLevel = Enum.GetValues(typeof(Level)).Length;
            lastAvailableLevel = lastCompletedLevel == lastPossibleLevel ? lastCompletedLevel : lastCompletedLevel + 1;
            levelCaptionBehavior = levelCaption.GetComponent<LevelCaptionBehavior>();
            levelPreviewBehavior = levelPreview.GetComponent<LevelPreviewBehavior>();
            characterPicker = characterPickerGo.GetComponent<CharacterPicker>();
            var lastSelectedProtagonistName = PlayerPrefs.GetString(PlayerPrefNames.LastSelectedProtagonist.ToString());
            if (Enum.TryParse<CharacterType>(lastSelectedProtagonistName, true, out var lastSelectedProtagonist))
                IntersceneState.Instance.SelectedProtagonist = lastSelectedProtagonist;
            else
                IntersceneState.Instance.SelectedProtagonist = CharacterType.SpaceGirl;
            selectedLevel = lastAvailableLevel;
            IntersceneState.Instance.SelectedLevel = (Level)selectedLevel;
            needToRefreshUi = true;
        }

        private void Update()
        {
            if(needToRefreshUi) 
            {
                RefreshUi();
                needToRefreshUi = false;
            }
        }

        public void SelectLevelToLeft()
        {
            if (selectedLevel <= 1) selectedLevel = lastAvailableLevel;
            else selectedLevel--;
            RefreshUi();
            IntersceneState.Instance.SelectedLevel = (Level)selectedLevel;
        }

        public void SelectLevelToRight()
        {
            if (selectedLevel >= lastAvailableLevel) selectedLevel = 1;
            else selectedLevel++;
            RefreshUi();
            IntersceneState.Instance.SelectedLevel = (Level)selectedLevel;
        }

        private void RefreshUi()
        {
            levelCaptionBehavior.SetLevelCaption(selectedLevel);
            levelPreviewBehavior.SetLevelPreview(selectedLevel);
            characterPicker.Pick(IntersceneState.Instance.SelectedProtagonist);
        }
    }
}