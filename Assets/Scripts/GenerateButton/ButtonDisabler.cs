using UnityEngine;

namespace GenerateButton
{
    public class ButtonDisabler : MonoBehaviour
    {
        public void DestroyButton()
        {
            Destroy(this.gameObject);
        }
    }
}