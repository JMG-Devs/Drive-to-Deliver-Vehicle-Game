using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HydrantScript : MonoBehaviour
{
    public Transform water;
    bool IsCollided;
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (IsCollided) return;

            IsCollided = true;
            water.SetParent(null);
            water.gameObject.SetActive(true);
            Invoke(nameof(TurnOff), 5);
        }
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}
