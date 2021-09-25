using UnityEngine;

namespace GenerateButton
{
    public class ButtonMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 movementVector;
        [SerializeField] private float period = 2f;

        private float movementFactor;
        private Vector3 startingPosition;

        private void Awake()
        {
            startingPosition.x = transform.position.x - (movementVector.x / 2);
            startingPosition.y = transform.position.y - movementVector.y;
        }

        private void Update()
        {
            float cycles = Time.time / period;

            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);

            movementFactor = (rawSinWave + 1f) / 2f;

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}