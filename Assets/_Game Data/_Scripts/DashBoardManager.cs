using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DashBoardManager : MonoBehaviour
{


    public GameManager gameManager;
    public Transform dashBoardRootForSteering, dashBoardRootForGear, RootForDashBoardItem, RootForStickers;
    public GameObject defaultSteering, defaultGear;
    //public RCC_MobileButtons rccButtonScript;
    //public RCC_UI_Controller selfRCCInputs;
    public SteeringWheel3D self3dController;
    public GearScript selfGearScript;
    public bool IsPlaneDashBoard;

    private void OnEnable()
    {
        if (gameManager == null)
        {
            gameManager = GameManager.instance;
        }
        gameManager.dashBoardManager = this;
        gameManager.AllSteeringParent.SetParent(dashBoardRootForSteering);
        gameManager.AllSteeringParent.localPosition = Vector3.zero;
        gameManager.AllSteeringParent.localEulerAngles = Vector3.zero;
        gameManager.AllGearParent.SetParent(dashBoardRootForGear);
        gameManager.AllGearParent.localPosition = Vector3.zero;
        gameManager.AllGearParent.localEulerAngles = Vector3.zero;
        gameManager.AllDashBoardItemsParent.SetParent(RootForDashBoardItem);
        gameManager.AllDashBoardItemsParent.localPosition = Vector3.zero;
        gameManager.AllDashBoardItemsParent.localEulerAngles = Vector3.zero;

        gameManager.AllStickersParent.SetParent(RootForStickers);
        gameManager.AllStickersParent.localPosition = Vector3.zero;
        gameManager.AllStickersParent.localEulerAngles = Vector3.zero;



        self3dController.carFront = gameManager.front;
        self3dController.steeringArcMesh = gameManager.steeringFrontArcMesh;
        self3dController.steeringFrontArcMesh = gameManager.steeringFrontArcMesh;
        self3dController.steeringBackArcMesh = gameManager.steeringBackArcMesh;
        selfGearScript.ForwardCam = gameManager.forwardCam;
        self3dController.carBack = gameManager.back;
        selfGearScript.ReverseCam = gameManager.reverseCam;

        gameManager.steeringWheel3D = self3dController;
        gameManager.gearScript = selfGearScript;
        if (PrefsManager.LevelNumber < 3||PrefsManager.LevelNumber==17)
        {
            gameManager.IsTutorial = true;
            if (PrefsManager.PP == 1)
            {
                gameManager.eventSys.SetActive(false);

            }
            gameManager.ActiveTutorial.SetActive(true);
        }
        EnableDashBoardReferences();
        // PrefsManager.SteeringNumber += 1;
        //  PrefsManager.StickNumber += 1;
        //  PrefsManager.DashBoardItemNumber += 1;
    }

    public void EnableDashBoardReferences()
    {
        EnableSelectedSteering();
        EnableSelectedStick();
        EnableSelectedDashBoardItem();
        EnableSelectedSticker();
    }

    public void EnableSelectedSteering()
    {
        if (IsPlaneDashBoard) return;

        for (int i = 1; i < gameManager.Steerings.Length; i++)
        {
            gameManager.Steerings[i].SetActive(false);
        }


        defaultSteering.SetActive(false);

        if (PrefsManager.SteeringNumber == 0)
        {
            defaultSteering.SetActive(true);
        }
        else
        {
            gameManager.Steerings[PrefsManager.SteeringNumber].SetActive(true);
        }
    }

    public void EnableSelectedStick()
    {
        if (IsPlaneDashBoard) return;

        for (int i = 1; i < gameManager.Gears.Length; i++)
        {
            gameManager.Gears[i].SetActive(false);
        }


        defaultGear.SetActive(false);

        if (PrefsManager.StickNumber == 0)
        {
            defaultGear.SetActive(true);
        }
        else
        {
            gameManager.Gears[PrefsManager.StickNumber].SetActive(true);
        }
    }

    public void EnableSelectedDashBoardItem()
    {

        for (int i = 1; i < gameManager.DashBoardItems.Length; i++)
        {
            gameManager.DashBoardItems[i].SetActive(false);
        }

        if (PrefsManager.DashBoardItemNumber == 0)
            return;

        gameManager.DashBoardItems[PrefsManager.DashBoardItemNumber].SetActive(true);

    }

    public void EnableSelectedSticker()
    {


        for (int i = 1; i < gameManager.Stickers.Length; i++)
        {
            gameManager.Stickers[i].SetActive(false);
        }

        if (PrefsManager.StickerNumber == 0)
            return;

        gameManager.Stickers[PrefsManager.StickerNumber].SetActive(true);

    }



    public void FeedRCCReferecne()
    {
        //rccButtonScript.gasButton = selfRCCInputs;
        //rccButtonScript.brakeButton = selfRCCInputs;
        //rccButtonScript.steeringWheel3D = self3dController;
        //rccButtonScript.gameObject.SetActive(true);
    }
}
