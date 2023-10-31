using _GAME.Code.Features;
using _GAME.Code.UI;
using _GAME.Code.UI.Windows;
using _GAME.Code.UI.Windows.Inventory;
using _GAME.Code.UI.Windows.Item_Detail;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Factories
{
    public class UiFactory
    {
        [Inject] private StaticDataFeature _staticDataFeature;
        [Inject] private DiContainer _diContainer;
        
        public void SpawnDamageText(int damage, Transform parent)
        {
            DamageText damageText = GameObject.Instantiate(_staticDataFeature.UiStaticData.DamageTextPrefab, parent);
            damageText.Text.text = $"{damage}";
            
            Vector3 moveToPos = new Vector3(Random.Range(-damageText.MaxXMovePos, damageText.MaxXMovePos),
                Random.Range(-damageText.MaxYMovePos, damageText.MaxYMovePos), 0f);

            damageText.transform.localScale = Vector3.zero;

            DOTween.Sequence()
                .Append(damageText.transform.DOScale(Vector3.one, 0.5f)).SetEase(Ease.OutBack)
                .Append(damageText.transform.DOLocalMove(moveToPos, 1f))
                .Append(damageText.Text.DOFade(0, 0.5f));
            
            GameObject.Destroy(damageText.gameObject, 3f);
        }

        public InventoryCardItem SpawnInventoryCardItem(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<InventoryCardItem>(_staticDataFeature.InventoryStaticData.InventoryCardItemPrefab, parent);
        }

        public ItemDetailStat SpawnItemDetailStat(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<ItemDetailStat>(_staticDataFeature.ItemDetailMainStaticData.ItemDetailStatPrefab, parent);
        }

        public CaseDroppedItem SpawnCaseDroppedItem(Transform parent)
        {
            return _diContainer.InstantiatePrefabForComponent<CaseDroppedItem>(_staticDataFeature.OpenCaseStaticData.CaseDroppedItemPrefab, parent);
        }
    }
}