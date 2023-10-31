using System.Collections.Generic;
using _GAME.Code.Logic.Character;
using _GAME.Code.Logic.Character.Stats;
using StarterAssets;
using TrailsFX;
using UnityEngine;

namespace _GAME.Code.Logic.Player
{
    public class PlayerRef : MonoBehaviour
    {
        public Animator Animator;
        public CharacterController CharacterController;
        public PlayerStats Stats;
        public CharacterVariables CharacterVariables;
        [Space]
        
        public CameraController CameraController;
        public PlayerInputController PlayerInputController;
        public AnimController AnimController;
        public PlayerAttackController PlayerAttackController;
        public DashCharacterController DashCharacterController;
        public ThirdPersonController ThirdPersonController;
        public List<TrailEffect> TrailEffects;
        [Space] 
        
        public Transform CameraRoot;
        public Transform CoinsFlyToPonit;
    }
}