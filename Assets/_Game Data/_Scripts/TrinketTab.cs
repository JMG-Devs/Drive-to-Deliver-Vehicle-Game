using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrinketTab : MonoBehaviour
{
    public GameObject[] Buttons, Highlight, Items;
    public ShopManager shopManager;
    private void OnEnable()
    {
        StartCoroutine(Anim());
        shopManager.EquipFinalizedSteeringState();
    }


    IEnumerator Anim()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.localScale = Vector3.zero;
        }
        foreach (GameObject g in Highlight)
        {
            g.SetActive(false);
        }
        Highlight[PrefsManager.TrinketNumber].SetActive(true);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.DOScale(1, .3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.05f);
        }
    }



    public void EnableSelectedIten(int number)
    {
        foreach (GameObject g in Highlight)
        {
            g.SetActive(false);
        }
        Highlight[number].SetActive(true);
        foreach (GameObject g in Items)
        {
            g.SetActive(false);
        }
        Items[number].SetActive(true);
    }
}
