using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public SoundManager soundManager;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    public GameObject levelCompleteScreen, rewardScreen, levelFailScreen,
        DummyBG, MainMenu, SettingsPannel,GamePlay,Shop,NewVehiclePannel,ShopButton,settingsButton,HitandRun,Accident;
    public bool IsInShop,IsLevelStarted;

    private void OnEnable()
    {
        if (PrefsManager.LevelNumber < 3)
        {
            ShopButton.SetActive(false);
            settingsButton.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReloadSceneWithButton()
    {
        soundManager.ButtonPlay();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void EnableRewardScreen()
    {
        levelCompleteScreen.SetActive(false);
        rewardScreen.SetActive(true);
        soundManager.UnlockPlay();
    }

    public void DisabelSettings()
    {
        soundManager.ButtonPlay();
        SettingsPannel.SetActive(false);
    }
    public void EnableSettings()
    {
        soundManager.ButtonPlay();
        SettingsPannel.SetActive(true);
    }

    public void PP()
    {
        soundManager.ButtonPlay();
        Application.OpenURL("https://redshift-games.co/privacy-policy/");
    }

    public void SwitchToGamePlay()
    {
        MainMenu.SetActive(false);
        GamePlay.SetActive(true);
    }

    public void EnableShop()
    {
        IsInShop = true;
        Shop.SetActive(true);
        soundManager.ShopMusicPlay();
    }

    public void DisableShop()
    {
        IsInShop = false;
        Shop.SetActive(false);
        soundManager.GamePlayMusicPlay();
    }

}
