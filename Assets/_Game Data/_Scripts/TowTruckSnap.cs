using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowTruckSnap : MonoBehaviour
{
    public Transform parkingPoint,player;
    public float moveSpeed;
    public float rotateSpeed;
    public bool isLerpStarting = false;
    public Collider selfCollider; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCollider.enabled = false;
            isLerpStarting = true;
        }
    }

    void FixedUpdate()
    {
        if (!isLerpStarting) return;

        // Smooth position
        player.position = Vector3.Lerp(
            player.position,
            parkingPoint.position,
            Time.deltaTime * moveSpeed
        );

        // Smooth rotation
        player.rotation = Quaternion.Lerp(
            player.rotation,
            parkingPoint.rotation,
            Time.deltaTime * rotateSpeed
        );

        // Finish condition
        if (Vector3.Distance(player.position, parkingPoint.position) < 0.5/*0.05f*/)
        {
            isLerpStarting = false;

            //Destroy(this); // optional optimization
        }
    }

}
