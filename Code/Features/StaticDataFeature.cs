using System.Collections.Generic;
using System.Linq;
using _GAME.Code.Save_Data;
using _GAME.Code.Static_Data;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.Types;
using UnityEngine;

namespace _GAME.Code.Features
{
    public class StaticDataFeature
    {
        private const string _enemyStaticDataPath = "Static Data/Enemy";
        private const string _playerStaticDataPath = "Static Data/Player Static Data";
        private const string _windowsStaticDataPath = "Static Data/Windows Static Data";
        private const string _levelsStaticDataPath = "Static Data/Levels Static Data";
        private const string _uiStaticDataPath = "Static Data/Ui Static Data";
        private const string _layerMaskStaticDataPath = "Static Data/Layer Mask Static Data";
        private const string _inventoryStaticDataPath = "Static Data/Inventory/Inventory Static Data";
        private const string _itemDetailMainStaticDataPath = "Static Data/Item Detail/Item Detail Main Static Data";
        private const string _openCaseStaticDataPath = "Static Data/Open Case Static Data";
        private const string _gameObjectsStaticDataPath = "Static Data/Game Objects Static Data";
        private const string _NGplusStaticDataPath = "Static Data/NG plus Static Data";
        private const string _inventoryItemStaticDataPath = "Static Data/Inventory/Items";
        private const string _soundStaticDataPath = "Static Data/Sound Static Data";
        
        
        private List<EnemyStaticData> _enemyStaticDataList;
        private List<InventoryItemStaticData> _inventoryItemStaticDataList;
        private PlayerStaticData _playerStaticData;
        private WindowsStaticData _windowsStaticData;
        private LevelsStaticData _levelsStaticData;
        private UiStaticData _uiStaticData;
        private LayerMaskStaticData _layerMaskStaticData;
        private InventoryStaticData _inventoryStaticData;
        private ItemDetailMainStaticData _itemDetailMainStaticData;
        private OpenCaseStaticData _openCaseStaticData;
        private GameObjectsStaticData _gameObjectsStaticData;
        private NGplusStaticData _NGplusStaticData;
        private SoundStaticData _soundStaticData;
        
        
        public PlayerStaticData PlayerStaticData => _playerStaticData;
        public WindowsStaticData WindowsStaticData => _windowsStaticData;
        public LevelsStaticData LevelsStaticData => _levelsStaticData;
        public UiStaticData UiStaticData => _uiStaticData;
        public LayerMaskStaticData LayerMaskStaticData => _layerMaskStaticData;
        public InventoryStaticData InventoryStaticData => _inventoryStaticData;
        public ItemDetailMainStaticData ItemDetailMainStaticData => _itemDetailMainStaticData;
        public OpenCaseStaticData OpenCaseStaticData => _openCaseStaticData;
        public GameObjectsStaticData GameObjectsStaticData => _gameObjectsStaticData;
        public NGplusStaticData NGplusStaticData => _NGplusStaticData;
        public List<InventoryItemStaticData> InventoryItemStaticDataList => _inventoryItemStaticDataList;
        public SoundStaticData SoundStaticData => _soundStaticData;
        
        public void Init()
        {
            Load();
        }

        private void Load()
        {
            _enemyStaticDataList = Resources.LoadAll<EnemyStaticData>(_enemyStaticDataPath).ToList();
            _inventoryItemStaticDataList = Resources.LoadAll<InventoryItemStaticData>(_inventoryItemStaticDataPath).ToList();
            
            _playerStaticData = Resources.Load<PlayerStaticData>(_playerStaticDataPath);
            _windowsStaticData = Resources.Load<WindowsStaticData>(_windowsStaticDataPath);
            _levelsStaticData = Resources.Load<LevelsStaticData>(_levelsStaticDataPath);
            _uiStaticData = Resources.Load<UiStaticData>(_uiStaticDataPath);
            _layerMaskStaticData = Resources.Load<LayerMaskStaticData>(_layerMaskStaticDataPath);
            _inventoryStaticData = Resources.Load<InventoryStaticData>(_inventoryStaticDataPath);
            _itemDetailMainStaticData = Resources.Load<ItemDetailMainStaticData>(_itemDetailMainStaticDataPath);
            _openCaseStaticData = Resources.Load<OpenCaseStaticData>(_openCaseStaticDataPath);
            _gameObjectsStaticData = Resources.Load<GameObjectsStaticData>(_gameObjectsStaticDataPath);
            _NGplusStaticData = Resources.Load<NGplusStaticData>(_NGplusStaticDataPath);
            _soundStaticData = Resources.Load<SoundStaticData>(_soundStaticDataPath);
        }

        public EnemyStaticData GetEnemyByName(EnemyName enemyName)
        {
            foreach (EnemyStaticData enemyStaticData in _enemyStaticDataList)
            {
                if (enemyStaticData.EnemyName == enemyName)
                    return enemyStaticData;
            }
            return null;
        }
    }
}