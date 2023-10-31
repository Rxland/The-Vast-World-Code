using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _GAME.Code.Tools
{
    public static class GameExtensions
    {
        private static System.Random random = new System.Random();
        
        public static float CalculatePercentageOfTheNumber(int percentage, int number)
        {
            float cof = number / 100f;
            
            return cof * percentage;
        }

        public static bool CheckIfAgentReachedDestination(NavMeshAgent agent)
        {
            if (!agent.pathPending && !agent.isStopped)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void CopyValues<T>(T formBase, T copy)
        {
            Type type = formBase.GetType();

            foreach (FieldInfo field in type.GetFields())
            {
                field.SetValue(copy, field.GetValue(formBase));
            }
        }
        
        public static bool IsLayerInMask(LayerMask mask, int layer)
        {
            return (mask.value & (1 << layer)) != 0;
        }
        
        public static Vector3 GetNavMeshPosition(Vector3 targetPosition, float maxDistance)
        {
            NavMeshHit navMeshHit;

            if (NavMesh.SamplePosition(targetPosition, out navMeshHit, maxDistance, NavMesh.AllAreas))
            {
                return navMeshHit.position;
            }

            return targetPosition;
        }
        
        public static bool IsSceneExists(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneNameInBuildSettings = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                if (sceneNameInBuildSettings == sceneName)
                {
                    return true;
                }
            }
            return false;
        }
        public static T GetRandomEnumValue<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
        
        public static float RoundToDecimalPlaces(float value, int decimalPlaces)
        {
            float multiplier = (float)Math.Pow(10, decimalPlaces);

            return (float)Math.Round(value * multiplier) / multiplier;
        }
        
        public static bool IsInsideImageArea(Image uiImage, Vector2 inputPos)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(uiImage.rectTransform, inputPos))
            {
                return true;
            }
            return false;
        }
    }
}