using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Static_Data.Inventory
{
    [CreateAssetMenu(fileName = "Weapon Inventory Item Static Data", menuName = "Static Data/Inventory/Weapon Inventory Item Static Data")]
    public class WeaponInventoryItemStaticData : InventoryItemStaticData
    {
        public WeaponName WeaponName;
    }
}