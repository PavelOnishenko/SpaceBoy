using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

namespace Assets.Scripts.Behaviors.Menu
{
    internal class MenuState : MonoBehaviour
    {
        [SerializeField] GameObject levelCaption;
        [SerializeField] GameObject levelPreview;
        [SerializeField] GameObject characterPortraitGo;

        private int selectedLevel = 1;
        private int selectedCharacterIndex = 1;
        private int lastAvailableLevel;
        private LevelCaptionBehavior levelCaptionBehavior;
        private LevelPreviewBehavior levelPreviewBehavior;
        private CharacterPortraitBehavior characterPortraitBehavior;

        private bool needToRefreshUi = false;

        private async void Start()
        {
            await TurnOnAnalyticsAsync();
            var lastCompletedLevel = PlayerPrefs.GetInt(PlayerPrefNames.LastCompletedLevel.ToString());
            var lastPossibleLevel = Enum.GetValues(typeof(Level)).Length;
            lastAvailableLevel = lastCompletedLevel == lastPossibleLevel ? lastCompletedLevel : lastCompletedLevel + 1;
            levelCaptionBehavior = levelCaption.GetComponent<LevelCaptionBehavior>();
            levelPreviewBehavior = levelPreview.GetComponent<LevelPreviewBehavior>();
            characterPortraitBehavior = characterPortraitGo.GetComponent<CharacterPortraitBehavior>();
            var lastSelectedProtagonistName = PlayerPrefs.GetString(PlayerPrefNames.LastSelectedProtagonist.ToString());
            if (Enum.TryParse<CharacterType>(lastSelectedProtagonistName, true, out var lastSelectedProtagonist))
                IntersceneState.Instance.SelectedProtagonist = lastSelectedProtagonist;
            else
                IntersceneState.Instance.SelectedProtagonist = CharacterType.SpaceGirl;
            selectedCharacterIndex = protagonistPortraitsOrder.ToList().IndexOf(IntersceneState.Instance.SelectedProtagonist);
            selectedLevel = lastAvailableLevel;
            IntersceneState.Instance.SelectedLevel = (Level)selectedLevel;
            needToRefreshUi = true;
        }

        private static async Task TurnOnAnalyticsAsync()
        {
            try
            {
                await UnityServices.InitializeAsync();
                AnalyticsService.Instance.StartDataCollection();
            }
            catch (Exception e)
            {
                Debug.Log("Analytics error! Description below:");
                Debug.LogException(e);
            }
        }

        private void Update()
        {
            if(needToRefreshUi) 
            {
                RefreshUi();
                needToRefreshUi = false;
            }
        }

        // todo refactor 4 times
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

        public void SelectCharacterToLeft()
        {
            if (selectedCharacterIndex <= 0) 
                selectedCharacterIndex = protagonistPortraitsOrder.Length - 1;
            else 
                selectedCharacterIndex--;
            RefreshUi();
            IntersceneState.Instance.SelectedProtagonist = protagonistPortraitsOrder[selectedCharacterIndex];
        }

        public void SelectCharacterToRight()
        {
            if (selectedCharacterIndex >= protagonistPortraitsOrder.Length - 1) 
                selectedCharacterIndex = 0;
            else 
                selectedCharacterIndex++;
            RefreshUi();
            IntersceneState.Instance.SelectedProtagonist = protagonistPortraitsOrder[selectedCharacterIndex];
        }

        private void RefreshUi()
        {
            levelCaptionBehavior.SetLevelCaption(selectedLevel);
            levelPreviewBehavior.SetLevelPreview(selectedLevel);
            characterPortraitBehavior.SetPreview(selectedCharacterIndex);
        }

        private CharacterType[] protagonistPortraitsOrder = 
            new[] { CharacterType.SpaceGirl, CharacterType.GreenGirl, CharacterType.CosmoKnight, CharacterType.Panther };
    }
}