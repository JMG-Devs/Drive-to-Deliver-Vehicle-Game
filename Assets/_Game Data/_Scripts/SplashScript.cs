using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Monetization.Runtime.Consent;
using GameAnalyticsSDK;
public class SplashScript : MonoBehaviour
{
    public float DelayTime = 4f;
    public Image fillBar;

    private void OnEnable()
    {
        PrivacyPolicyPanel.OnPolicyAcceptedEvent += Dummy;

    }

    private void OnDisable()
    {
        PrivacyPolicyPanel.OnPolicyAcceptedEvent -= Dummy;
    }

    void Start()
    {
        Application.targetFrameRate = 60;
        
        StartGame();
    }

    public void StartGame()
    {
        GameAnalytics.Initialize();
        PlayerPrefs.SetInt("Steering" + 0, 1);
        PlayerPrefs.SetInt("Gear" + 0, 1);
        PlayerPrefs.SetInt("DBItem" + 0, 1);
        PlayerPrefs.SetInt("Sticker" + 0, 1);
        fillBar.DOFillAmount(1, DelayTime).SetDelay(0.5f).OnComplete(() => GoToNextScene());
    }

    public void Dummy()
    {

    }

   public void GoToNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
