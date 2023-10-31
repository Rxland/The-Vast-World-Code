using System;
using System.Collections;
using _GAME.Code.Features;
using _GAME.Code.Logic.Level;
using _GAME.Code.Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace _GAME.Code.Factories
{
    public class EnvFactory
    {
        public LevelRef Level => _level;
        public Action LevelLoadedAction;

        private LevelRef _level;

        [Inject] private ZenjectSceneLoader _zenjectSceneLoader;
        [Inject] private WindowFactory _windowFactory;
        [Inject] private SaveReactivePropertiesFeature _saveReactivePropertiesFeature;

        public IEnumerator SpawnLevel(int levelId)
        {
            string levelName = $"Level_{levelId}";

            if (!GameExtensions.IsSceneExists(levelName))
            {
                _saveReactivePropertiesFeature.CurrentLevel.Value = 2;
                _saveReactivePropertiesFeature.NGplusLevel.Value++;
                levelName = $"Level_{_saveReactivePropertiesFeature.CurrentLevel.Value}";
            }
            
            AsyncOperation asyncLoad = _zenjectSceneLoader.LoadSceneAsync(levelName, LoadSceneMode.Additive,container => { }, LoadSceneRelationship.Child);
            
            while (!asyncLoad.isDone)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            
                _windowFactory.LoadingWindow.LoadingSlider.value = progress;
            
                yield return null;
            }
            _level = GameObject.FindObjectOfType<LevelRef>();

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
            
            LevelLoadedAction?.Invoke();
        }
        
        public IEnumerator UnLoadLLevel(int levelIdToUnload, Action callAfterUnload)
        {
            if (!_level) yield break;
            
            AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync($"Level_{levelIdToUnload}");
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            callAfterUnload?.Invoke();
        }
    }
}