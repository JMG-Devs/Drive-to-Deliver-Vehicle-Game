using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardRef : MonoBehaviour
{
    public GameObject[] Cards;
    public GameObject ray;


    public void EnableAllCards()
    {
        foreach(GameObject g in Cards)
        {
            g.SetActive(true);
        }
    }

    public void EnableRay()
    {
        ray.SetActive(true);
        SoundManager.instance.ChestPlay();
    }
}
