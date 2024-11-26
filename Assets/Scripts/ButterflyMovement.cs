using System.Collections;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    public float speed = 3f; // Movement speed of the butterfly prefab
    public float pointerSpeed = 1f; // Movement speed of the front pointer
    public float minScale = 0.3f; // Minimum scale of the butterfly
    public float maxScale = 0.5f; // Maximum scale of the butterfly
    public Transform frontPointer; // The front pointer for movement
    public Gradient[] trailGradients; // Array of gradients to randomize from

    public Vector3 movementBounds = new Vector3(800f, 600f, 600f); // Bounds for movement

    private float changeDirectionTime; // Time to change pointer direction
    private Vector3 currentPointerDirection; // Current direction of the front pointer

    void Start()
    {
        // Set a random scale
        float scale = Random.Range(minScale, maxScale);
        transform.localScale = new Vector3(scale, scale, scale);

        // Randomize trail gradient
        if (trailGradients.Length > 0)
        {
            TrailRenderer trail = GetComponentInChildren<TrailRenderer>();
            if (trail != null)
            {
                Gradient randomGradient = trailGradients[Random.Range(0, trailGradients.Length)];
                trail.colorGradient = randomGradient;
            }
        }

        // Initialize pointer direction
        SetRandomPointerDirection();
    }

    void Update()
    {
        // Move the front pointer in its current direction
        if (frontPointer != null)
        {
            frontPointer.position += currentPointerDirection * pointerSpeed * Time.deltaTime;

            // Clamp the pointer position within bounds
            frontPointer.position = new Vector3(
                Mathf.Clamp(frontPointer.position.x, -movementBounds.x / 2, movementBounds.x / 2),
                Mathf.Clamp(frontPointer.position.y, 0, movementBounds.y),
                Mathf.Clamp(frontPointer.position.z, -movementBounds.z / 2, movementBounds.z / 2)
            );

            // Randomly change the pointer direction after some time
            if (Time.time >= changeDirectionTime)
            {
                SetRandomPointerDirection();
            }
        }

        // Move the butterfly prefab towards the front pointer
        if (frontPointer != null)
        {
            Vector3 directionToPointer = (frontPointer.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, frontPointer.position, speed * Time.deltaTime);

            // Rotate the butterfly to face the pointer direction
            if (directionToPointer != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(directionToPointer);
                Vector3 eulerRotation = toRotation.eulerAngles;
                eulerRotation.x = Mathf.Clamp(eulerRotation.x, -110f, -70f); // Limit X-axis rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(eulerRotation), Time.deltaTime * 5f);
            }
        }
    }

    private void SetRandomPointerDirection()
    {
        if (frontPointer != null)
        {
            // Set a new random direction for the front pointer
            currentPointerDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-0.1f, 0.1f), // Small vertical variation
                Random.Range(-1f, 1f)
            ).normalized;

            // Set a random time to change direction again
            changeDirectionTime = Time.time + Random.Range(10f, 15f);
        }
    }
}
