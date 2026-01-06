using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Collider selfCollider;
    public GameObject forwardCam, reverseCam;
    GameManager gameManager;
    public int cameraSwitchTime;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCollider.enabled = false;
            gameManager.brain.m_DefaultBlend.m_Time = cameraSwitchTime;
            gameManager.dashBoardManager.selfGearScript.ForwardCam = forwardCam;
            gameManager.dashBoardManager.selfGearScript.ReverseCam = reverseCam;
            forwardCam.SetActive(true);
        }
    }
}
