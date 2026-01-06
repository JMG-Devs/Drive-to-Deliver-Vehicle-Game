using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GearTab : MonoBehaviour
{
    public GameObject[] Buttons, Highlight, Items;
    public ShopManager shopManager;
    public Button[] Button;
    int SelectedItem, check;
    private void OnEnable()
    {
       
        StartCoroutine(Anim());
        shopManager.EquipFinalizedSteeringState();
        CheckButtonInteraction();
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
        Highlight[PrefsManager.StickNumber].SetActive(true);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].transform.DOScale(1, .3f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(.05f);
        }

    }

    public void CheckButtonInteraction()
    {
        for (int i = 0; i < Button.Length; i++)
        {
            check = PlayerPrefs.GetInt("Gear" + i);
            if (check == 0) continue;

            Button[i].interactable = true;
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
        PrefsManager.StickNumber = number;
        GameManager.instance.dashBoardManager.EnableDashBoardReferences();
        SoundManager.instance.ButtonPlay();
    }
}
