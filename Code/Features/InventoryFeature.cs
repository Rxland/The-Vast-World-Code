using System.Collections.Generic;
using _GAME.Code.Save_Data;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.Types;
using Zenject;

namespace _GAME.Code.Features
{
    public class InventoryFeature
    {
        public InventoryItemSaveData SelectedInventoryItemSaveData;
        
        [Inject] private SaveFeature _saveFeature;
        [Inject] private StaticDataFeature _staticDataFeature;

        public InventorySaveData GetInventorySaveData()
        {
            return _saveFeature.SaveData.InventorySaveData;
        }

        public void Save()
        {
            _saveFeature.SaveGame();
        }

        public float GetAllEquippedStats(StatType statType)
        {
            float value = 0f;
            
            foreach (InventoryItemSaveData inventoryItemSaveData in _saveFeature.SaveData.InventorySaveData.SelectedItems)
            {
                InventoryItemStaticData inventoryItemStaticData = GetInventoryItemStaticData(inventoryItemSaveData);
                InventoryItemUpgrades inventoryItemUpgrades = inventoryItemStaticData.GetInventoryItemUpgrades(inventoryItemSaveData.CurrentUpgradeLevel);

                foreach (InventoryItemStat inventoryItemStat in inventoryItemUpgrades.InventoryItemStatList)
                {
                    if (inventoryItemStat.StatType == statType)
                    {
                        value += inventoryItemStat.Value;
                    }
                }
            }

            return value;
        }

        public InventoryItemStaticData GetInventoryItemStaticData(InventoryItemSaveData inventoryItemSaveData)
        {
            foreach (InventoryItemStaticData inventoryItemStaticData in _staticDataFeature.InventoryItemStaticDataList)
            {
                if (inventoryItemStaticData.LevelId == inventoryItemSaveData.ItemLevel && inventoryItemStaticData.InventoryItemType == inventoryItemSaveData.InventoryItemType)
                    return inventoryItemStaticData;
            }

            return null;
        }
        
        public InventoryItemSaveData GetInventoryItemSaveData(InventoryItemStaticData inventoryItemStaticData)
        {
            List<InventoryItemSaveData> allItemsSavesFromSaves = new (_saveFeature.SaveData.InventorySaveData.SelectedItems);
            allItemsSavesFromSaves.AddRange(_saveFeature.SaveData.InventorySaveData.ItemsOnStash);

            foreach (InventoryItemSaveData allItemsSavesFromSave in allItemsSavesFromSaves)
            {
                if (allItemsSavesFromSave.ItemLevel == inventoryItemStaticData.LevelId &&
                    allItemsSavesFromSave.InventoryItemType == inventoryItemStaticData.InventoryItemType)
                {
                    return allItemsSavesFromSave;
                }
            }

            return null;
        }
    }
}