using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Monetization.Runtime.Analytics;

public class CraneCP : MonoBehaviour
{
    public Collider selfCollider;
    public GameObject craneDashBoard, detector;
    GameManager gameManager;
    public TowTruckController truckController;
    public Transform rotatorArmY, rodeScalerY, BlackScalerx, roadScalerYY, brokenVehicle, finalPoint;
    public GameObject LevelCompleteTrigger;
    public Transform CheckPointParent, waves;
    public GameObject waveParticle, message;

    public void PerformPick()
    {
        brokenVehicle.GetComponent<Collider>().enabled = false;
        detector.SetActive(false);
        rotatorArmY.DOLocalRotate(new Vector3(00, -90, 0), .5f);
        // rodeScalerY.DOLocalRotate(new Vector3(00, -90, 0), 1f).SetDelay(2f);
        BlackScalerx.DOScaleX(1.3f, .5f).SetDelay(1f);
        roadScalerYY.DOScaleY(75, .5f).SetDelay(1.5f).OnComplete(() => { brokenVehicle.SetParent(finalPoint); });

        brokenVehicle.DOLocalMove(Vector3.zero, 0.5f).SetDelay(2f);
        brokenVehicle.DOLocalRotate(new Vector3(0, 180, 0), 0.5f).SetDelay(2f);
        BlackScalerx.DOScaleX(1f, .5f).SetDelay(2.5f);
        finalPoint.DOLocalMoveY(-1.27f, .5f).SetDelay(3f);
        roadScalerYY.DOScaleY(35, .5f).SetDelay(3f);
        // rodeScalerY.DOLocalRotate(Vector3.zero, 1).SetDelay(8f);
        rotatorArmY.DOLocalRotate(Vector3.zero, .5f).SetDelay(3.5f).OnComplete(
        () =>
        {
            brokenVehicle.SetParent(roadScalerYY);
            LevelCompleteTrigger.SetActive(true);
            ResetMainDashBoard();

        }); ;
        AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber + 1),"Vehicle_Picked_");
    }




    private void Start()
    {
        gameManager = GameManager.instance;
        gameManager.CranePressDown.AddListener(PerformPick);
        craneDashBoard = gameManager.craneDashBoard;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            selfCollider.enabled = false;
            gameManager.steeringWheel3D.enabled = false;
            gameManager.gearScript.enabled = false;
            //gameManager.dashBoardManager.selfRCCInputs.pressing = false;
            //gameManager.dashBoardManager.selfRCCInputs.enabled = false;
            gameManager.DashBoardAnimateOut();
            craneDashBoard.SetActive(true);
            detector.SetActive(true);
            truckController.canControl = true;
            CheckPointParent.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waves.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waveParticle.SetActive(true);
            AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber + 1), "CheckPoint_Completed_");
        }
    }


    public void ResetMainDashBoard()
    {
        craneDashBoard.transform.DOLocalMove(new Vector3(0, -10, 0), 0.5f).SetEase(Ease.InOutSine).SetRelative(true);
        gameManager.steeringWheel3D.enabled = true;
        gameManager.gearScript.enabled = true;
       // gameManager.dashBoardManager.selfRCCInputs.pressing = true;
        //gameManager.dashBoardManager.selfRCCInputs.enabled = true;
        gameManager.DashBoardAnimateIn();
        message.SetActive(true);
    }
}
