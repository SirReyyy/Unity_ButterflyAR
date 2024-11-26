using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation
    public Vector2 xRotationBounds = new Vector2(30f, 0f); // Bounds for X-axis rotation
    public Vector2 yRotationBounds = new Vector2(-60f, 60f); // Bounds for Y-axis rotation

    private Vector3 currentRotation; // Current rotation of the camera

    void Start()
    {
        // Initialize the current rotation
        currentRotation = transform.eulerAngles;
    }

    void Update()
    {
        float xInput = 0f;
        float yInput = 0f;

        // Get WASD inputs for rotation
        if (Input.GetKey(KeyCode.W)) xInput = 1f;
        if (Input.GetKey(KeyCode.S)) xInput = -1f;
        if (Input.GetKey(KeyCode.A)) yInput = -1f;
        if (Input.GetKey(KeyCode.D)) yInput = 1f;

        // Calculate rotation changes
        float xRotation = xInput * rotationSpeed * Time.deltaTime;
        float yRotation = yInput * rotationSpeed * Time.deltaTime;

        // Apply bounds to the rotation
        currentRotation.x = Mathf.Clamp(currentRotation.x - xRotation, xRotationBounds.x, xRotationBounds.y);
        currentRotation.y = Mathf.Clamp(currentRotation.y + yRotation, yRotationBounds.x, yRotationBounds.y);

        // Apply the rotation to the camera
        transform.eulerAngles = currentRotation;
    }
}
