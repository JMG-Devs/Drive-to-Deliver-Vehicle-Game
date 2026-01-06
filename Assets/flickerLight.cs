using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flickerLight : MonoBehaviour
{
    public GameObject[] yellowLight;
    WaitForSeconds time;
    bool IsIn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tram"))
        {
            IsIn = true;
            StartCoroutine(LightFlicker());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tram"))
        {
            IsIn = false;
          
        }
    }



    IEnumerator LightFlicker()
    {
        while (IsIn)
        {
            yellowLight[0].SetActive(true);
            yellowLight[1].SetActive(true);
            yield return new WaitForSeconds(0.25f);
            yellowLight[0].SetActive(false);
            yellowLight[1].SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        yield return null;
    }
}
