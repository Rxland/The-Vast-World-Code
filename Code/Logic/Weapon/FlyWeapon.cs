using UnityEngine;

namespace _GAME.Code.Logic.Weapon
{
    public class FlyWeapon : MonoBehaviour
    {
        [SerializeField] private Transform _flyTarget;
        [Space] 
        
        [SerializeField] private float _flyPosLerpSpeed;
        [SerializeField] private float _flyRotationLerpSpeed;
        
        private void Start()
        {
            transform.SetParent(null);
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _flyTarget.transform.position, Time.deltaTime * _flyPosLerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, _flyTarget.transform.rotation, Time.deltaTime * _flyRotationLerpSpeed);
        }

        public void SetActiveWeapon(bool activeMode)
        {
            gameObject.SetActive(activeMode);
        }
    }
}