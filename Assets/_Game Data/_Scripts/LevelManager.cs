using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int DashBoardToActivate;
    public Transform carFront, carBack;
    GameManager gameManager;
    public GameObject forwardCam, reverseCam, LevelStartCam;
    public Rigidbody carToStop;
    public float cameraSwitchDelay=0.1f;

    public SkinnedMeshRenderer steeringFrontArcMesh, steeringBackArcMesh;
    private void OnEnable()
    {
        gameManager = GameManager.instance;
        gameManager.forwardCam = forwardCam;
        gameManager.reverseCam = reverseCam;
        gameManager.carToStop = carToStop;
        gameManager.ActiveDashBoard = DashBoardToActivate;
        gameManager.front = carFront;
        gameManager.back = carBack; 
        gameManager.steeringFrontArcMesh = steeringFrontArcMesh;
        gameManager.steeringBackArcMesh = steeringBackArcMesh;

        if (gameManager.IsNewVehicleLevel)
        {
            Invoke(nameof(EnableWithDelay), 2.5f);
        }
        else
        {
        gameManager.EnableAppropriateDashBoard();

        }
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(cameraSwitchDelay);
        LevelStartCam.SetActive(false);
    }

    public void EnableWithDelay()
    {
        gameManager.EnableAppropriateDashBoard();
    }

}
