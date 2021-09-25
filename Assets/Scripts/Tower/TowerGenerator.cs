using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tower
{
    public class TowerGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject towerPrefab;
        [SerializeField] TMP_Text towerCounter;

        [Header("Tower Generatable Places")]
        [SerializeField] private List<WaypointTower> waypoints = new List<WaypointTower>();

        public List<WaypointTower> Waypoints
        {
            get { return waypoints; }
            private set { waypoints = value; }
        }

        private GameObject towerParent;
        private TowerPlaceChanger towerPlaceChanger;
        private int counter = 1;
        private int towerCount = 3;

        public int Counter
        {
            get
            {
                return counter;
            }
            set
            {
                counter = value;
            }
        }

        public int TowerCount
        {
            get
            {
                return towerCount;
            }
            set
            {
                towerCount = value;
            }
        }

        private void Start()
        {
            towerPlaceChanger = FindObjectOfType<TowerPlaceChanger>();
            towerParent = new GameObject("Towers");
        }

        public void GenerateTower()
        {
            if (towerCount > 0)
            {
                InstantiateTower();
            }
        }

        private void Update()
        {
            towerCounter.text = $"{towerCount}\n Tower Remained!";
        }

        private void InstantiateTower()
        {
            if (waypoints[towerPlaceChanger.RandomIndex] != null)
            {
                GameObject tower = Instantiate(towerPrefab, waypoints[towerPlaceChanger.RandomIndex].transform.position, Quaternion.identity);
                counter++;
                if (towerPlaceChanger.PlaceChangers[towerPlaceChanger.RandomIndex] != null)
                {
                    towerPlaceChanger.PlaceChangers[towerPlaceChanger.RandomIndex].SetActive(false);
                }
                waypoints[towerPlaceChanger.RandomIndex] = null;
                towerPlaceChanger.PlaceChangers[towerPlaceChanger.RandomIndex] = null;
                tower.transform.SetParent(towerParent.transform);
                towerCount--;
            }
        }
    }
}