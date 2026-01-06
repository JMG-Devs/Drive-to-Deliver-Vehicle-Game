using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowTruckController : MonoBehaviour
{
    public bool canControl = false;

    [Header("Tow Truck Parts")]
    public GameObject rotatorArm;
    public GameObject scalerArm;
    public GameObject liftRotator;
    public GameObject blackScaler;
    public GameObject rodeScaler;
    public MeshRenderer detector;

    [Header("Rotator Arm Y-axis Limits")]
    public float rotatorArmMinimumYLimit = -90F;  //-90f
    public float rotatorArmMaxYLimit = 90F;      //90f
    public float sensitivityRotatorArmY = 0.5f;  //0.5f

    [Header("Scalar Arm Z-axis Limits")]
    public float scalarArmMinimumZLimit = 1f; //1f
    public float scalarArmMaxZLimit = 1.5f;  // 1.5f

    float rotationY = 0F;
    public float scalarZ = 0.0065f;
    float value = 0;

    public static TowTruckController instance;

    [Space(5)]
    [Header("LOGs")]
    public bool canPerformTask = false;
    public Joystick3D joystick;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void OnEnable()
    {
        joystick = GameManager.instance.joystick3DScript;
    }
    // Update is called once per frame
    void Update()
    {
        if (!canControl || joystick == null) return;

        Vector2 input = joystick.GetInput();
        float horizontal = input.x; // left/right tilt on joystick
        float vertical = input.y;   // forward/back tilt

        // --- Rotator Arm Rotation (Y axis) ---
        rotationY += horizontal * sensitivityRotatorArmY * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, rotatorArmMinimumYLimit, rotatorArmMaxYLimit);
        rotatorArm.transform.localEulerAngles = new Vector3(0, -rotationY, 0);

        // --- Scalar Arm Z scaling (extend/retract) ---
        if (vertical > 0)
        {
            if (scalerArm.transform.localScale.z < scalarArmMaxZLimit)
                scalerArm.transform.localScale += new Vector3(0, 0, scalarZ * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            if (scalerArm.transform.localScale.z > scalarArmMinimumZLimit)
                scalerArm.transform.localScale += new Vector3(0, 0, -scalarZ * Time.deltaTime);
        }
    }



}
