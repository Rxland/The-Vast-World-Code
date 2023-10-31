using _GAME.Code.Features;
using _GAME.Code.Static_Data;
using _GAME.Code.Types;
using _GAME.Code.UI.Windows;
using _GAME.Code.UI.Windows.Inventory;
using Exoa.TutorialEngine;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using DeviceType = _GAME.Code.Types.DeviceType;

namespace _GAME.Code.Factories
{
    public class WindowFactory : MonoBehaviour
    {
        public Transform WindowsParent;
        public WindowBase CurrentWindow;
        [Space] 
        
        public MainMenuWindow MainMenuWindow;
        public TutorialController TutorialController;
        [ReadOnly] public LoadingWindow LoadingWindow;
        [ReadOnly] public HudWindow HudWindow;
        [ReadOnly] public SettingsWindow SettingsWindow;
        [ReadOnly] public ItemDetailWindow ItemDetailWindow;
        [ReadOnly] public InventoryWindow InventoryWindow;
        [ReadOnly] public ReviveWindow ReviveWindow;
        [ReadOnly] public LossWindow LossWindow;
        [ReadOnly] public MobileControlsWindow MobileControlsWindow;
        [ReadOnly] public OpenCaseWindow OpenCaseWindow;
        

        [Inject] private DiContainer _diContainer;
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private DeviceFeature _deviceFeature;


        [Button]
        public WindowBase SpawnWindow(WindowName windowName)
        {
            WindowBase newWindow = null;
            
            foreach (WindowSpawnData windowSpawnData in _staticDataFeature.WindowsStaticData.AllWindowsToSpawn)
            {
                if (windowSpawnData.WindowName == windowName)
                {
                    newWindow = _diContainer.InstantiatePrefabForComponent<WindowBase>(windowSpawnData.WindowBase, WindowsParent);
                    
                    CurrentWindow = newWindow;
                    CurrentWindow.OpenWindow();
                    
                    switch (windowName)
                    {
                        case WindowName.MainMenu:
                            MainMenuWindow = (MainMenuWindow)newWindow;
                            break;
                        case WindowName.Loading:
                            LoadingWindow = (LoadingWindow)newWindow;
                            break;
                        case WindowName.Hud:
                            HudWindow = (HudWindow)newWindow;
                            break;
                        case WindowName.Inventory:
                            InventoryWindow = (InventoryWindow)newWindow;
                            break;
                        case WindowName.Settings:
                            SettingsWindow = (SettingsWindow)newWindow;
                            break;
                        case WindowName.ItemDetail:
                            ItemDetailWindow = (ItemDetailWindow)newWindow;
                            break;
                        case WindowName.Revive:
                            ReviveWindow = (ReviveWindow)newWindow;
                            break;
                        case WindowName.Loss:
                            LossWindow = (LossWindow)newWindow;
                            break;
                        case WindowName.MobileInputsWindow:
                            MobileControlsWindow = (MobileControlsWindow)newWindow;
                            break;
                        case WindowName.OpenCaseWindow:
                            OpenCaseWindow = (OpenCaseWindow)newWindow;
                            break;
                    }
                }
            }
            return newWindow;
        }
        
        public void TrySpawnMobileControlsWindow()
        {
            if (_deviceFeature.CurrentDeviceType != DeviceType.Mobile) return;
            
            SpawnWindow(WindowName.MobileInputsWindow);
        }
    }
}