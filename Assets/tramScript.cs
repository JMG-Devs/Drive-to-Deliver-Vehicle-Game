using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tramScript : MonoBehaviour
{
    bool IsHit;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (IsHit) return;

            IsHit = true;
            UIManager.instance.Accident.SetActive(true);
            GameManager.instance.levelFailed();
        }
    }
}
