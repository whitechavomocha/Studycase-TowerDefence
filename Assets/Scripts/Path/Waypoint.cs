using UnityEngine;

namespace Path
{
    public class Waypoint : MonoBehaviour
    {
        [SerializeField] Vector3[] points;
        public Vector3[] Points => points;

        private Vector3 currentPos;
        public Vector3 CurrentPos => currentPos;

        private bool isGameStarted;

        private void Start()
        {
            isGameStarted = true;
            currentPos = transform.position;
        }

        private void OnDrawGizmos()
        {
            if (!isGameStarted && transform.hasChanged)
            {
                currentPos = transform.position;
            }

            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(points[i] + currentPos, 0.2f);
                if (i < points.Length - 1)
                {
                    Gizmos.DrawLine(points[i] + currentPos, points[i + 1] + currentPos);
                }
            }
        }
    }
}