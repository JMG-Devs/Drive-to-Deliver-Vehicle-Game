using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class StickerTab : MonoBehaviour
{
    public GameObject[] Buttons, Highlight, Items;
    public ShopManager shopManager;
    public Button[] Button;
    int SelectedItem, check;
    public GameObject EventSystem;
    public Button CashRandomButton, AdRandomButton;
    public TMP_Text walletText;
    int initialMoney, finalMoney;

    private void OnEnable()
    {
        
        
        StartCoroutine(Anim());
        shopManager.EquipFinalizedSteeringState();
        CheckButtonInteraction();
        if (PrefsManager.AvailableStickers + 1 > 6)
        {
            AdRandomButton.interactable = false;
            CashRandomButton.interactable = false;
        }
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
        Highlight[PrefsManager.StickerNumber].SetActive(true);
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
            check = PlayerPrefs.GetInt("Sticker" + i);
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
        PrefsManager.StickerNumber = number;
        GameManager.instance.dashBoardManager.EnableDashBoardReferences();
        SoundManager.instance.ButtonPlay();
    }

    int CalculateRandom()
    {
        int RandomValue;
        do
        {
            RandomValue = Random.Range(0, 8);

        } while (PlayerPrefs.GetInt("Sticker" + RandomValue) == 1);


        return RandomValue;
    }

    public void UnlockStickerWithAd()
    {
        MonetizationServices.Ads.ShowRewarded(UnlockRandom, "Sticker_Unlocked_Ad");
    }

    public void UnlockStickerWithCash()
    {
        if (PrefsManager.Money >= 1000)
        {
            UnlockRandom();
            initialMoney = PrefsManager.Money;
            PrefsManager.Money -= 1000;
            finalMoney = PrefsManager.Money;
            MoneyDeductionCountAnimate();
        }
    }


    public void MoneyDeductionCountAnimate()
    {
        walletText.text = initialMoney + "$";
        DOTween.To(() => initialMoney, x => initialMoney = x, finalMoney, 1).OnUpdate(() => walletText.text = initialMoney + "$");

    }



    public void UnlockRandom()
    {
        EventSystem.SetActive(false);
        for (int i = 0; i < 8; i++)
        {
            Items[i].SetActive(false);
            Highlight[i].SetActive(false);

        }
        CashRandomButton.interactable = false;
        AdRandomButton.interactable = false;
        StartCoroutine(RandomAnimation(CalculateRandom()));
        SoundManager.instance.ButtonPlay();
    }

    IEnumerator RandomAnimation(int Give)
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 8; i++)
            {

                if (PlayerPrefs.GetInt("Sticker" + i) == 1)
                {
                    continue;
                }
                Items[i].SetActive(true);
                Highlight[i].SetActive(true);
                SoundManager.instance.SelectionPlay();
                yield return new WaitForSeconds(0.3f);
                Items[i].SetActive(false);
                Highlight[i].SetActive(false);
                yield return new WaitForSeconds(0.05f);
            }
        }
        Items[Give].SetActive(true);
        Highlight[Give].SetActive(true);
        // shiny[Give].enabled = true;
        // shiny[Give].Play();
        int ac = PrefsManager.AvailableStickers;
        PrefsManager.AvailableStickers += 1;
        PlayerPrefs.SetInt("Sticker" + Give, 1);
        PrefsManager.StickerNumber = Give;
        Button[Give].interactable = true;
        GameManager.instance.dashBoardManager.EnableDashBoardReferences();
        if (ac + 1 <= 6)
        {
            AdRandomButton.interactable = true;
            CashRandomButton.interactable = true;
        }
        SoundManager.instance.ShopUnlockPlay();
        EventSystem.SetActive(true);
        
    }


}
