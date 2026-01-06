using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Detector : MonoBehaviour
{
    public MeshRenderer mesh;
    TowTruckController truck;

    public Material greenMaterial;
    public Material redMaterial;
    public GameObject tut2;
    IEnumerator Start()
    {

        //mesh.material.DOColor(new Color(255f,2f,0f,132f), 0); //Red

        mesh.material = redMaterial;
        yield return new WaitForSeconds(0.15f);

        truck = TowTruckController.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vehicle") == true)
        {
            truck.canPerformTask = true;
            //truck.canPerformTask = true;
            mesh.material = greenMaterial;
            if (PrefsManager.Button == 0 )
            {
                GameManager.instance.tutotialButton.SetActive(true);
                PrefsManager.Button = 1;
            }

            //mesh.material.DOColor(new Color(41f, 255f, 0f, 132f), 0.15f); //Green
            //mesh.material.DOColor(new Color(41f, 255f, 0f, 132f), "_Color", 0.15f); //Green
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vehicle") == true)
        {
            truck.canPerformTask = false;
            //truck.canPerformTask = false;
            mesh.material = redMaterial;

            //mesh.material.DOColor(new Color(255f, 2f, 0f, 132f), 0.15f); //Red
            //mesh.material.DOColor(new Color(255f, 2f, 0f, 132f), "_Color", 0.15f); //Red
        }
    }
}
