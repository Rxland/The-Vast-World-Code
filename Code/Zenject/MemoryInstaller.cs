using _GAME.Code.Factories.Pools;
using _GAME.Code.Logic;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Zenject
{
    public class MemoryInstaller : MonoInstaller<MemoryInstaller>
    {
        public Coin CoinPrefab;
        public int CoinsPoolAmount;
        [Space] 
        
        public Effect DeadPuffEffectPrefab;
        public int DeadPuffEffectAmount;
        [Space]
        
        public Effect BloodEffectPrefab;
        public int BloodEffectAmount;
        [Space]
        
        public Effect Sword_Slash_1_Pool_Prefab;
        public int Sword_Slash_1_Pool_Amount;
        [Space]
        
        public Effect Sword_Slash_3_Pool_Prefab;
        public int Sword_Slash_3_Pool_Amount;
        [Space]
        
        public Effect Sword_Slash_4_Pool_Prefab;
        public int Sword_Slash_4_Pool_Amount;
        [Space]
        
        public Effect Sword_Slash_8_Pool_Prefab;
        public int Sword_Slash_8_Pool_Amount;
        [Space]
        
        public Effect Prick_Pool_Prefab;
        public int Prick_Pool_Amount;
        

        public override void InstallBindings()
        {
            Container.BindMemoryPool<Coin, CoinsPool>().WithInitialSize(CoinsPoolAmount).FromComponentInNewPrefab(CoinPrefab).UnderTransformGroup("Coins");
            
            Container.BindMemoryPool<Effect, DeadPuffEffectPool>()
                .WithInitialSize(DeadPuffEffectAmount).FromComponentInNewPrefab(DeadPuffEffectPrefab).UnderTransformGroup("Dead Puffs Effects").NonLazy();
            
            Container.BindMemoryPool<Effect, BloodEffectPool>()
                .WithInitialSize(BloodEffectAmount).FromComponentInNewPrefab(BloodEffectPrefab).UnderTransformGroup("Blood Effects").NonLazy();
            
            Container.BindMemoryPool<Effect, Sword_Slash_1_Pool>()
                .WithInitialSize(Sword_Slash_1_Pool_Amount).FromComponentInNewPrefab(Sword_Slash_1_Pool_Prefab).UnderTransformGroup("Sword Slash 1 Pool").NonLazy();
            
            Container.BindMemoryPool<Effect, Sword_Slash_3_Pool>()
                .WithInitialSize(Sword_Slash_3_Pool_Amount).FromComponentInNewPrefab(Sword_Slash_3_Pool_Prefab).UnderTransformGroup("Sword Slash 3 Pool").NonLazy();
            
            Container.BindMemoryPool<Effect, Sword_Slash_4_Pool>()
                .WithInitialSize(Sword_Slash_4_Pool_Amount).FromComponentInNewPrefab(Sword_Slash_4_Pool_Prefab).UnderTransformGroup("Sword Slash 4 Pool").NonLazy();
            
            Container.BindMemoryPool<Effect, Sword_Slash_8_Pool>()
                .WithInitialSize(Sword_Slash_8_Pool_Amount).FromComponentInNewPrefab(Sword_Slash_8_Pool_Prefab).UnderTransformGroup("Sword Slash 8 Pool").NonLazy();
            
            Container.BindMemoryPool<Effect, Prick_1_Pool>()
                .WithInitialSize(Prick_Pool_Amount).FromComponentInNewPrefab(Prick_Pool_Prefab).UnderTransformGroup("Prick Pool").NonLazy();
        }
    }
}