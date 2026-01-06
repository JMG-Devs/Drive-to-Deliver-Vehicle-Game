using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CivilianHit : MonoBehaviour
{
    public Animator selfAnim;
    public Collider selfCollider;
    public Rigidbody selfRb;
    public float forceAmount = 5, liftFactor = 1.5f;
    Vector3 dir;
    public DOTweenAnimation dotAnim;
    bool IsHit;
 
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (IsHit) return;

            IsHit = true;
            if (dotAnim != null)
            {
                dotAnim.DOPause();
            }
            selfAnim.updateMode = AnimatorUpdateMode.AnimatePhysics;
            selfAnim.applyRootMotion = false;
            selfCollider.enabled = false;
           
            selfAnim.enabled = false;
            dir = selfRb.position - other.transform.position;
            selfRb.AddForce((dir + (Vector3.up * liftFactor)) * forceAmount, ForceMode.Impulse);
            UIManager.instance.HitandRun.SetActive(true);
            GameManager.instance.levelFailed();
        }
    }
}
