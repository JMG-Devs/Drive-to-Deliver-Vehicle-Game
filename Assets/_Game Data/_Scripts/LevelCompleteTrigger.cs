using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelCompleteTrigger : MonoBehaviour
{
    public Collider selfCollider;
    GameManager gameManager;
    public Transform ParkingParent, waves;
    public GameObject waveParticle;
    public GameObject LevelCompleteMessage;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCollider.enabled = false;
            gameManager.levelCompleted();
            // GameManager.instance.dashBoardManager.rccButtonScript.enabled = false;

            ParkingParent.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waves.DOScale(0, 0.5f).SetEase(Ease.InBack);
            waveParticle.SetActive(true);
            transform.DOScale(0, 0.5f).SetEase(Ease.InBack);
            if (LevelCompleteMessage != null)
            {
                LevelCompleteMessage.SetActive(true);
            }
        }
    }
}
