using System;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.Tools;
using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Save_Data
{
    [Serializable]
    public class InventoryItemSaveData : ICloneable
    {
        public int ItemLevel;
        public bool IsEquipped;
        public InventoryItemType InventoryItemType;
        [Space] 
        
        public int CurrentUpgradeLevel;
        public int CurrentUpgradeCardsAmount;
        
        [Header("Previus State")]
        public PreviousInventoryItemSaveData PreviousInventoryItemSaveData;
        
        
        public object Clone()
        {
            PreviousInventoryItemSaveData clone = new PreviousInventoryItemSaveData();
            GameExtensions.CopyValues(this, clone);

            return clone;
        }
    }

    [Serializable]
    public class PreviousInventoryItemSaveData : InventoryItemSaveData
    {
        
    }
}