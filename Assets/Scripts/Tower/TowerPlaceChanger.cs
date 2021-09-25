using GenerateButton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tower
{
    public class TowerPlaceChanger : MonoBehaviour
    {
        [Header("Place Changing Speed")]
        [SerializeField] [Range(0f, 5f)] private float activatedSpeed = .4f;

        [SerializeField] [Range(0f, 5f)] private float disabledSpeed = 1f;

        [Header("Others")]
        [SerializeField] private GameObject placeChanger;

        [SerializeField] [Tooltip("For stop to coroutine!")] [Range(1, 30)] private int totalTower = 26;
        [SerializeField] private Button generateButton;

        private TowerGenerator towerGenerator;
        [HideInInspector] public GameObject placeChangerParent;

        [SerializeField] private List<GameObject> placeChangers = new List<GameObject>();

        public List<GameObject> PlaceChangers
        {
            get { return placeChangers; }
            set { placeChangers = value; }
        }

        private int randomIndex;

        public int RandomIndex
        {
            get { return randomIndex; }
            private set { randomIndex = value; }
        }

        private void Start()
        {
            towerGenerator = FindObjectOfType<TowerGenerator>();

            placeChangerParent = new GameObject($"{placeChanger.name}s");

            for (int i = 0; i < towerGenerator.Waypoints.Count; i++)
            {
                GameObject towerPlaceChanger = Instantiate(placeChanger, towerGenerator.Waypoints[i].transform.position, Quaternion.identity);
                towerPlaceChanger.transform.SetParent(placeChangerParent.transform);
                placeChangers.Add(towerPlaceChanger);
                SpriteRenderer sr = towerPlaceChanger.gameObject.GetComponent<SpriteRenderer>();
                sr.enabled = false;
            }

            StartCoroutine(EnablePlaceChanger());
        }

        private IEnumerator EnablePlaceChanger()
        {
            randomIndex = Random.Range(0, towerGenerator.Waypoints.Count);
            generateButton.enabled = true;

            while (placeChangers[randomIndex] == null)
            {
                randomIndex = Random.Range(0, towerGenerator.Waypoints.Count);
            }

            if (placeChangers[randomIndex] != null)
            {
                SpriteRenderer sr = placeChangers[randomIndex].gameObject.GetComponent<SpriteRenderer>();
                sr.enabled = true;
            }

            yield return new WaitForSeconds(activatedSpeed);

            StartCoroutine(DisablePlaceChanger(randomIndex));
        }

        private IEnumerator DisablePlaceChanger(int index)
        {
            generateButton.enabled = false;
            if (placeChangers[index] != null)
            {
                SpriteRenderer sr = placeChangers[index].gameObject.GetComponent<SpriteRenderer>();
                sr.enabled = false;
            }

            yield return new WaitForSeconds(disabledSpeed);

            if (towerGenerator.Counter < totalTower)
            {
                StartCoroutine(EnablePlaceChanger());
            }
            else
            {
                generateButton.GetComponentInParent<ButtonDisabler>().DestroyButton();
            }
        }
    }
}