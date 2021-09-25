using Enemy;
using UnityEngine;

namespace Utilities
{
    public class LevelManager : MonoBehaviour
    {
        private void RestartLevel()
        {
            RestartScene.RestartSceneFunc();
        }

        private void OnEnable()
        {
            EnemyMovement.OnReachedEnd += RestartLevel;
        }

        private void OnDisable()
        {
            EnemyMovement.OnReachedEnd -= RestartLevel;
        }
    }
}