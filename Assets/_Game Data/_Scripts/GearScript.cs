using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GearScript : RCC_Core
{
    public Scrollbar gearSlider;

    public int gearDirection = 0;

    public bool Is3D;
    public GameObject ForwardCam, ReverseCam;
    public Transform shiftStick;
    public SteeringWheel3D steeringWheel3D;
    public Vector3 forwardGearAngle, reverseGearAngle;
    public UIManager uiManager;

    private void Start()
    {
        uiManager = UIManager.instance;
    }

    private void OnMouseDown()
    {
        if (!Is3D) return;

        if (uiManager.IsInShop) return;

        if (GameManager.instance.IsTutorial)
        {
            if (PrefsManager.LevelNumber != 2)
            {
                return;
            }
            else
            {
                GameManager.instance.IsTutorial = false;
                GameManager.instance.eventSys.SetActive(true);
                GameManager.instance.ActiveTutorial.SetActive(false);
            }
        }

        uiManager.SwitchToGamePlay();
        ChangeGearOnButton();
    }


    public void ChangeGearOnButton()
    {

        switch (gearDirection)
        {

            case 0:
                RCCSceneManager.activePlayerVehicle.StartCoroutine("ChangeGear", -1);
                RCCSceneManager.activePlayerVehicle.NGear = false;
                gearDirection = -1;
                gearSlider.value = 1;
                ForwardCam.SetActive(false);
                ReverseCam.SetActive(true);
                steeringWheel3D.isReversing = true;
                steeringWheel3D.steeringArcMesh = steeringWheel3D.steeringBackArcMesh;
                steeringWheel3D.steeringFrontArcMesh.gameObject.SetActive(false);
                steeringWheel3D.steeringBackArcMesh.gameObject.SetActive(true);
                shiftStick.DOLocalRotate(reverseGearAngle, 0.15f).SetEase(Ease.Linear);
                SoundManager.instance.SwitchToReverse();
                break;

            case 1:
                RCCSceneManager.activePlayerVehicle.NGear = true;
                break;

            case -1:
                RCCSceneManager.activePlayerVehicle.StartCoroutine("ChangeGear", 0);
                RCCSceneManager.activePlayerVehicle.NGear = false;
                gearDirection = 0;
                gearSlider.value = 0;
                ForwardCam.SetActive(true);
                ReverseCam.SetActive(false);
                steeringWheel3D.isReversing = false;
                steeringWheel3D.steeringArcMesh = steeringWheel3D.steeringFrontArcMesh;
                steeringWheel3D.steeringFrontArcMesh.gameObject.SetActive(true);
                steeringWheel3D.steeringBackArcMesh.gameObject.SetActive(false);
                shiftStick.DOLocalRotate(forwardGearAngle, 0.15f).SetEase(Ease.Linear);
                SoundManager.instance.SwitchToForwrd();
                break;

        }
        RCCSceneManager.activePlayerVehicle.semiAutomaticGear = true;
    }
}
