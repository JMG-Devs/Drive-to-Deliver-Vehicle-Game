using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CarReset : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle"))
        {
            other.GetComponent<DOTweenAnimation>().DORestart();
        }
    }
}
