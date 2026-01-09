using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Monetization.Runtime.Analytics;
public class CheckPointScript : MonoBehaviour
{
    public Collider selfCollider;
    public GameObject secondForwardCam, secondReverseCam;
    GameManager gameManager;
    public GameObject[] nextThingsToOn;
    public UnityEvent actionToPerform, DeliveryEvent;

    public Transform CheckPointParent, waves;
    public GameObject waveParticle;
    public Transform trailer, tractor;


    private void Start()
    {
        gameManager = GameManager.instance;
        if (selfCollider != null) return;

        selfCollider = GetComponent<Collider>();

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
           
            // GameManager.instance.dashBoardManager.rccButtonScript.enabled = false;
            if (nextThingsToOn.Length > 0)
            {
                foreach (GameObject g in nextThingsToOn)
                {
                    g.SetActive(true);
                }
            }
            CheckPointParent.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waves.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waveParticle.SetActive(true);
            SoundManager.instance.CheckPointPlay();
            transform.DOScale(0, 0.75f).SetEase(Ease.InBack).OnComplete(() =>
            {
                if (DeliveryEvent != null)
                {
                    DeliveryEvent?.Invoke();
                    SoundManager.instance.PuffPlay();
                }
                secondForwardCam.SetActive(true);
                gameManager.steeringWheel3D.enabled = true;
                gameManager.gearScript.enabled = true;
               // gameManager.dashBoardManager.selfRCCInputs.pressing = true;
                //gameManager.dashBoardManager.selfRCCInputs.enabled = true;
                gameManager.dashBoardManager.selfGearScript.ForwardCam = secondForwardCam;
                gameManager.dashBoardManager.selfGearScript.ReverseCam = secondReverseCam;
                gameManager.DashBoardAnimateIn();
                if (actionToPerform != null)
                {
                    actionToPerform?.Invoke();
                }
            });
        }


    }

    public void SendCheckPointLog()
    {
        AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber+1),"CheckPoint_Completed_");
    }


    public void SetTrailerChild()
    {
        trailer.SetParent(tractor);
       // trailer.DOLocalMove(new Vector3(0, -0.3f, -3.63f), 0.25f).SetEase(Ease.InOutSine);
       // trailer.DOLocalRotate(Vector3.zero, 0.25f).SetEase(Ease.InOutSine);
        trailer.localPosition = new Vector3(0, -0.3f, -3.63f);
        trailer.localRotation = Quaternion.identity;
    }
}
