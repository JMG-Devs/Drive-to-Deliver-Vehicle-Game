using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Monetization.Runtime.Analytics;
public class CraneDropCP : MonoBehaviour
{
    public Collider selfCollider;
    public GameObject craneDashBoard, detector;
    GameManager gameManager;
    public TowTruckController truckController;
    public Transform rotatorArmY, rodeScalerY, BlackScalerx, roadScalerYY, brokenVehicle, finalPoint, craneObject;
    public Transform CheckPointParent, waves;
    public GameObject waveParticle, reachedMessage, completeMessage;


    public void PerformDrop()
    {


        brokenVehicle.SetParent(craneObject);
        detector.SetActive(false);
        rotatorArmY.DOLocalRotate(new Vector3(00, -90, 0), .5f);
        // rodeScalerY.DOLocalRotate(new Vector3(00, -90, 0), 1f).SetDelay(2f);
        BlackScalerx.DOScaleX(1.3f, .5f).SetDelay(1f);
        roadScalerYY.DOScaleY(75, .5f).SetDelay(1.5f);
        brokenVehicle.DOLocalMoveY(-3.7f, 0.5f).SetDelay(1.5f).OnComplete(() => { brokenVehicle.SetParent(finalPoint); }); ;
        brokenVehicle.DOLocalMove(Vector3.zero, 0.5f).SetDelay(2f);
        brokenVehicle.DOLocalRotate(new Vector3(0, 180, 0), 0.5f).SetDelay(2f);
        BlackScalerx.DOScaleX(1f, .5f).SetDelay(2.5f);
        //  finalPoint.DOLocalMoveY(-1.27f, .5f).SetDelay(3f);
        roadScalerYY.DOScaleY(35, .5f).SetDelay(3f);
        // rodeScalerY.DOLocalRotate(Vector3.zero, 1).SetDelay(8f);
        rotatorArmY.DOLocalRotate(Vector3.zero, .5f).SetDelay(3.5f).OnComplete(() => LevelCompleteAfterDrop());
        AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber + 1), "Vehicle_droped_");
    }




    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.CranePressDown.AddListener(PerformDrop);
        craneDashBoard = gameManager.craneDashBoard;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            selfCollider.enabled = false;
            gameManager.steeringWheel3D.enabled = false;
            gameManager.gearScript.enabled = false;
            gameManager.dashBoardManager.selfRCCInputs.pressing = false;
            gameManager.dashBoardManager.selfRCCInputs.enabled = false;
            gameManager.DashBoardAnimateOut();
            craneDashBoard.SetActive(true);
            detector.SetActive(true);
            truckController.canControl = true;
            CheckPointParent.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waves.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waveParticle.SetActive(true);
            reachedMessage.SetActive(true);
            AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber + 1), "CheckPoint_Completed_");
        }
    }


    public void LevelCompleteAfterDrop()
    {
        completeMessage.SetActive(true);
        craneDashBoard.transform.DOLocalMove(new Vector3(0, -10, 0), 0.5f).SetEase(Ease.InOutSine).SetRelative(true);
        PrefsManager.LevelNumber += 1;
        gameManager.levelCompletedSceneSwitch();
       
    }
}
