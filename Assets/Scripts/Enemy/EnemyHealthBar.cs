using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image fillAmountImage;
        public Image FillAmountImage => fillAmountImage;
    }
}