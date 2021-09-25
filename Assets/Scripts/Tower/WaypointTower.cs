using UnityEngine;

namespace Tower
{
    public class WaypointTower : MonoBehaviour
    {
        private bool isPlacable = true;

        public bool IsPlacable
        {
            get { return isPlacable; }
            set { isPlacable = value; }
        }
    }
}