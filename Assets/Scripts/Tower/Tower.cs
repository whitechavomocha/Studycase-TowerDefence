using Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float attackRange = 1.25f;
        [SerializeField] private ParticleSystem attackParticle;

        public EnemyMovement CurrrentEnemyTarget { get; set; }

        private bool gameStarted = false;
        private List<EnemyMovement> enemies;

        private void Start()
        {
            gameStarted = true;
            enemies = new List<EnemyMovement>();
        }

        private void Update()
        {
            GetCurrentEnemyTarget();
            RotateToTarget();
        }

        private void GetCurrentEnemyTarget()
        {
            if (enemies.Count <= 0)
            {
                CurrrentEnemyTarget = null;
                return;
            }

            CurrrentEnemyTarget = enemies[0];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                attackParticle.Play();
                EnemyMovement newEnemy = collision.GetComponent<EnemyMovement>();
                enemies.Add(newEnemy);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
                if (enemies.Contains(enemy))
                {
                    enemies.Remove(enemy);
                }
            }
        }

        private void RotateToTarget()
        {
            if (CurrrentEnemyTarget == null)
            {
                attackParticle.Stop();
                return;
            }

            Vector3 targetPos = CurrrentEnemyTarget.transform.position - transform.GetChild(0).transform.position;
            float angle = Vector3.SignedAngle(transform.GetChild(0).transform.up, targetPos, transform.GetChild(0).transform.forward);
            transform.GetChild(0).transform.Rotate(0f, 0f, angle);


            CurrrentEnemyTarget.GetComponent<EnemyHealth>().Damage = transform.GetComponent<TowerDamage>().Damage;
        }

        private void OnDrawGizmos()
        {
            if (!gameStarted)
            {
                GetComponent<CircleCollider2D>().radius = attackRange;
            }
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}