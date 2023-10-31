using _GAME.Code.Factories;
using _GAME.Code.Features;
using _GAME.Code.Logic.Interaction.Trigger;
using _GAME.Code.Types;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Logic
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private OpenChestInteraction _openChestInteraction;
        [SerializeField] private ParticleSystem _canOpenEffect;

        [Inject] private WindowFactory _windowFactory;
        [Inject] private LevelFeature _levelFeature;

        private void Start()
        {
            _openChestInteraction.gameObject.SetActive(false);
        }

        public void EnableInteraction()
        {
            _openChestInteraction.gameObject.SetActive(true);
            _canOpenEffect.Play();
        }
        
        [Button]
        public void OpenChest()
        {
            _levelFeature.SetNextLevelSaves();
            
            _canOpenEffect.Stop();
            
            _animator.SetBool("Open", true);

            DOVirtual.DelayedCall(0.5f, () => _windowFactory.SpawnWindow(WindowName.OpenCaseWindow));
        }
    }
}