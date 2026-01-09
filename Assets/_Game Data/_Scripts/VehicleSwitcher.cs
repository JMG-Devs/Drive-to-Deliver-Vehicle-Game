using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Monetization.Runtime.Analytics;

public class VehicleSwitcher : MonoBehaviour
{
    public Collider selfCollider;
    public GameObject newVehicle, newVehicleForwardCam, newVehicleReverseCam;
    GameManager gameManager;
    public int cameraSwitchTime, dashboardToactivate;
    public RCC_CarControllerV4 rccScript,vehicleRcc;
    public Transform newVehicleFront, newVehicleBack;
    public SkinnedMeshRenderer steeringFrontArcMesh, steeringBackArcMesh,OldArc;
    public Transform CheckPointParent, waves;
    public GameObject waveParticle,LevelComplete;
    public float timeDelay=2;
    public GameObject message;
    private void Start()
    {
        gameManager = GameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCollider.enabled = false;
           // gameManager.steeringWheel3D.lineRenderer.enabled = false;
            gameManager.steeringWheel3D.enabled = false;
            gameManager.gearScript.enabled = false;
            //gameManager.dashBoardManager.selfRCCInputs.pressing = false;
            //gameManager.dashBoardManager.selfRCCInputs.enabled = false;
            gameManager.DashBoardAnimateOut();
            gameManager.ActiveDashBoard = dashboardToactivate;
            gameManager.front = newVehicleFront;
            gameManager.back = newVehicleBack;
            OldArc.gameObject.SetActive(false);
            steeringFrontArcMesh.gameObject.SetActive(true);
            gameManager.steeringFrontArcMesh = steeringFrontArcMesh;
            gameManager.steeringBackArcMesh = steeringBackArcMesh;
     
            CheckPointParent.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waves.DOScale(0, 0.5f).SetEase(Ease.InBack);
            AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber + 1), "First_Vehicle_Parked");
            waveParticle.SetActive(true);
            Invoke(nameof(FeedReferences), 0.75f);
            Invoke(nameof(EnableDashBoardDelay),timeDelay);
        }
    }

    public void FeedReferences()
    {
       
        rccScript.enabled = false;
        vehicleRcc.enabled = true;
      
        gameManager.brain.m_DefaultBlend.m_Time = cameraSwitchTime;
        newVehicleForwardCam.SetActive(true);
        gameObject.SetActive(false);
        LevelComplete.SetActive(true);
    }


    public void EnableDashBoardDelay()
    {
        message.SetActive(true);
        gameManager.EnableAppropriateDashBoard();
        gameManager.dashBoardManager.selfGearScript.ForwardCam = newVehicleForwardCam;
        gameManager.dashBoardManager.selfGearScript.ReverseCam = newVehicleReverseCam;
        gameManager.DashBoards[3].SetActive(false);
    }
}
