using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyScript : MonoBehaviour
{
    public Collider selfCol;
    public UnityEvent actionPerform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            selfCol.enabled = false;
            PrefsManager.Keys += 1;
            SoundManager.instance.KeyPlay();
            actionPerform?.Invoke();
        }
    }
}
