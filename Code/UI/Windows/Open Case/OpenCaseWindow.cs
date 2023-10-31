using System.Collections.Generic;
using System.Linq;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Save_Data;
using _GAME.Code.Static_Data;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.Types;
using _GAME.Code.UI.Buttons;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class OpenCaseWindow : WindowBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private Button _nextItemButton;
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private SimpleButton _openOneMoreButton;

        private List<InventoryItemStaticData> _droppedItems = new();
        private List<int> _droppedCardsAmount = new();

        private List<CaseDroppedItem> _spawnedCaseDroppedItems = new();

        private InventorySaveData _inventorySaveData;
        private OpenCaseStaticData _openCaseStaticData;
        
        System.Random random = new System.Random();

        private int _currentCardId;
        private CaseDroppedItem _prevCaseDroppedItem;
        private CaseDroppedItem _currentCaseDroppedItem;
        
        [Inject] private InventoryFeature _inventoryFeature;
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private UiFactory _uiFactory;
        [Inject] private LevelFeature _levelFeature;
        [Inject] private SaveFeature _saveFeature;
        [Inject] private WindowFactory _windowFactory;
        [Inject] private GameFactory _gameFactory;
        [Inject] private SDKFeature _sdkFeature;


        [Button]
        public override void OpenWindow()
        {
            TryHideWindows();
            
            base.OpenWindow();
            
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, 0.3f);
            
            _inventorySaveData = _inventoryFeature.GetInventorySaveData();
            _openCaseStaticData = _staticDataFeature.OpenCaseStaticData;

            _openOneMoreButton.transform.localScale = Vector3.zero;
            _openOneMoreButton.Button.onClick.AddListener(OpenOneMoreButtonClick);
            
            BackButton.transform.localScale = Vector3.zero;
            BackButton.Button.onClick.AddListener(OnCloseWindowButtonClick);
            
            LockPlayerInputs();
            DropLoot();
            SpawnDroppingItems();
            SaveDroppedLootTo();
            UpdateDroppingItemsAmount();
            DOVirtual.DelayedCall(0.3f, StartAnimations);
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
            CursorFeature.SetCursorLockedMode(false);
        }

        private void TryHideWindows()
        {
            if (_windowFactory.HudWindow)
                _windowFactory.HudWindow.CloseWindow();
            
            if (_windowFactory.MobileControlsWindow)
                _windowFactory.MobileControlsWindow.CloseWindow();
        }
        
        private void LockPlayerInputs()
        {
            _gameFactory.Player.PlayerAttackController.enabled = false;
            _gameFactory.Player.ThirdPersonController.enabled = false;
        }
        
        private void StartAnimations()
        {
            _nextItemButton.onClick.AddListener(OnNextItemClick);
            OnNextItemClick();
        }

        private void OnNextItemClick()
        {
            _prevCaseDroppedItem = _currentCaseDroppedItem ? _currentCaseDroppedItem : null;
            
            if (_currentCardId < _spawnedCaseDroppedItems.Count)
            {
                _currentCaseDroppedItem = _spawnedCaseDroppedItems[_currentCardId];
                _currentCaseDroppedItem.ContentContainer.position = _centerPoint.position;
                _currentCaseDroppedItem.ContentContainer.transform.DOScale(Vector3.one * 1.3f, 0.3f).SetEase(Ease.OutBack).SetDelay(0.3f);
            }
            
            if (_prevCaseDroppedItem)
            {
                _prevCaseDroppedItem.ContentContainer.transform.DOLocalMove(Vector3.zero, 0.5f).SetEase(Ease.InOutBack);
                _prevCaseDroppedItem.ContentContainer.transform.DOScale(Vector3.one * 0.7f, 0.5f).SetEase(Ease.InOutBack);
            }
            
            if (_currentCardId > _spawnedCaseDroppedItems.Count - 1)
            {
                _nextItemButton.gameObject.SetActive(false);

                DOTween.Sequence()
                    .Append(_openOneMoreButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack))
                    .Append(BackButton.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).SetDelay(2f));
            }
            
            _currentCardId++;
        }
        
        private void DropLoot()
        {
            if (!_inventorySaveData.IsFirstTimeCaseOpend)
            {
                _droppedItems = new(_openCaseStaticData.FirstTimeDroppingCaseItems);
                
                _droppedCardsAmount.AddRange(_openCaseStaticData.FirstTimeDroppingCaseItemsAmount);
                
                _inventorySaveData.IsFirstTimeCaseOpend = true;
                _inventoryFeature.Save();
                
                return;
            }

            int numberOfRandomElements = Random.Range(_openCaseStaticData.MinDropItemsAmount, _openCaseStaticData.MaxDropItemsAmount);
            _droppedItems = _openCaseStaticData.AllCanDropCaseItems.OrderBy(x => random.Next()).Take(numberOfRandomElements).ToList();

            for (int i = 0; i < _droppedItems.Count; i++)
            {
                int cardAmount = Random.Range(_openCaseStaticData.MinCardsAmountInDrop, _openCaseStaticData.MaxCardsAmountInDrop);
                _droppedCardsAmount.Add(cardAmount);
            }
        }

        private void SaveDroppedLootTo()
        {
            for (int i = 0; i < _droppedItems.Count; i++)
            {
                InventoryItemStaticData inventoryItemStaticData = _droppedItems[i];
                
                InventoryItemSaveData inventoryItemSaveData = _inventoryFeature.GetInventoryItemSaveData(inventoryItemStaticData);

                if (inventoryItemSaveData == null)
                {
                    InventoryItemSaveData newInventoryItemSaveData = new InventoryItemSaveData();
                    newInventoryItemSaveData.ItemLevel = inventoryItemStaticData.LevelId;
                    newInventoryItemSaveData.InventoryItemType = inventoryItemStaticData.InventoryItemType;
                    newInventoryItemSaveData.CurrentUpgradeCardsAmount += _droppedCardsAmount[i];
                    
                    _inventorySaveData.ItemsOnStash.Add(newInventoryItemSaveData);
                }
                else
                {
                    inventoryItemSaveData.CurrentUpgradeCardsAmount += _droppedCardsAmount[i];
                }
            }
            _inventoryFeature.Save();
        }

        private void SpawnDroppingItems()
        {
            foreach (InventoryItemStaticData inventoryItemStaticData in _droppedItems)
            {
                CaseDroppedItem newCaseDroppedItem = _uiFactory.SpawnCaseDroppedItem(_itemsContainer);
                
                newCaseDroppedItem.ItemImg.sprite = inventoryItemStaticData.ItemSprite;
                
                newCaseDroppedItem.ContentContainer.transform.localScale = Vector3.zero;

                _spawnedCaseDroppedItems.Add(newCaseDroppedItem);
            }
        }

        private void UpdateDroppingItemsAmount()
        {
            for (int i = 0; i < _spawnedCaseDroppedItems.Count; i++)
            {
                CaseDroppedItem spawnedCaseDroppedItem = _spawnedCaseDroppedItems[i];
                
                spawnedCaseDroppedItem.CardAmountText.text = _droppedCardsAmount[i].ToString();
            }
        }

        private void OnCloseWindowButtonClick()
        {
            _levelFeature.LoadNextLevel();
        }

        private void OpenOneMoreButtonClick()
        {
            _sdkFeature.ShowRewardVideoOpenOneMoreCase();
        }

        public void GetOpenOneMore()
        {
            WindowFactory.SpawnWindow(WindowName.OpenCaseWindow);
            CloseWindow();
        }
    }
}