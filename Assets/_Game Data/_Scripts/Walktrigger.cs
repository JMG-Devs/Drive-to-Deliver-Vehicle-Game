using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Walktrigger : MonoBehaviour
{
    public Animator[] pedAnim;
    public DOTweenAnimation[] pedTween;
    public Collider selfCollider;
    public GameObject[] green, red;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCollider.enabled = false;

            foreach (Animator a in pedAnim)
            {
                a.SetBool("walk", true);
            }
            foreach (DOTweenAnimation d in pedTween)
            {
                d.DOPlay();
            }
            for (int i = 0; i < green.Length; i++)
            {
                green[i].SetActive(false);
                red[i].SetActive(true);
            }
            Invoke(nameof(DisableWalk), 5.1f);
        }
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





}
