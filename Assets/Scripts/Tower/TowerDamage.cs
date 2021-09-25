using UnityEngine;

/// <summary>
/// For the each tower's different damage power
/// </summary>
public class TowerDamage : MonoBehaviour
{
    private float damage;
    public float Damage {
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
        damage = Random.Range(10f, 50f);
    }
}