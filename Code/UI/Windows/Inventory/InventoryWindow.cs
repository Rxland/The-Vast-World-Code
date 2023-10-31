using System.Collections.Generic;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Game_State_Machine;
using _GAME.Code.Save_Data;
using _GAME.Code.Types;
using _GAME.Code.UI.Buttons;
using I2.Loc;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI.Windows.Inventory
{
    public class InventoryWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [Space]
        
        [SerializeField] private List<InventoryCardItem> _inventoryEquipSlotsList;
        [SerializeField] private List<InventoryCardItem> _spawnedInventoryCardsInStash;
        [SerializeField] private Transform _cardsInStashContainer;
        [SerializeField] private SimpleButton _settingsButton;
        [Space] 
        
        private TutorialsSaveData _tutorialsSaveData;
        private string _tutorName = "Inventory_Tutor";

        [Inject] private InventoryFeature _inventoryFeature;
        [Inject] private UiFactory _uiFactory;
        [Inject] private TutorialFeature _tutorialFeature;
        [Inject] private RunTimeDataFeature _runTimeDataFeature;
        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;
        
        [Button]
        public override void OpenWindow()
        {
            base.OpenWindow();

            InitLevelText();
            InitButtons();
            UpdateStashCards();
            InitSelectedItems();
            TryShowTutor();
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
        }

        private void InitLevelText()
        {
            _currentLevelText.text = $"{LocalizationManager.GetTranslation("Level")} {_saveReactivePropertiesFeature.LevelsDoneAmount.Value + 1}";
        }
        
        private void InitButtons()
        {
            BackButton.Button.onClick.RemoveListener(BackButtonClick);
            _settingsButton.Button.onClick.RemoveAllListeners();
            
            BackButton.Button.onClick.AddListener(BackButtonClick);
            _settingsButton.Button.onClick.AddListener(() => WindowFactory.SpawnWindow(WindowName.Settings));
        }

        private void UpdateStashCards()
        {
            TryClearSpawnedInventoryCardsInStash();
            
            InventorySaveData inventorySaveData = _inventoryFeature.GetInventorySaveData();

            for (int i = 0; i < inventorySaveData.ItemsOnStash.Count; i++)
            {
                InventoryItemSaveData inventoryItemSaveData = inventorySaveData.ItemsOnStash[i];
                InventoryCardItem inventoryCardItem = _uiFactory.SpawnInventoryCardItem(_cardsInStashContainer);

                inventoryCardItem.name = $"Item Card {i + 1}";
                
                inventoryCardItem.InventoryItemSaveData = inventoryItemSaveData;
                inventoryCardItem.Init();

                _spawnedInventoryCardsInStash.Add(inventoryCardItem);
            }
        }
        
        private void TryClearSpawnedInventoryCardsInStash()
        {
            if (_spawnedInventoryCardsInStash.Count == 0) return;

            for (int i = 0; i < _spawnedInventoryCardsInStash.Count; i++)
            {
                InventoryCardItem inventoryCardItem = _spawnedInventoryCardsInStash[i];
                Destroy(inventoryCardItem.gameObject);
            }
            _spawnedInventoryCardsInStash.Clear();
        }

        private void InitSelectedItems()
        {
            InventorySaveData inventorySaveData = _inventoryFeature.GetInventorySaveData();

            foreach (InventoryItemSaveData inventoryItemSaveData in inventorySaveData.SelectedItems)
            {
                InventoryCardItem inventoryCardItem = _inventoryEquipSlotsList.Find(x => x.InventoryItemType == inventoryItemSaveData.InventoryItemType);
                inventoryCardItem.InventoryItemSaveData = inventoryItemSaveData;
                inventoryCardItem.Init();
            }
            
            foreach (InventoryCardItem inventoryCardItem in _inventoryEquipSlotsList)
            {
                if (inventoryCardItem.InventoryItemSaveData.IsEquipped) continue;
                
                inventoryCardItem.NoOneCardSelectedImg.gameObject.SetActive(true);
                inventoryCardItem.BottomPart.gameObject.SetActive(false);
            }
        }
        
        private void TryShowTutor()
        {
            _tutorialsSaveData = _tutorialFeature.GetTutorialsSaveData();
            
            if (_tutorialsSaveData.IsInventoryTutorDone || _runTimeDataFeature.TutorRunTimeData.IsTutorPlaying) return;
            
            _tutorialFeature.LoadTutor(_tutorName, OnTutorDone);

            _runTimeDataFeature.TutorRunTimeData.IsTutorPlaying = true;
        }

        private void OnTutorDone()
        {
            _tutorialFeature.HideTutor();
            
            _runTimeDataFeature.TutorRunTimeData.IsTutorPlaying = false;
            _tutorialsSaveData.IsInventoryTutorDone = true;
            _tutorialFeature.Save();
        }
        
        private void BackButtonClick()
        {
            CloseWindow();
            _gameStateMachine.ChangeState(GameStateType.GameRun);
        }
    }
}