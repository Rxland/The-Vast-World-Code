using System.Collections.Generic;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Save_Data;
using _GAME.Code.Static_Data.Inventory;
using _GAME.Code.Types;
using _GAME.Code.UI.Buttons;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _GAME.Code.UI.Windows.Inventory
{
    public class InventoryCardItem : MonoBehaviour
    {
        public InventoryItemType InventoryItemType;
        [Space]
        
        public SimpleButton ItemButton;
        [Space]
        
        public Image CardImg;
        public Image NoOneCardSelectedImg;
        public Image CardXpSliderImgFilled;
        public List<Image> Stars;
        [Space]
        
        public TextMeshProUGUI LevelText;
        public TextMeshProUGUI CardXpText;
        [Space] 
        
        public Slider CardXpSlider;
        [Space] 
        
        public Color CardXpMaxColor;

        public Transform BottomPart;

        [ReadOnly] public InventoryItemSaveData InventoryItemSaveData;
        [ReadOnly] public InventoryItemStaticData InventoryItemStaticData;
        
        [Inject] private InventoryFeature _inventoryFeature;
        [Inject] private WindowFactory _windowFactory;
        
        public void Init()
        {
            InventoryItemStaticData = _inventoryFeature.GetInventoryItemStaticData(InventoryItemSaveData);
            
            int cardsToUpgrade = InventoryItemStaticData.GetInventoryItemUpgrades(InventoryItemSaveData.CurrentUpgradeLevel + 1).CardToUpgrade;
                
            CardXpText.text = $"{InventoryItemSaveData.CurrentUpgradeCardsAmount}/" +
                              $"{InventoryItemStaticData.GetInventoryItemUpgrades(InventoryItemSaveData.CurrentUpgradeLevel + 1).CardToUpgrade}";
            CardXpSlider.maxValue = cardsToUpgrade;
            CardXpSlider.value = InventoryItemSaveData.CurrentUpgradeCardsAmount;
            SetSliderColor();

            LevelText.text = $"{InventoryItemSaveData.CurrentUpgradeLevel + 1}";
            CardImg.sprite = InventoryItemStaticData.ItemSprite;
            TurnOnStars(InventoryItemSaveData.CurrentUpgradeLevel);
            
            ItemButton.Button.onClick.RemoveAllListeners();
            ItemButton.Button.onClick.AddListener(() => OnItemClick(InventoryItemSaveData));
        }

        public void StartUpgradeAnimations(InventoryItemSaveData newItemSaveData)
        {
            InventoryItemStaticData inventoryItemStaticData = _inventoryFeature.GetInventoryItemStaticData(newItemSaveData);
            int cardsToUpgrade = InventoryItemStaticData.GetInventoryItemUpgrades(newItemSaveData.CurrentUpgradeLevel).CardToUpgrade;
            Image starImg = Stars[newItemSaveData.CurrentUpgradeLevel];
            int filledCardsAmount = 1;
            

            CardXpText.text = $"{newItemSaveData.CurrentUpgradeCardsAmount}/" +
                              $"{inventoryItemStaticData.GetInventoryItemUpgrades(newItemSaveData.CurrentUpgradeLevel + 1).CardToUpgrade}";
            CardXpSlider.maxValue = cardsToUpgrade;
            CardXpSlider.value = 0;
            filledCardsAmount = newItemSaveData.CurrentUpgradeCardsAmount;
            SetSliderColor();

            LevelText.transform.localScale = Vector3.zero;
            LevelText.text = $"{newItemSaveData.CurrentUpgradeLevel + 1}";
            
            DOTween.Sequence()
                .Append(LevelText.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
                .Append(CardXpSlider.DOValue(0, 0.5f))
                .Append(CardXpSlider.DOValue(filledCardsAmount, 0.5f))
                .AppendCallback(() =>
                {
                    starImg.transform.localScale = Vector3.zero;
                    starImg.gameObject.SetActive(true);
                })
                .Append(starImg.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));
        }
        
        private void OnItemClick(InventoryItemSaveData inventoryItemSaveData)
        {
            _inventoryFeature.SelectedInventoryItemSaveData = inventoryItemSaveData;
            
            _windowFactory.SpawnWindow(WindowName.ItemDetail);
        }
        
        private void TurnOnStars(int level)
        {
            if (level >= Stars.Count)
                level = Stars.Count - 1;
            
            for (int i = 0; i < level + 1; i++)
            {
                Image starImg = Stars[i];
                starImg.gameObject.SetActive(true);
            }
        }

        private void SetSliderColor()
        {
            int cardsToUpgrade = InventoryItemStaticData.GetInventoryItemUpgrades(InventoryItemSaveData.CurrentUpgradeLevel + 1).CardToUpgrade;
            
            if (InventoryItemSaveData.CurrentUpgradeCardsAmount >= cardsToUpgrade)
            {
                CardXpSliderImgFilled.color = CardXpMaxColor;
            }
            else
            {
                CardXpSliderImgFilled.color = Color.white;
            }
        }
    }
}