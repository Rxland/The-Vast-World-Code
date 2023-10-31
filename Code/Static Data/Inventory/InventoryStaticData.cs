using _GAME.Code.UI.Windows.Inventory;
using UnityEngine;

namespace _GAME.Code.Static_Data.Inventory
{
    [CreateAssetMenu(fileName = "Inventory Static Data", menuName = "Static Data/Inventory/Inventory Static Data")]
    public class InventoryStaticData : ScriptableObject
    {
        public InventoryCardItem InventoryCardItemPrefab;
    }
}