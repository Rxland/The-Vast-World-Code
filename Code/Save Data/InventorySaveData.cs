using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GAME.Code.Save_Data
{
    [Serializable]
    public class InventorySaveData
    {
        public bool IsFirstTimeCaseOpend;
        [Space]
        
        public List<InventoryItemSaveData> ItemsOnStash;
        public List<InventoryItemSaveData> SelectedItems;
    }
}