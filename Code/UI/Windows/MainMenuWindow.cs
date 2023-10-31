using _GAME.Code.Factories;
using _GAME.Code.Logic.Game_State_Machine;
using _GAME.Code.Types;
using _GAME.Code.UI.Buttons;
using TMPro;
using UnityEngine;
using Zenject;

namespace _GAME.Code.UI.Windows
{
    public class MainMenuWindow : WindowBase
    {
        public TextMeshProUGUI GameNameText;
        public SimpleButton StartGameButton;
        public SimpleButton OpenSettingsButton;
        [Space] 
        
        public GameObject LoadingCircle;
        

        [Inject] private GameStateMachine _gameStateMachine;
        [Inject] private WindowFactory _windowFactory;
        
        public override void OpenWindow()
        {
            StartGameButton.Button.onClick.AddListener(() =>
            {
                _gameStateMachine.ChangeState(GameStateType.LoadLevel);
            });
            OpenSettingsButton.Button.onClick.AddListener(() =>
            {
                _windowFactory.SpawnWindow(WindowName.Settings);
            });
        }

        public void TurnOnViewElements()
        {
            LoadingCircle.gameObject.SetActive(false);
            StartGameButton.gameObject.SetActive(true);
            OpenSettingsButton.gameObject.SetActive(true);
            GameNameText.gameObject.SetActive(true);
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
        }
    }
}