using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Monetization.Runtime.Analytics;
public class CheckPointScriptWithoutCam : MonoBehaviour
{
    public Collider selfCollider;
    GameManager gameManager;
    public GameObject[] nextThingsToOn;
    public UnityEvent actionToPerform,DeliveryEvent;

    public Transform CheckPointParent, waves;
    public GameObject waveParticle;
    public Transform trailer, tractor;
    public Animator[] pedAnim;
    public DOTweenAnimation[] pedTween;
    public GameObject[] green, red;

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
            gameManager.dashBoardManager.selfRCCInputs.pressing = false;
            gameManager.dashBoardManager.selfRCCInputs.enabled = false;
            gameManager.DashBoardAnimateOut();

            // GameManager.instance.dashBoardManager.rccButtonScript.enabled = false;
          
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
                if (nextThingsToOn.Length > 0)
                {
                    foreach (GameObject g in nextThingsToOn)
                    {
                        g.SetActive(true);
                    }
                }
                gameManager.steeringWheel3D.enabled = true;
                gameManager.gearScript.enabled = true;
              //  gameManager.dashBoardManager.selfRCCInputs.pressing = true;
                gameManager.dashBoardManager.selfRCCInputs.enabled = true;
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
        AnalyticsManager.LogRandomEvent((PrefsManager.LevelNumber + 1), "CheckPoint_Completed_");
    }

    public void EnablePedWalk()
    {
        foreach(Animator a in pedAnim)
        {
            a.SetBool("walk", true);
        }
        foreach(DOTweenAnimation d in pedTween)
        {
            d.DOPlay();
        }
        for(int i = 0; i < green.Length; i++)
        {
            green[i].SetActive(false);
            red[i].SetActive(true);
        }
        Invoke(nameof(DisableWalk), 5.1f);
    }

    public void DisableWalk()
    {
        foreach (Animator a in pedAnim)
        {
            a.SetBool("walk", false);
        }
        for (int i = 0; i < green.Length; i++)
        {
            green[i].SetActive(true);
            red[i].SetActive(false);
        }
    }

    public void EnableSignalShift()
    {
        for (int i = 0; i < green.Length; i++)
        {
            green[i].SetActive(false);
            red[i].SetActive(true);
        }
    }
    public void DisableSignalShift()
    {
        for (int i = 0; i < green.Length; i++)
        {
            green[i].SetActive(true);
            red[i].SetActive(false);
        }
    }


    public void StartDrivingAgain()
    {
        trailer.SetParent(tractor);
        trailer.DOLocalMove(new Vector3(0, -0.3f, -3.63f), 0.25f).SetEase(Ease.InOutSine);
    }
}
