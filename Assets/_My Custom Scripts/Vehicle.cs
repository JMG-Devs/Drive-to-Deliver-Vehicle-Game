using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Vehicle_Type
{
    MailVan, BankVan, Police, SchoolBus, FireBrigade, 
    Ambulance, Crane, FireTruck, TowTruck, RaceCar, 
    Taxi , OilTankerTrailer, OilTanker, BagageTruck, 
    LoadedVan, ForkLifter, Jeep, PickupTruck, RollarTruck,
    GarbageTruck,NewsVan,CityBus,IceCreamVan, OilTanker1, 
    OilTanker2, OilTankerTrailer1, OilTankerTrailer2, 
    Taxi1, Taxi2, Taxi3, DirtTruck,None
}
public class Vehicle : MonoBehaviour
{
    [Header("Have Pickup")]
    public bool havePickUp = false;

    [Header("Vehicle Type")]
    public Vehicle_Type vehicleType;

    [Header("Have Sire")]
    public bool haveSiren;

    [Header("Siren Lights")]
    public GameObject sirenLights;

    private void Awake()
    {
        // Agar Tou Pick Up Sytem ni ha
        if (havePickUp == false)
          //  if (GetComponent<PickUpVehicle>() != null)
            {
              //  print("NO PICKIP");
           //     GetComponent<PickUpVehicle>().enabled = false;
            }

    }

    public void SirenOn()
    {
        if (haveSiren && sirenLights)
            sirenLights.SetActive(true);
    }

    public void SirenOff()
    {
        if (haveSiren && sirenLights)
            sirenLights.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       // if (other.CompareTag("ArrowCheckpoint"))
       // {
        //    other.gameObject.GetComponent<BoxCollider>().enabled = false;
           // other.gameObject.GetComponentInParent<VfxCheckpoints>().vfxCollect();
       // }
    }

}
