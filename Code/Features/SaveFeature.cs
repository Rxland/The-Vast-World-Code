using _GAME.Code.Save_Data;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class SaveFeature : MonoBehaviour
    {
        public SaveData SaveData = new();
        [Space]
        
        public SaveData DefaultSaveData;
        
        [Inject] private SDKFeature _SDKFeature;
        
        
        [Button]
        public void SaveGame()
        {
            ES3.Save("Save Game", SaveData);

            _SDKFeature.SaveProgress();
        }

        public void LoadGame()
        {
            // SaveData = ES3.Load<SaveData>("Save Game", SaveData);
            _SDKFeature.GetSaves();
        }
    }
}