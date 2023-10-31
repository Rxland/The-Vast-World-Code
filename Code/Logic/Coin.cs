using System;
using _GAME.Code.Factories;
using _GAME.Code.Factories.Pools;
using _GAME.Code.Features;
using _GAME.Code.Logic.Extentions;
using _GAME.Code.Tools;
using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic
{
    public class Coin : MonoBehaviour
    {
        public int MoneyToAddAmount;
        [Space]
        
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _rbSpawnForce;
        [SerializeField] private float _jumpToPlayerTweenPower;
        [Space]
        
        [SerializeField] private float _xMaxRotationAngel;
        [SerializeField] private float _xMinRotationAngel;
        [Space]
        
        [SerializeField] private float _yMaxRotationAngel;
        [SerializeField] private float _yMinRotationAngel;
        [Space]
        
        [SerializeField] private float _canTakeDelay;
        [Space]
        
        [SerializeField] private GravityMultiplier _gravityMultiplier;
        
        private bool _canBeTaken;
        private Vector3 _startPos;
        
        [Inject] private MoneyFeature _moneyFeature;
        [Inject] private GameFactory _gameFactory;
        [Inject] private CoinsPool _coinsPool;

        public void Init()
        {
            _startPos = transform.position;
            
            float xRotation = UnityEngine.Random.Range(_xMinRotationAngel, _xMaxRotationAngel);
            float yRotation = UnityEngine.Random.Range(_yMinRotationAngel, _yMaxRotationAngel);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

            _rb.isKinematic = false;
            
            _rb.AddForce(transform.forward * _rbSpawnForce);

            _gravityMultiplier.IsOn = true;

            Invoke(nameof(MoveToPlayer), _canTakeDelay);
        }

        private void MoveToPlayer()
        {
            _rb.isKinematic = true;
            _gravityMultiplier.IsOn = false;

            CoinGot();

            // StartCoroutine(LerpTool.DoJump(transform, _gameFactory.Player.CoinsFlyToPonit, 0.5f, CoinGot));
        }
    
        private void CoinGot()
        {
            _moneyFeature.IncreaseMoney(MoneyToAddAmount);
            _coinsPool.Despawn(this);
        }
        
        #region For Debug

        private void ResetCoin()
        {
            transform.position = _startPos;
            _rb.velocity = Vector3.zero;
            _gravityMultiplier.IsOn = false;
        }

        #endregion
    }
}