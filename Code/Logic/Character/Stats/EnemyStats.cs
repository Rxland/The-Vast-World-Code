using System;
using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Enemy;
using _GAME.Code.Types;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic.Character.Stats
{
    public class EnemyStats : StatsBase
    {
        [SerializeField] private Canvas _worldCanvas;
        private EnemyRef _enemyRef;

        [Inject] private UiFactory _uiFactory;
        [Inject] private GameFactory _gameFactory;
        [Inject] private LevelFeature _levelFeature;

        public override void Init()
        {
            base.Init();
            _enemyRef = GetComponent<EnemyRef>();
        }

        protected override void Hit(int damage, Transform damageFromT)
        {
            base.Hit(damage, damageFromT);
            
            transform.DOPunchScale(Vector3.one * _hitPunchStrength, _hitPunchDuration);
            
            _uiFactory.SpawnDamageText(damage, _worldCanvas.transform);
            _gameFactory.SpawnCoins(transform.position, 5);
            SoundFactory.SpawnSound(StaticDataFeature.SoundStaticData.GetRandomHitSound(), transform.position);
        }

        public override void Kill()
        {
            base.Kill();
            
            Observable.Timer(TimeSpan.FromSeconds(KillDelay))
                .Subscribe(_ =>
                {
                    Collider.enabled = false;
                    
                    SpawnKilledEffect();
                    SoundFactory.SpawnSound(StaticDataFeature.SoundStaticData.GetRandomEnemyKilledSound(), transform.position);
                    
                    _levelFeature.level.SpawnedEnemiesList.Remove(_enemyRef);
                    Destroy(gameObject);
                    
                }).AddTo(this);
        }

        protected override bool TryStun(Transform damageFromT)
        {
            if (!base.TryStun(damageFromT)) return false;

            Vector3 moveToPos = (transform.position - damageFromT.position).normalized;
            Vector3 moveTo = transform.position + (moveToPos * _hitMoveForce);
            
            transform.DOLocalMove(moveTo, _hitMoveDuration).SetEase(_hitMoveEase);

            _enemyRef.BehaviorTree.SetVariableValue(BotBehaviorVariableName.IsStunned.ToString(), true);

            return true;
        }
    }
}