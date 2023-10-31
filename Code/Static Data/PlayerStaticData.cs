using _GAME.Code.Logic.Player;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Player Static Data", menuName = "Static Data/Player Static Data")]
    public class PlayerStaticData : ScriptableObject
    {
        public PlayerRef PlayerRefPrefab;
    }
}