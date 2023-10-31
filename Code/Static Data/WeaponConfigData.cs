using System;
using _GAME.Code.Tools;
using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Weapon Static Data", menuName = "Static Data/Weapon Static Data")]
    public class WeaponConfigData : ScriptableObject, ICloneable
    {
        public WeaponName WeaponName;
        public int AnimId;

        public object Clone()
        {
            WeaponConfigData config = CreateInstance<WeaponConfigData>();
            GameExtensions.CopyValues(this, config);

            return config;
        }
    }
}