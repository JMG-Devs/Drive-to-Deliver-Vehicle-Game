using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel3D : MonoBehaviour
{
    //[Header("Steering Wheel Settings")]
    //public Transform steeringWheel;
    //public float maxSteerAngle = 270f;
    //public float resetSpeed = 200f;
    //public float sensitivity = 0.4f;

    //[Header("Debug")]
    //public float steeringAngle;
    //public float input;

    //private bool isDragging = false;
    //private float lastMouseX;
    //private Camera mainCam;


    //void Start()
    //{
    //    if (!steeringWheel) steeringWheel = transform;
    //    mainCam = Camera.main;
    //}

    //void Update()
    //{
    //    if (!isDragging)
    //    {
    //        steeringAngle = Mathf.MoveTowards(steeringAngle, 0f, resetSpeed * Time.deltaTime);
    //    }

    //    steeringWheel.localRotation = Quaternion.Euler(0f, 0f, -steeringAngle);
    //    input = Mathf.Clamp(steeringAngle / maxSteerAngle, -1f, 1f);
    //}

    //void OnMouseDown()
    //{
    //    isDragging = true;
    //    lastMouseX = Input.mousePosition.x;
    //}

    //void OnMouseDrag()
    //{
    //    if (!mainCam) return;

    //    float deltaX = Input.mousePosition.x - lastMouseX;
    //    lastMouseX = Input.mousePosition.x;

    //    // Project wheel center into screen space
    //    Vector3 wheelScreenPos = mainCam.WorldToScreenPoint(steeringWheel.position);

    //    // Determine if touch is above or below the wheel’s center
    //    bool isBelowCenter = Input.mousePosition.y < wheelScreenPos.y;

    //    // Flip rotation direction if below the center
    //    float direction = isBelowCenter ? -1f : 1f;

    //    steeringAngle += deltaX * sensitivity * direction;
    //    steeringAngle = Mathf.Clamp(steeringAngle, -maxSteerAngle, maxSteerAngle);
    //}

    //void OnMouseUp()
    //{
    //    isDragging = false;
    //}

    //public float GetSteeringInput()
    //{
    //    return input;
    //}


    [Header("Steering Wheel Settings")]
    public Transform steeringWheel;
    public float maxSteerAngle = 270f;
    public float resetSpeed = 200f;
    public float sensitivity = 0.4f;

    [Header("Car / Arc Settings")]
    public Transform carFront;       // Front bumper
    public Transform carBack;        // Rear bumper
    public float arcLength = 10f;
    public int segments = 20;
    public float maxCurveStrength = 45f;
    public float lineHeight = 0.05f;

    [Header("Debug")]
    public float steeringAngle;
    public float input;
    public bool isReversing = false;   // <--- ✅ toggle this at runtime

    private bool isDragging = false;
    private float lastMouseX;
    public Camera mainCam;
    // public LineRenderer lineRenderer;
    public SkinnedMeshRenderer steeringArcMesh, steeringFrontArcMesh, steeringBackArcMesh; // Mesh with blendshape
    public int blendShapeIndex = 0;
    float steerValue;
    float lastMouseAngle = 0f;
    public UIManager uiManager;
    public GameObject vehicleSource;
    void Start()
    {
        if (!steeringWheel) steeringWheel = transform;

        uiManager = UIManager.instance;
    }

    void Update()
    {
        // --- Handle Steering Wheel Rotation ---
        if (!isDragging)
            steeringAngle = Mathf.MoveTowards(steeringAngle, 0f, resetSpeed * Time.deltaTime);

        steeringWheel.localRotation = Quaternion.Euler(0f, 0f, -steeringAngle);
        input = Mathf.Clamp(steeringAngle / maxSteerAngle, -1f, 1f);


        steerValue = input;
        if (isReversing)
            steerValue = -steerValue;

        // --- Apply Steering BlendShape ---
        if (steeringArcMesh)
        {
            // Convert -1 →1 input into your blendshape range 0 →100
            float blendValue = Mathf.Lerp(0f, 100f, (steerValue + 1f) / 2f);
            steeringArcMesh.SetBlendShapeWeight(blendShapeIndex, blendValue);
        }
    }



    void OnMouseDown()
    {
        if (uiManager.IsInShop) return;

        if (GameManager.instance.IsTutorial && PrefsManager.LevelNumber == 2)
        {
            return;
        }


        if (PrefsManager.LevelNumber < 2||PrefsManager.LevelNumber==17)
        {

            GameManager.instance.IsTutorial = false;
            GameManager.instance.eventSys.SetActive(true);
            GameManager.instance.ActiveTutorial.SetActive(false);
        }

        uiManager.SwitchToGamePlay();
        isDragging = true;
        vehicleSource.SetActive(true);

        Vector3 wheelScreenPos = mainCam.WorldToScreenPoint(steeringWheel.position);
        Vector2 mouse = Input.mousePosition;

        // Store initial angle
        lastMouseAngle = Mathf.Atan2(mouse.y - wheelScreenPos.y, mouse.x - wheelScreenPos.x) * Mathf.Rad2Deg;
    }

    void OnMouseDrag()
    {
        if (!mainCam) return;

        if (GameManager.instance.IsTutorial && PrefsManager.LevelNumber == 2)
        {
            return;
        }

        Vector3 wheelScreenPos = mainCam.WorldToScreenPoint(steeringWheel.position);
        Vector2 mouse = Input.mousePosition;

        // Current angle of mouse around wheel
        float currentAngle = Mathf.Atan2(mouse.y - wheelScreenPos.y, mouse.x - wheelScreenPos.x) * Mathf.Rad2Deg;

        // Angle delta (difference)
        float delta = Mathf.DeltaAngle(lastMouseAngle, currentAngle);
        lastMouseAngle = currentAngle;

        // Apply rotation
        steeringAngle -= delta * sensitivity;
        steeringAngle = Mathf.Clamp(steeringAngle, -maxSteerAngle, maxSteerAngle);
    }

    void OnMouseUp()
    {
        isDragging = false;
        vehicleSource.SetActive(false);
    }
    public float GetSteeringInput() => input;

    // -------------------------------------
    // Draw a smooth curved arc forward or backward
    // -------------------------------------



}
