using System.Collections.Generic;
using _GAME.Code.Logic.Weapon;
using _GAME.Code.Save_Data;
using _GAME.Code.Static_Data.Inventory;
using Zenject;

namespace _GAME.Code.Features
{
    public class PlayerFeature
    {
        [Inject] private SaveFeature _saveFeature;
        [Inject] private InventoryFeature _inventoryFeature;
        
        public List<Weapon> GetEquippedWeapons(List<Weapon> allWeapon)
        {
            List<Weapon> equipedWeapons = new();
            List<Weapon> allWeaponCopy = new List<Weapon>(allWeapon);
            
            
            foreach (InventoryItemSaveData inventoryItemSaveData in _saveFeature.SaveData.InventorySaveData.SelectedItems)
            {
                InventoryItemStaticData inventoryItemStaticData = _inventoryFeature.GetInventoryItemStaticData(inventoryItemSaveData);
                WeaponInventoryItemStaticData weaponInventoryItemStaticData = inventoryItemStaticData as WeaponInventoryItemStaticData;
                
                if (weaponInventoryItemStaticData == null) continue;
                
                Weapon weapon = allWeaponCopy.Find(x => x.WeaponName == weaponInventoryItemStaticData.WeaponName);
   
                weapon.Init();
                
                equipedWeapons.Add(weapon);
                allWeaponCopy.Remove(weapon);          
            }
            return equipedWeapons;
        }
    }
}