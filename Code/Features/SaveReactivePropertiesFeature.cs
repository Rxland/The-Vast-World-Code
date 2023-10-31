using UniRx;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class SaveReactivePropertiesFeature : MonoBehaviour
    {
        public ReactiveProperty<int> Money = new();
        public ReactiveProperty<int> CurrentLevel = new();
        public ReactiveProperty<int> NGplusLevel = new();
        public ReactiveProperty<int> LevelsDoneAmount = new();
        public BoolReactiveProperty FirstLevelDone = new();

        [Inject] private SaveFeature _saveFeature;
        

        public void Init()
        {
            InitMoney();
            InitCurrentLevel();
            InitNGplusLevel();
            InitFirstLevelDone();
            InitLevelDoneAmount();
        }

        private void InitMoney()
        {
            Money.Value = _saveFeature.SaveData.PlayerSaveData.MoneyAmount;
            Money.Subscribe(_ =>
            {
                _saveFeature.SaveData.PlayerSaveData.MoneyAmount = Money.Value;
                
            }).AddTo(this);
        }
        
        private void InitCurrentLevel()
        {
            CurrentLevel.Value = _saveFeature.SaveData.CurrentLevel;
            CurrentLevel.Subscribe(_ =>
            {
                _saveFeature.SaveData.CurrentLevel = CurrentLevel.Value;
                _saveFeature.SaveGame();
                
            }).AddTo(this);
        }
        
        private void InitNGplusLevel()
        {
            NGplusLevel.Value = _saveFeature.SaveData.NGplusLevel;
            NGplusLevel.Subscribe(_ =>
            {
                _saveFeature.SaveData.NGplusLevel = NGplusLevel.Value;
                _saveFeature.SaveGame();
                
            }).AddTo(this);
        }
        
        private void InitFirstLevelDone()
        {
            FirstLevelDone.Value = _saveFeature.SaveData.FirstLevelDone;
            FirstLevelDone.Subscribe(_ =>
            {
                _saveFeature.SaveData.FirstLevelDone = FirstLevelDone.Value;
                _saveFeature.SaveGame();
                
            }).AddTo(this);
        }
        private void InitLevelDoneAmount()
        {
            LevelsDoneAmount.Value = _saveFeature.SaveData.LevelsDoneAmount;
            LevelsDoneAmount.Subscribe(_ =>
            {
                _saveFeature.SaveData.LevelsDoneAmount = LevelsDoneAmount.Value;
                _saveFeature.SaveGame();
                
            }).AddTo(this);
        }
    }
}