using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject[] TabsContent, TabFillers,shopSteering,shopGear,shopdashItem,shopSticker;
    public TMP_Text walletText;

    private void OnEnable()
    {
        ChangeTab(0);
        walletText.text = PrefsManager.Money + "$";
    }

    public void ChangeTab(int number)
    {
        for (int i = 0; i < TabsContent.Length; i++)
        {
            if (i == number)
            {
                TabsContent[i].SetActive(true);
                TabFillers[i].SetActive(true);

            }
            else
            {
                TabsContent[i].SetActive(false);
                TabFillers[i].SetActive(false);
            }
        }
        SoundManager.instance.ButtonPlay();
    }

    public void EquipFinalizedSteeringState()
    {
        foreach(GameObject g in shopSteering)
        {
            g.SetActive(false);
        }
        shopSteering[PrefsManager.SteeringNumber].SetActive(true);
        foreach (GameObject g in shopGear)
        {
            g.SetActive(false);
        }
        shopGear[PrefsManager.StickNumber].SetActive(true);
        foreach (GameObject g in shopdashItem)
        {
            g.SetActive(false);
        }
        shopdashItem[PrefsManager.DashBoardItemNumber].SetActive(true);
        foreach (GameObject g in shopSticker)
        {
            g.SetActive(false);
        }
        shopSticker[PrefsManager.StickerNumber].SetActive(true);
    }


}
