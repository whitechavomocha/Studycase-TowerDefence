using GenerateButton;
using System.Collections;
using TMPro;
using Tower;
using UnityEngine;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float minDelay = 2f;
        [SerializeField] private float maxDelay = 5f;
        [SerializeField] private int enemyCount = 2;
        [SerializeField] private float timeBtwWaves = 5f;
        [SerializeField] private float enemySpeed = 4f;
        [SerializeField] TMP_Text killcounter;
        [SerializeField] GameObject generateButton;

        public float EnemySpeed
        {
            get { return enemySpeed; }
            private set { value = enemySpeed; }
        }
        [SerializeField] private TMP_Text waveAlert;

        private float spawnTimer;
        private int enemiesSpawned = 0;
        private int remainedEnemies = 0;
        private Pool enemyPool;
        private Animator waveAlertAnimator;
        private int killcount = 0;
        private TowerGenerator towerGenerator;

        private void Start()
        {
            enemyPool = GetComponent<Pool>();
            towerGenerator = FindObjectOfType<TowerGenerator>();
            waveAlertAnimator = waveAlert.GetComponent<Animator>();

            remainedEnemies = enemyCount;
            waveAlertAnimator.SetTrigger("wavealert");
        }

        private void Update()
        {
            spawnTimer -= Time.deltaTime;

            if (spawnTimer < 0)
            {
                spawnTimer = GetRandomDelay();
                if (enemiesSpawned < enemyCount)
                {
                    enemiesSpawned++;
                    SpawnEnemy();
                }
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemy = enemyPool.GetInstanceFromPool();
            enemy.SetActive(true);
        }

        private float GetRandomDelay()
        {
            float randomTimer = UnityEngine.Random.Range(minDelay, maxDelay);
            return randomTimer;
        }

        private IEnumerator NextWave()
        {
            yield return new WaitForSeconds(timeBtwWaves);
            enemyCount++;
            enemySpeed -= 0.5f;
            if (enemySpeed <= 0.5f)
            {
                enemySpeed = 0.5f;
            }
            remainedEnemies = enemyCount;
            spawnTimer = 0f;
            enemiesSpawned = 0;
            waveAlertAnimator.SetTrigger("wavealert");
        }

        private void RecordEnemy()
        {
            killcount++;
            killcounter.text = $"{killcount}\n slime has slained!";
            if (killcount % 3 == 0)
            {
                ++towerGenerator.TowerCount;
            }
            if (killcount >= 25)
            {
                ButtonMovement buttonmove = generateButton.GetComponent<ButtonMovement>();
                buttonmove.enabled = true;
            }
            remainedEnemies--;
            if (remainedEnemies == 0)
            {
                StartCoroutine(NextWave());
            }
        }

        private void OnEnable()
        {
            EnemyMovement.OnReachedEnd += RecordEnemy;
            EnemyHealth.OnEnemyDied += RecordEnemy;
        }

        private void OnDisable()
        {
            EnemyMovement.OnReachedEnd -= RecordEnemy;
            EnemyHealth.OnEnemyDied -= RecordEnemy;
        }
    }
}