using UnityEngine;

namespace _GAME.Code.Static_Data
{
    [CreateAssetMenu(fileName = "Layer Mask Static Data", menuName = "Static Data/Layer Mask Static Data")]
    public class LayerMaskStaticData : ScriptableObject
    {
        public LayerMask PlayerLayerMask;
    }
}