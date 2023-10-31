using System;
using System.Collections.Generic;
using _GAME.Code.Types;
using _GAME.Code.UI.Windows;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Windows Static Data", menuName = "Static Data/Windows Static Data")]
    public class WindowsStaticData : ScriptableObject
    {
        public List<WindowSpawnData> AllWindowsToSpawn;
    }

    [Serializable]
    public struct WindowSpawnData
    {
        public WindowName WindowName;
        public WindowBase WindowBase;
    }
}