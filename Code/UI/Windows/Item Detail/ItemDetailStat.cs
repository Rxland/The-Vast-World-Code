using _GAME.Code.Types;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Code.UI.Windows.Item_Detail
{
    public class ItemDetailStat : MonoBehaviour
    {
        [ReadOnly] public StatType StatType;
        [Space]
        
        public Image IconBgImg;
        public Image IconImg;
        [Space]
        
        public TextMeshProUGUI NameText;
        public TextMeshProUGUI ValuesText;
        [Space] 
        
        public Color UpgradeValueColor;
    }
}