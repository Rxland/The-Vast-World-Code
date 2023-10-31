using _GAME.Code.Logic;
using _GAME.Code.Logic.Extentions;
using UnityEditor;
using UnityEngine;

namespace _GAME.Code.Editor
{
    [CustomEditor(typeof(Mark))]
    public class MarkEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderGizmo(Mark mark, GizmoType gizmoType)
        {
            Gizmos.color = mark.Color;
            Gizmos.DrawSphere(mark.transform.position, mark.Radius);
        }  
    }
}