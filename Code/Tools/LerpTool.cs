using System;
using System.Collections;
using UnityEngine;

namespace _GAME.Code.Tools
{
    public static class LerpTool
    {
        public static IEnumerator DoJump(Transform startPoint, Transform endPoint, float lerpDuration, Action endEvent)
        {
            Vector3 startPos = startPoint.position;

            float elapsedTime = 0f;

            while (elapsedTime < lerpDuration)
            {
                startPoint.position = Vector3.Lerp(startPos, endPoint.position, elapsedTime / lerpDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            endEvent?.Invoke();
        }
    }
}