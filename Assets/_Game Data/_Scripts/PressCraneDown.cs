using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressCraneDown : MonoBehaviour
{
   

    public Collider selfCollider;
    public GameObject tut;
    private void OnMouseDown()
    {
        if (TowTruckController.instance.canPerformTask)
        {
        selfCollider.enabled = false;
            TowTruckController.instance.enabled = false;
        GameManager.instance.CranePressDown?.Invoke();
          
            
                tut.SetActive(false);
            

        }
        
    }
}
