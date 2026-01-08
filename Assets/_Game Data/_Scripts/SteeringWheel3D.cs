using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel3D : RCC_Core
{



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
    public GearScript GearScript;
    public static RCC_Inputs mobileInputs = new RCC_Inputs();

    void Start()
    {
        if (!steeringWheel) steeringWheel = transform;

        uiManager = UIManager.instance;
    }
    float GassValue;
    float BrakeValue;
    private void OnDisable()
    {
        GassValue = 0;
        BrakeValue = 1;
        mobileInputs.brakeInput = BrakeValue;
        mobileInputs.throttleInput = GassValue;
        if (RCC_SceneManager.Instance && RCC_SceneManager.Instance.activePlayerVehicle)
            RCC_SceneManager.Instance.activePlayerVehicle.OverrideInputs(mobileInputs);
        //if (RCC_SceneManager.Instance.activePlayerVehicle)
        //    RCC_SceneManager.Instance.activePlayerVehicle.Rigid.isKinematic = true;
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

        if (isDragging)
        {
            GassValue = 1;
            BrakeValue = 0;
        }
        else
        {
            GassValue = 0;
            BrakeValue = 1;
        }

        mobileInputs.throttleInput = GassValue;
        if (GearScript.gearDirection == 0)
        {
            mobileInputs.steerInput = steerValue;

        }
        else
        {
            mobileInputs.steerInput = -steerValue;

        }
        mobileInputs.brakeInput = BrakeValue;
        if (RCC_SceneManager.Instance.activePlayerVehicle != null)
            RCC_SceneManager.Instance.activePlayerVehicle.OverrideInputs(mobileInputs);
    }

    public void ToggleDirection()
    {

    }

    void OnMouseDown()
    {
        if (uiManager.IsInShop || PrefsManager.PP == 0) return;

        if (GameManager.instance.IsTutorial && PrefsManager.LevelNumber == 2)
        {
            return;
        }


        if (PrefsManager.LevelNumber < 2 || PrefsManager.LevelNumber == 17)
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
        if (!mainCam || PrefsManager.PP == 0) return;

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
