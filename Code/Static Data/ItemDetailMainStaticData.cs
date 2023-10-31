using System;
using System.Collections.Generic;
using _GAME.Code.Types;
using _GAME.Code.UI.Windows.Item_Detail;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Item Detail Main Static Data", menuName = "Static Data/Item Detail/Item Detail Main Static Data")]
    public class ItemDetailMainStaticData : ScriptableObject
    {
        public ItemDetailStat ItemDetailStatPrefab;
        [Space] 
        
        public List<ItemDetailStatData> ItemDetailStatDataList;
        
        
        public ItemDetailStatData GetItemDetailStatData(StatType statType)
        {
            foreach (ItemDetailStatData itemDetailStatData in ItemDetailStatDataList)
            {
                if (itemDetailStatData.StatType == statType)
                    return itemDetailStatData;
            }
            return null;
        }
    }

    [Serializable]
    public class ItemDetailStatData
    {
        public StatType StatType;
        [Space]
        
        [PreviewField] public Sprite ItemBgSprite;
        [PreviewField] public Sprite ItemIconSprite;
    }
}