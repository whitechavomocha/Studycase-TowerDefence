using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        public static Action OnEnemyDied;

        [SerializeField] private GameObject healthBarPrefab;
        [SerializeField] private Transform healthBarPos;

        //[SerializeField] private float initialHealth = 100f;
        //[SerializeField] private float maxHealth = 100f;

        [SerializeField] private float initialHealth = 20f;
        [SerializeField] private float maxHealth = 20f;

        public float CurrentHealth { get; set; }

        private Image healthBar;
        private float damage;

        public float Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }

        private void Start()
        {
            CreateHealthBar();
            CurrentHealth = initialHealth;
        }

        private void Update()
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, CurrentHealth / maxHealth, Time.deltaTime * 10f);
        }

        private void CreateHealthBar()
        {
            GameObject newHealthBar = Instantiate(healthBarPrefab, healthBarPos.position, Quaternion.identity);
            newHealthBar.transform.SetParent(transform);

            EnemyHealthBar bar = newHealthBar.GetComponent<EnemyHealthBar>();
            healthBar = bar.FillAmountImage;
        }

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }

        public void ResetHealth()
        {
            CurrentHealth = initialHealth;
            healthBar.fillAmount = 1f;
        }

        private void Die()
        {
            CurrentHealth = initialHealth;
            healthBar.fillAmount = 1f;
            OnEnemyDied?.Invoke();
            Pool.ReturnThePool(gameObject);
        }

        private void OnParticleCollision(GameObject other)
        {
            TakeDamage(damage);
        }
    }
}