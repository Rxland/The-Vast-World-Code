using _GAME.Code.Logic;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Game Objects Static Data", menuName = "Static Data/Game Objects Static Data")]
    public class GameObjectsStaticData : ScriptableObject
    {
        public Coin CoinPrefab;
    }
}