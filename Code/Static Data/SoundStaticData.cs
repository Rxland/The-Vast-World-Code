using System.Collections.Generic;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Sound Static Data", menuName = "Static Data/Sound Static Data")]
    public class SoundStaticData : ScriptableObject
    {
        public List<AudioSource> HitsSounds;
        public List<AudioSource> EnemyKilledSounds;

        
        
        
        public AudioSource GetRandomHitSound()
        {
            return HitsSounds[Random.Range(0, HitsSounds.Count)];
        }
        
        public AudioSource GetRandomEnemyKilledSound()
        {
            return EnemyKilledSounds[Random.Range(0, EnemyKilledSounds.Count)];
        }
    }
}