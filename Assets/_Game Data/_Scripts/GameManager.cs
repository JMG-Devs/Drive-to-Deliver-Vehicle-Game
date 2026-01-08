using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Events;
using Monetization.Runtime.Analytics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public SoundManager soundManager;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }
    public UIManager uiManager;
    public DashBoardManager dashBoardManager;
    public GameObject[] DashBoards, Steerings, Gears, DashBoardItems, Stickers;
    public Transform AllSteeringParent, AllGearParent, AllDashBoardItemsParent, AllStickersParent;
    public int ActiveDashBoard;
    public SteeringWheel3D steeringWheel3D;
    public GearScript gearScript;
    public Transform front, back;
    public GameObject forwardCam, reverseCam;
    public GameObject[] levels;
    public Rigidbody carToStop;
    public CinemachineBrain brain;
    public UnityEvent CranePressDown;
    public GameObject craneDashBoard;
    public Joystick3D joystick3DScript;
    public SkinnedMeshRenderer steeringFrontArcMesh, steeringBackArcMesh;
    public GameObject rewardSystemCam;
    public GameObject rccCanvas, eventSys;
    public GameObject[] TutorialsArray;
    public GameObject ActiveTutorial;
    public bool IsNewVehicleLevel, IsTutorial;
    public GameObject tutotialButton;
    bool IsLevelFailed;
    [Space(10)]
    [Header("Levels Info")]
    public int NumberOfLevels;
    public bool IsTesting;
    public int LevelNo;
    private void OnEnable()
    {

        if (PrefsManager.Keys > 2)
        {
            PrefsManager.Keys = 0;
        }


    }
    private void Start()
    {
        uiManager = UIManager.instance;
        soundManager = SoundManager.instance;
        switch (PrefsManager.LevelNumber)
        {
            case 2:
            case 6:
            case 11:
            case 14:
            case 17:
                IsNewVehicleLevel = true;
                uiManager.NewVehiclePannel.SetActive(true);

                break;
        }
        if (PrefsManager.LevelNumber < 3)
        {
            ActiveTutorial = TutorialsArray[PrefsManager.LevelNumber];
        }
        else if (PrefsManager.LevelNumber == 17)
        {
            ActiveTutorial = TutorialsArray[3];

        }

        Instantiate(GetLevelPrefab());
        //levels[PrefsManager.LevelNumber%NumberOfLevels].SetActive(true);

        MonetizationServices.Ads.ShowBanner();
        AnalyticsManager.LogLevelStart(PrefsManager.LevelNumber + 1);
    }

    private GameObject GetLevelPrefab()
    {
        if (IsTesting)
        {
            return Resources.Load<GameObject>("LevelsPrefab/Level_" + (LevelNo));
        }
        else
        {
            return Resources.Load<GameObject>("LevelsPrefab/Level_" + (PrefsManager.LevelNumber % NumberOfLevels + 1));
        }
    }
    public void EnableAppropriateDashBoard()
    {
        DashBoards[ActiveDashBoard].SetActive(true);
        if (uiManager.IsLevelStarted) return;

        uiManager.IsLevelStarted = true;
        if (PrefsManager.PP == 1)
        {
            uiManager.MainMenu.SetActive(true);

        }
    }
    public void EnableMainMenu()
    {
        //Enables MainMenu Through Privacy Policy Continue if Policy is accepted in gameplay rathar than Splash

        uiManager.MainMenu.SetActive(true);
    }

    public void DashBoardAnimateOut()
    {
        dashBoardManager.transform.DOLocalMove(new Vector3(0, -10, 0), 0.5f).SetEase(Ease.InOutSine).SetRelative(true);
    }

    public void DashBoardAnimateIn()
    {
        dashBoardManager.transform.DOLocalMove(new Vector3(0, 10, 0), 0.5f).SetEase(Ease.InOutSine).SetRelative(true).SetDelay(0.75f);
    }

    public void levelCompleted()
    {
        // steeringWheel3D.lineRenderer.enabled = false;
        uiManager.GamePlay.SetActive(false);
        steeringWheel3D.steeringArcMesh.enabled = false;
        steeringWheel3D.enabled = false;
        gearScript.enabled = false;
        dashBoardManager.selfRCCInputs.pressing = false;
        dashBoardManager.selfRCCInputs.enabled = false;
        DashBoardAnimateOut();
        PrefsManager.LevelNumber += 1;
        AnalyticsManager.LogLevelCompleteSuccessful(PrefsManager.LevelNumber);
        if (PrefsManager.Keys > 2)
        {
            uiManager.DummyBG.SetActive(true);
        }
        else
        {
            Invoke(nameof(SceneSwitch), 2);
        }
        soundManager.CheckPointPlay();

    }





    public void levelCompletedSceneSwitch()
    {
        AnalyticsManager.LogLevelCompleteSuccessful(PrefsManager.LevelNumber);
        if (PrefsManager.Keys > 2)
        {
            uiManager.DummyBG.SetActive(true);
        }
        else
        {
            Invoke(nameof(SceneSwitch), 2);
        }
        soundManager.CheckPointPlay();
    }

    public void SceneSwitch()
    {
        uiManager.levelCompleteScreen.SetActive(true);
    }

    public void LevelFailedSwitch()
    {

        uiManager.levelFailScreen.SetActive(true);
        soundManager.LevelFailPlay();
    }

    public void levelFailed()
    {
        // steeringWheel3D.lineRenderer.enabled = false;
        if (IsLevelFailed) return;

        IsLevelFailed = true;
        steeringWheel3D.steeringArcMesh.enabled = false;
        steeringWheel3D.enabled = false;
        gearScript.enabled = false;
        dashBoardManager.selfRCCInputs.pressing = false;
        dashBoardManager.selfRCCInputs.enabled = false;
        DashBoardAnimateOut();
        Invoke(nameof(LevelFailedSwitch), 2);
        AnalyticsManager.LogLevelCompleteFailed(PrefsManager.LevelNumber + 1);
    }


    public void SwitchToRewardSystem()
    {
        rewardSystemCam.SetActive(true);
        brain.gameObject.SetActive(false);
        rccCanvas.SetActive(false);
        uiManager.DummyBG.SetActive(false);
    }

    public void SwitchToOldCanvas()
    {
        brain.gameObject.SetActive(true);
        rewardSystemCam.SetActive(false);
        Invoke(nameof(SceneSwitch), 1f);
    }

}
