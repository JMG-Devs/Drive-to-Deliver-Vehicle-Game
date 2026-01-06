using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RewardSystem : MonoBehaviour
{
    public static RewardSystem instance;

    public GameObject ChooseReward;
    public GameObject[] Rewards;
    public int a, b, c;
    public Transform Point1, Point2, Point3;
    public GameObject FreeClaimButton, RewardedClaimButton, DiscardButton;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    void Start()
    {
        UIManager.instance.GamePlay.SetActive(false);
        a = CalculateRandom(-1, -1);
        b = CalculateRandom(a, -1);
        c = CalculateRandom(a, b);
        PrefsManager.Keys = 0;
        Invoke(nameof(DelayCall), 2);
        SoundManager.instance.ChestSlidePlay();
    }

    public void DelayCall()
    {
        MakeChild(Point1, Rewards[a].transform);
        MakeChild(Point2, Rewards[b].transform);
        MakeChild(Point3, Rewards[c].transform);
        SetRewardIfValid(a);
        UnlockItem(a);
    }


    int CalculateRandom(int exclude1, int exclude2)
    {
        int randomValue;

        do
        {
            randomValue = Random.Range(0, 18);
        }
        while (
            PlayerPrefs.GetInt("Reward" + randomValue) == 1 ||
            randomValue == exclude1 ||
            randomValue == exclude2
        );

        return randomValue;
    }


    public void GiveRewardAfterAd()
    {
        SoundManager.instance.ButtonPlay();
        MonetizationServices.Ads.ShowRewarded(ClaimItemsCallback, "ChestBox_reward_ad");
    }

    public void ClaimItemsCallback()
    {
        SetRewardIfValid(b);
        SetRewardIfValid(c);
        UnlockItem(b);
        UnlockItem(c);
        GameManager.instance.SwitchToOldCanvas();
    }


    public void MakeChild(Transform point, Transform reward)
    {
        reward.SetParent(point);
        reward.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        reward.gameObject.SetActive(true);
    }



    void SetRewardIfValid(int value)
    {
        if (value != 0 && value != 1 && value != 2)
        {
            PlayerPrefs.SetInt("Reward" + value, 1);
        }
    }



    public void UnlockItem(int value)
    {
        switch (value)
        {
            case 0:
                PrefsManager.Money += 200;
                break;

            case 1:
                PrefsManager.Money += 300;
                break;

            case 2:
                PrefsManager.Money += 500;
                break;

            case 3:
                EnableSteering(1);
                break;

            case 4:
                EnableSteering(2);
                break;

            case 5:
                EnableSteering(3);
                break;

            case 6:
                EnableSteering(4);
                break;

            case 7:
                EnableSteering(5);
                break;

            case 8:
                EnableDBItem(1);
                break;

            case 9:
                EnableDBItem(2);
                break;

            case 10:
                EnableDBItem(3);
                break;

            case 11:
                EnableDBItem(4);
                break;

            case 12:
                EnableDBItem(5);
                break;

            case 13:
                EnableGear(1);
                break;

            case 14:
                EnableGear(2);
                break;

            case 15:
                EnableGear(3);
                break;

            case 16:
                EnableGear(4);
                break;

            case 17:
                EnableGear(5);
                break;

        }
    }



    public void EnableSteering(int a)
    {
        PlayerPrefs.SetInt("Steering" + a, 1);
        PrefsManager.SteeringNumber = a;
    }

    public void EnableGear(int a)
    {
        PlayerPrefs.SetInt("Gear" + a, 1);
        PrefsManager.StickNumber = a;
    }

    public void EnableDBItem(int a)
    {
        PlayerPrefs.SetInt("DBItem" + a, 1);
        PrefsManager.DashBoardItemNumber = a;
    }

    public void SwitchToRewardedClaimItems()
    {
        FreeClaimButton.SetActive(false);
        RewardedClaimButton.SetActive(true);
        DiscardButton.SetActive(true);

        Point1.DOScale(Vector3.zero, 0.25f);
        Point2.gameObject.SetActive(true);
        Point3.gameObject.SetActive(true);
        SoundManager.instance.AfterCardClickedPlay();
    }


    public void DiscardButtonCall()
    {
        GameManager.instance.SwitchToOldCanvas();
        SoundManager.instance.ButtonPlay();
    }



}
