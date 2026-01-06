using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public ParticleSystem particle;
    public MeshRenderer selfMesh;
    public Collider selfCollider;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCollider.enabled = false;
            selfMesh.enabled = false;
            particle.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            selfCollider.enabled = false;
            selfMesh.enabled = false;
            particle.Play();
        }
    }
}
