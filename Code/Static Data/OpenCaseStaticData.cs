using System.Collections.Generic;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.UI.Windows;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Open Case Static Data", menuName = "Static Data/Open Case Static Data")]
    public class OpenCaseStaticData : ScriptableObject
    {
        public CaseDroppedItem CaseDroppedItemPrefab;
        [Space]
        
        public List<InventoryItemStaticData> FirstTimeDroppingCaseItems;
        public List<int> FirstTimeDroppingCaseItemsAmount;
        [Space]
        
        public List<InventoryItemStaticData> AllCanDropCaseItems;
        [Space] 
        
        public int MinDropItemsAmount;
        public int MaxDropItemsAmount;
        [Space]
        
        public int MinCardsAmountInDrop;
        public int MaxCardsAmountInDrop;
    }
}