using _GAME.Code.Features;
using _GAME.Code.UI.Windows.Inventory;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class UpgradeCardAnimWindow : WindowBase
    {
        [SerializeField] private InventoryCardItem _inventoryCardItem;

        [Header("Anim Set Up")] 
        [SerializeField] private float _animDuration;
        [SerializeField] private Ease _animEase;
        [SerializeField] private float _delay;
        
        [Header("Effects")] 
        [SerializeField] private ParticleSystem _effect;
        
        private Sequence _sequence;

        [Inject] private InventoryFeature _inventoryFeature;
        
        public override void OpenWindow()
        {
            base.OpenWindow();
            
            _inventoryCardItem.transform.localScale = Vector3.zero;
            BackButton.transform.localScale = Vector3.zero;

            _inventoryCardItem.InventoryItemSaveData = _inventoryFeature.SelectedInventoryItemSaveData.PreviousInventoryItemSaveData;
            _inventoryCardItem.Init();
            
            _sequence = DOTween.Sequence();
            _sequence.Append(_inventoryCardItem.transform.DOScale(Vector3.one, _animDuration).SetEase(_animEase));
            _sequence.AppendCallback(() => _effect.Play());
            
            _sequence.AppendCallback(() => _inventoryCardItem.StartUpgradeAnimations(_inventoryFeature.SelectedInventoryItemSaveData));

            _sequence.Append(BackButton.transform.DOScale(Vector3.one, _animDuration).SetEase(_animEase).SetDelay(_delay));

            WindowFactory.ItemDetailWindow.ReOpenWindow();
            WindowFactory.InventoryWindow.ReOpenWindow();
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
            CursorFeature.SetCursorLockedMode(false);
        }
    }
}