using _GAME.Code.UI;
using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Ui Static Data", menuName = "Static Data/Ui Static Data")]
    public class UiStaticData : ScriptableObject
    {
        public DamageText DamageTextPrefab;
    }
}