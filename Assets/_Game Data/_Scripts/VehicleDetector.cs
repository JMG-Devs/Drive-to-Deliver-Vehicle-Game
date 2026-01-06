using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VehicleDetector : MonoBehaviour
{
    public DOTweenAnimation[] tween;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Vehicle"))
        {

            foreach (DOTweenAnimation d in tween)
            {
                d.DOPause();

            }
        }

    }
}
