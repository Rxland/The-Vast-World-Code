using UnityEngine;

namespace _GAME.Code.Factories
{
    public class SoundFactory
    {
        public void SpawnSound(AudioSource audioSource, Vector3 spawnPos, float destroyDelay = 2f)
        {
            AudioSource newSound = GameObject.Instantiate(audioSource);
            newSound.transform.position = spawnPos;
            
            GameObject.Destroy(newSound.gameObject, destroyDelay);
        }

        public void PlayAudioClickAtPoint(AudioClip audioClip, Vector3 playPos)
        {
            AudioSource.PlayClipAtPoint(audioClip, playPos, 1f);
        }
    }
}