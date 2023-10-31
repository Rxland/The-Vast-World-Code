using System;
using _GAME.Code.Factories;
using _GAME.Code.Save_Data;
using Exoa.TutorialEngine;
using Zenject;

namespace _GAME.Code.Features
{
    public class TutorialFeature
    {
        [Inject] private SaveFeature _saveFeature;
        [Inject] private WindowFactory _windowFactory;

        public TutorialsSaveData GetTutorialsSaveData()
        {
            return _saveFeature.SaveData.TutorialsSaveData;
        }

        public void Save()
        {
            _saveFeature.SaveGame();
        }
        
        public void LoadTutor(string tutorialName, Action onTutorCompetedEvent)
        {
            _windowFactory.TutorialController.gameObject.SetActive(true);
            
            TutorialLoader.instance.Load(tutorialName);
            TutorialEvents.OnTutorialComplete += () => onTutorCompetedEvent?.Invoke();
        }

        public void HideTutor()
        {
            TutorialLoader.instance.HideTutor();
            _windowFactory.TutorialController.gameObject.SetActive(false);
        }
    }
}