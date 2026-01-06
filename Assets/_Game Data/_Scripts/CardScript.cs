using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardScript : MonoBehaviour
{
    public Collider[] coll;
    public DOTweenAnimation[] tweens;
    public RewardSystem rewardSystem;
    private void OnMouseDown()
    {
        for (int i = 0; i < coll.Length; i++)
        {
            coll[i].enabled = false;
            tweens[i].DOPause();
        }
        rewardSystem.ChooseReward.SetActive(false);
        coll[1].transform.DOScale(Vector3.zero, 0.25f);
        coll[2].transform.DOScale(Vector3.zero, 0.25f);
        transform.DOLocalMove(new Vector3(0, 0, 3.63f), 0.5f);
        transform.DOLocalRotate(Vector3.zero, 0.5f);
                SoundManager.instance.CardClickedPlay();
        transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(
            () =>
            {
                transform.DOScale(Vector3.zero, 0.25f);
                rewardSystem.Point1.gameObject.SetActive(true);
                rewardSystem.FreeClaimButton.SetActive(true);
                SoundManager.instance.AfterCardClickedPlay();
            });

    }
}
