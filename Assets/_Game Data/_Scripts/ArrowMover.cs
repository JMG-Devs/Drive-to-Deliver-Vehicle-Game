using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMover : MonoBehaviour
{
    public float speed = 0.5f;
    public Renderer rend;
    private Material firstMat;
    void Start()
    {
        firstMat = rend.materials[0];
    }

    void Update()
    {
        if (firstMat == null) return;

        Vector2 offset = firstMat.mainTextureOffset;
        offset.x -= speed * Time.deltaTime;
        firstMat.mainTextureOffset = offset;
    }

}
