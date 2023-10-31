using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _GAME.Code.Save_Data
{
    [Serializable]
    public class SaveData
    {
        [Header("Level")] 
        public int CurrentLevel;
        public int NGplusLevel;
        public int LevelsDoneAmount;
        public bool FirstLevelDone;

        [Header("Player")] 
        public PlayerSaveData PlayerSaveData;

        [Header("Inventory")] 
        public InventorySaveData InventorySaveData;

        [Header("Other")]
        [ReadOnly] public List<string> KilledEnemiesIds;

        [Header("Settings")] 
        public SettingsSaveData SettingsSaveData;

        [Header("Tutor")]
        public TutorialsSaveData TutorialsSaveData;
    }
}