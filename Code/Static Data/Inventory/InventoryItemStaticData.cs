using System;
using System.Collections.Generic;
using _GAME.Code.Types;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME.Code.Static_Data.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Item Static Data", menuName = "Static Data/Inventory/Inventory Item Static Data")]
    public class InventoryItemStaticData : ScriptableObject
    {
        public InventoryItemType InventoryItemType;
        public int LevelId;
        [Space]
        
        [PreviewField] public Sprite ItemSprite;
        [Space] 
        
        public float UpdateScaleValue = 1f;
        public InventoryItemUpgrades InventoryItemUpgradesScale;


        public InventoryItemUpgrades GetInventoryItemUpgrades(int currentCardUpdateLevel)
        {
            float scale = 1f;

            if (currentCardUpdateLevel > 0)
                scale *= (UpdateScaleValue * currentCardUpdateLevel / 10) + 1f;

            InventoryItemUpgrades inventoryItemUpgrades = new InventoryItemUpgrades();
            inventoryItemUpgrades.CardToUpgrade = Mathf.RoundToInt(InventoryItemUpgradesScale.CardToUpgrade * scale);
            inventoryItemUpgrades.Price = Mathf.RoundToInt(InventoryItemUpgradesScale.Price * scale);
            inventoryItemUpgrades.InventoryItemStatList = new List<InventoryItemStat>(InventoryItemUpgradesScale.InventoryItemStatList);

            for (int i = 0; i < inventoryItemUpgrades.InventoryItemStatList.Count; i++)
            {
                InventoryItemStat defaultInventoryItemStat = InventoryItemUpgradesScale.InventoryItemStatList[i];

                if (currentCardUpdateLevel > 0)
                    defaultInventoryItemStat = GetInventoryItemUpgrades(currentCardUpdateLevel - 1).InventoryItemStatList[i];

                InventoryItemStat newInventoryItemStat = new InventoryItemStat();
                newInventoryItemStat.StatType = defaultInventoryItemStat.StatType;
                newInventoryItemStat.Value = defaultInventoryItemStat.Value;

                newInventoryItemStat.Value *= scale;

                inventoryItemUpgrades.InventoryItemStatList[i] = newInventoryItemStat;
            }

            return inventoryItemUpgrades;
        }
        
        public bool HaveCardsToUpgrade(int card, int level)
        {
            if (card >= GetInventoryItemUpgrades(level).CardToUpgrade)
                return true;

            return false;
        }
    }

    [Serializable]
    public class InventoryItemUpgrades
    {
        public int CardToUpgrade;
        public int Price;
        [Space]
        
        public List<InventoryItemStat> InventoryItemStatList;
    }
    
    [Serializable]
    public class InventoryItemStat
    {
        public StatType StatType;
        public float Value;
    }
}