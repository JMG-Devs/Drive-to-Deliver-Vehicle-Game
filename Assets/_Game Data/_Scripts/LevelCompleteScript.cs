using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelCompleteScript : MonoBehaviour
{

    public GameObject[] Highlight, RewardScreenReward;
    public Image[] FillVehicle;
    int VehicleToGive;
    public float[] starsToUnlock = { 6, 12, 15, 9, 9 };
    public TMP_Text[] starsText;
    public float starsDiff;
    public bool RewardAvailable;
    public Scrollbar scrollBar;
    public TMP_Text walletText;
    int myMoney;
    public Button nextButton, ClaimButton;
    public GameObject moneyParticle;
   public int startStar, endStar;

    private void OnEnable()
    {
        VehicleToGive = PrefsManager.LevelCompleteVehicle;
        UIManager.instance.GamePlay.SetActive(false);
        walletText.text = PrefsManager.Money + "$";
    }

    void Start()
    {
        FillImages();
        SoundManager.instance.LevelCompletePlay();
        MonetizationServices.Ads.ShowInterstitial("Level_Complete");
    }


    public void FillImages()
    {
        if (PrefsManager.LevelCompleteVehicle >= 5)
        {
            foreach (Image i in FillVehicle)
            {
                i.fillAmount = 1;
            }
            return;
        }
        scrollBar.value = VehicleToGive * 0.25f;
        Highlight[VehicleToGive].SetActive(true);
        RewardScreenReward[VehicleToGive].SetActive(true);
        if (VehicleToGive != 0)//To Fill previous Completed Rewards
        {
            for (int i = 0; i < VehicleToGive; i++)
            {
                FillVehicle[i].fillAmount = 1;
            }
        }
        PrefsManager.Stars += 3;
        CalculateReward();
        if (VehicleToGive == 4)
            return;

        for (int i = VehicleToGive + 1; i < FillVehicle.Length; i++)
        {
            FillVehicle[i].fillAmount = 0;
        }
    }

    public void CalculateReward()
    {
        starsDiff = starsToUnlock[VehicleToGive] - PrefsManager.Stars;
        StarDeductionCountAnimate();
        FillVehicle[VehicleToGive].DOFillAmount(PrefsManager.Stars / starsToUnlock[VehicleToGive], 1).SetDelay(1.25f).SetEase(Ease.InOutSine);
        if (starsDiff == 0)
        {
            PrefsManager.Stars = 0;
            PrefsManager.LevelCompleteVehicle += 1;
            RewardAvailable = true;
        }
    }

    public void StarDeductionCountAnimate()
    {
        endStar =(int)starsDiff;
        startStar = endStar + 3;
        starsText[VehicleToGive].text = startStar + "";
        DOTween.To(() => startStar, x => startStar = x, endStar, 1).SetDelay(1.25f).OnUpdate(() => starsText[VehicleToGive].text = startStar+"");

    }

    public void NextButton()
    {
        moneyParticle.SetActive(true);
        nextButton.interactable = false;
        ClaimButton.interactable = false;
        myMoney = PrefsManager.Money;
        PrefsManager.Money += 200;
        DOTween.To(() => myMoney, x => myMoney = x, PrefsManager.Money, 1).SetDelay(2f).OnStart(SoundManager.instance.CashPlay)
          .OnUpdate(() => walletText.text = myMoney + "$")
          .OnComplete(() => ScreenChangeAfterMoney());
        SoundManager.instance.PurchasePlay();

    }

    public void ScreenChangeAfterMoney()
    {
        if (RewardAvailable)
        {
            UIManager.instance.EnableRewardScreen();
        }
        else
        {
            UIManager.instance.ReloadScene();
        }
    }

    public void GiveMoneyRewarded()
    {
        MonetizationServices.Ads.ShowRewarded(CashRewardCallBack,"Triple_Cash");
    }

    public void CashRewardCallBack()
    {
        moneyParticle.SetActive(true);
        nextButton.interactable = false;
        ClaimButton.interactable = false;
        myMoney = PrefsManager.Money;
        PrefsManager.Money += 600;
        DOTween.To(() => myMoney, x => myMoney = x, PrefsManager.Money, 1).SetDelay(2f).OnStart(SoundManager.instance.CashPlay)
            .OnUpdate(() => walletText.text = myMoney + "$")
            .OnComplete(() => ScreenChangeAfterMoney());
        SoundManager.instance.PurchasePlay();
    }

}
