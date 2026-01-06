using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class VehicleBodyDetect : MonoBehaviour
{
    public DOTweenAnimation[] tween;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(DOTweenAnimation d in tween)
            {
            d.DOPause();

            }
        }
    }
}
