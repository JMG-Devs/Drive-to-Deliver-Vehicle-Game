using UnityEngine;

public class Joystick3D : MonoBehaviour
{
    public float rotationStrength = 0.5f;
    public float maxTiltAngle = 30f;
    public float resetSpeed = 5f;

    private bool isDragging = false;
    private Vector3 currentTilt;
    private Vector2 inputValue;
    private Vector3 lastMousePos;
    public GameObject tutorial;
    // Get current input for crane


    private void OnEnable()
    {
        if (PrefsManager.Crane == 0)
        {
            tutorial.SetActive(true);
        }
    }
    public Vector2 GetInput()
    {
        return inputValue;
    }

    void OnMouseDown()
    {
        if (PrefsManager.Crane == 0)
        {
            PrefsManager.Crane = 1;
            tutorial.SetActive(false);
        }
        isDragging = true;
        lastMousePos = Input.mousePosition;
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Stop input immediately
        inputValue = Vector2.zero;
    }

    void Update()
    {
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            lastMousePos = Input.mousePosition;

            currentTilt.x += -delta.y * rotationStrength;
            currentTilt.z += delta.x * rotationStrength;

            currentTilt.x = Mathf.Clamp(currentTilt.x, -maxTiltAngle, maxTiltAngle);
            currentTilt.z = Mathf.Clamp(currentTilt.z, -maxTiltAngle, maxTiltAngle);

            transform.localRotation = Quaternion.Euler(currentTilt);

            // Update crane input only while dragging
            inputValue = new Vector2(
                currentTilt.z / maxTiltAngle,
                currentTilt.x / maxTiltAngle
            );
        }
        else
        {
            // Only reset joystick visually
            currentTilt = Vector3.Lerp(
                currentTilt,
                new Vector3(-30f, 0f, 0f),
                Time.deltaTime * resetSpeed
            );
            transform.localRotation = Quaternion.Euler(currentTilt);

            // 🚫 Do NOT change inputValue here — crane stops instantly
        }
    }
}