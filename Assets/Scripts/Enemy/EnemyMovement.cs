using Path;
using System;
using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        public static Action OnReachedEnd;

        private Vector3[] points;
        private Waypoint waypoint;
        private float travelPercent;
        private Vector3 startPos;
        private Vector3 endPos;
        private EnemySpawner enemySpawner;

        private EnemyHealth enemyHealth;
        public EnemyHealth EnemyHealth
        { get; set; }

        private void OnEnable()
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
            enemyHealth = GetComponent<EnemyHealth>();

            FindPath();
            ReturnToStart();
            StartCoroutine(FollowPath());
        }

        private void ReturnToStart()
        {
            transform.position = points[0];
        }

        private void FindPath()
        {
            waypoint = FindObjectOfType<Waypoint>();
            points = waypoint.Points;
        }

        private IEnumerator FollowPath()
        {
            for (int i = 0; i < points.Length; i++)
            {
                startPos = transform.position;
                endPos = points[i];
                travelPercent = 0f;

                while (travelPercent < 1f)
                {
                    travelPercent += (Time.deltaTime / enemySpawner.EnemySpeed);
                    transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                    yield return new WaitForEndOfFrame();
                }

                if (this.transform.position == points[(points.Length - 1)])
                {
                    ReturnEnemyToPool();
                }
            }
        }

        private void ReturnEnemyToPool()
        {
            OnReachedEnd?.Invoke();
            enemyHealth.ResetHealth();
            ReturnToStart();
            Pool.ReturnThePool(gameObject);
        }
    }
}