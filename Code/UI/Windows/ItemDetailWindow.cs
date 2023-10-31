using System;
using System.Collections.Generic;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Save_Data;
using _GAME.Code.Static_Data;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.Types;
using _GAME.Code.UI.Buttons;
using _GAME.Code.UI.Windows.Item_Detail;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class ItemDetailWindow : WindowBase
    {
        [Header("Item Detail Window")]
        public SimpleButton SecondCloseButton;
        public SimpleButton SelectButton;
        public SimpleButton UpgradeButton;
        [Space]
        
        public Image ItemImg;
        public Image UpgradeMoneyIconImg;
        [Space]
        
        public TextMeshProUGUI ItemLevelText;
        public TextMeshProUGUI UpgradePriceText;
        [Space] 
        
        public Transform StatsContainer;
        public List<ItemDetailStat> SpawnedItemDetailStats;

        private InventorySaveData _inventorySaveData;
        private InventoryItemSaveData _inventoryItemSaveData; 
        private InventoryItemStaticData _inventoryItemStaticData; 
        
        [Inject] private InventoryFeature _inventoryFeature;
        [Inject] private UiFactory _uiFactory;
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private MoneyFeature _moneyFeature;
        [Inject] private SaveFeature _saveFeature;
        [Inject] private LocalizationFeature _localizationFeature;
        
        public override void OpenWindow()
        {
            base.OpenWindow();
            
            Render();
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
            CursorFeature.SetCursorLockedMode(false);
        }

        public override void ReOpenWindow()
        {
            Render();
        }

        private void Render()
        {
            SecondCloseButton.Button.onClick.RemoveAllListeners();
            UpgradeButton.Button.onClick.RemoveAllListeners();
            SelectButton.Button.onClick.RemoveAllListeners();
            
            SecondCloseButton.Button.onClick.AddListener(CloseWindow);
            UpgradeButton.Button.onClick.AddListener(UpgradeCard);
            SelectButton.Button.onClick.AddListener(SelectButtonClick);
            
            _inventoryItemSaveData = _inventoryFeature.SelectedInventoryItemSaveData;
            _inventoryItemStaticData = _inventoryFeature.GetInventoryItemStaticData(_inventoryItemSaveData);
            _inventorySaveData = _inventoryFeature.GetInventorySaveData();
            
            ItemImg.sprite = _inventoryItemStaticData.ItemSprite;
            ItemLevelText.text = $"{_inventoryItemSaveData.CurrentUpgradeLevel + 1}";

            InventoryItemUpgrades nextItemUpgrades = _inventoryItemStaticData.GetInventoryItemUpgrades(_inventoryItemSaveData.CurrentUpgradeLevel + 1);
            UpgradePriceText.text = $"{nextItemUpgrades.Price}";

            if (!_moneyFeature.HaveMoneyByBuy(nextItemUpgrades.Price) || 
                !_inventoryItemStaticData.HaveCardsToUpgrade(_inventoryItemSaveData.CurrentUpgradeCardsAmount, _inventoryItemSaveData.CurrentUpgradeLevel + 1))
            {
                UpgradeButton.Button.interactable = false;
            }
            
            SpawnStats();
        }

        private void SpawnStats()
        {
            TryClearStats();
            
            List<InventoryItemStat> inventoryItemStatList;
            
            inventoryItemStatList = _inventoryItemStaticData.GetInventoryItemUpgrades(_inventoryItemSaveData.CurrentUpgradeLevel).InventoryItemStatList;

            for (int i = 0; i < inventoryItemStatList.Count; i++)
            {
                InventoryItemStat inventoryItemStat = inventoryItemStatList[i];
                
                ItemDetailStat itemDetailStat = _uiFactory.SpawnItemDetailStat(StatsContainer);
                ItemDetailStatData itemDetailStatData = _staticDataFeature.ItemDetailMainStaticData.GetItemDetailStatData(inventoryItemStat.StatType);

                itemDetailStat.IconBgImg.sprite = itemDetailStatData.ItemBgSprite; 
                itemDetailStat.IconImg.sprite = itemDetailStatData.ItemIconSprite;

                itemDetailStat.NameText.text = _localizationFeature.GetTranslateByStatType(inventoryItemStat.StatType);

                float statValue = statValue = inventoryItemStat.Value;
                
                if (inventoryItemStat.StatType == StatType.AttackSpeed)
                    statValue *= 100;
                
                InventoryItemUpgrades nextItemUpgrades = _inventoryItemStaticData.GetInventoryItemUpgrades(_inventoryItemSaveData.CurrentUpgradeLevel + 1);
                InventoryItemStat nextInventoryItemStat = nextItemUpgrades.InventoryItemStatList[i];

                float nextUpdateValue = nextInventoryItemStat.Value - inventoryItemStat.Value;
                
                if (inventoryItemStat.StatType == StatType.AttackSpeed)
                    nextUpdateValue *= 100;

                statValue = (float)Math.Round(statValue, 2);
                nextUpdateValue = (float)Math.Round(nextUpdateValue, 2);
                
                itemDetailStat.ValuesText.text = $"{statValue}  " +
                                                 $"<color=#{ColorUtility.ToHtmlStringRGBA(itemDetailStat.UpgradeValueColor)}>+{nextUpdateValue}";
                
                
                SpawnedItemDetailStats.Add(itemDetailStat);
            }
            
            if (_inventorySaveData.SelectedItems.Contains(_inventoryFeature.SelectedInventoryItemSaveData))
            {
                SelectButton.Button.interactable = false;
            }
        }

        private void TryClearStats()
        {
            if (SpawnedItemDetailStats.Count == 0) return;

            for (int i = 0; i < SpawnedItemDetailStats.Count; i++)
            {
                ItemDetailStat itemDetailStat = SpawnedItemDetailStats[i];
                
                Destroy(itemDetailStat.gameObject);
            }
            SpawnedItemDetailStats.Clear();
        }

        private void UpgradeCard()
        {
            InventoryItemUpgrades nextItemUpgrades = _inventoryItemStaticData.GetInventoryItemUpgrades(_inventoryItemSaveData.CurrentUpgradeLevel + 1);

            _inventoryItemSaveData.PreviousInventoryItemSaveData = (PreviousInventoryItemSaveData)_inventoryItemSaveData.Clone();
            
            _inventoryItemSaveData.CurrentUpgradeLevel++;
            _inventoryItemSaveData.CurrentUpgradeCardsAmount -= nextItemUpgrades.CardToUpgrade;
            
            _moneyFeature.DecreaseMoney(nextItemUpgrades.Price);
            
            _saveFeature.SaveGame();
            
            UpgradeButton.Button.onClick.RemoveAllListeners();
            
            WindowFactory.SpawnWindow(WindowName.UpgradeCardAnim);
        }

        private void SelectButtonClick()
        {
            InventoryItemSaveData busyItemInSlot = ReturnBusyItemInSlot();
            
            if (busyItemInSlot != null)
            {
                busyItemInSlot.IsEquipped = false;
                
                _inventorySaveData.ItemsOnStash.Add(busyItemInSlot);
                _inventorySaveData.SelectedItems.Remove(busyItemInSlot);
            }

            _inventoryFeature.SelectedInventoryItemSaveData.IsEquipped = true;
            
            _inventorySaveData.SelectedItems.Add(_inventoryFeature.SelectedInventoryItemSaveData);
            _inventorySaveData.ItemsOnStash.Remove(_inventoryFeature.SelectedInventoryItemSaveData);

            ReOpenWindow();
            WindowFactory.InventoryWindow.ReOpenWindow();
            
            _inventoryFeature.Save();
        }

        private InventoryItemSaveData ReturnBusyItemInSlot()
        {
            InventoryItemSaveData inventoryItemSaveData = _inventorySaveData.SelectedItems.Find(x =>
                x.InventoryItemType == _inventoryFeature.SelectedInventoryItemSaveData.InventoryItemType);

            return inventoryItemSaveData;
        }
    }
}